using MusicApp.Domain.Aplicativo.Exception;
using MusicApp.Domain.Pagamento.Aggregates;
using MusicApp.Domain.Pagamento.ValueObjects;
using MusicApp.Core.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Domain.Conta.Aggregates
{
    public class CartaoCredito
    {

        private readonly int MAX_FREQUENCIA = 3;
        private readonly int MIN_INTERVALO_MINUTOS = 2;


        // Info
        public Guid Id { get; set; }
        public bool CartaoAtivo { get; set; }
        public decimal LimiteDisponivel { get; set; }
        public string Numero { get; set; }

        // Transacoes
        public List<Transacao> Transacoes { get; set; }


        // Construtor
        public CartaoCredito() 
        {
            this.Id = Guid.NewGuid();

            this.Transacoes = new List<Transacao>();
        }


        // METODOS
        private bool PossuiLimiteDisponivel(decimal valor) => this.LimiteDisponivel >= valor;

        private bool AltaFrequenciaPequenoIntervalo() 
        {
            if (this.Transacoes.Count < MAX_FREQUENCIA) return false;
            
            DateTime horarioAntepenultimaTransacao = this.Transacoes[^3].DataTransacao;
            DateTime horarioAtual = DateTime.Now;

            if (horarioAtual.Subtract(horarioAntepenultimaTransacao).TotalMinutes < MIN_INTERVALO_MINUTOS)
            {
                return true;
            }

            return false;
        }

        private bool TransacaoDuplicada(Comerciante comerciante, decimal valor) 
        {
            if (!this.Transacoes.Any()) return false;

            DateTime horarioAtual = DateTime.Now;

            Transacao ultimaTransacao = this.Transacoes[^1];
            decimal ultimoValor = ultimaTransacao.Valor;
            string ultimoCnpj = ultimaTransacao.Comerciante.Cnpj;
            DateTime ultimoHorario = ultimaTransacao.DataTransacao;

            if (ultimoValor == valor && 
                ultimoCnpj == comerciante.Cnpj && 
                horarioAtual.Subtract(ultimoHorario).TotalMinutes < MIN_INTERVALO_MINUTOS) return true;

            return false;

        }

        private void ValidarTransacao(Comerciante comerciante, decimal valor)
        {

            CartaoCreditoException cartaoException = new CartaoCreditoException(); 


            // Verificar se cartao esta ativo
            if (!this.CartaoAtivo)
            {
                ErroNegocio erroNegocio = new ErroNegocio() {
                    NomeErro = nameof(CartaoCreditoException),
                    MensagemErro = "Cartao de credito nao esta ativo."
                };

                cartaoException.AdicionarErro(erroNegocio);
            }

            // Verificar se possui limite disponivel
            if (!PossuiLimiteDisponivel(valor))
            {
                ErroNegocio erroNegocio = new ErroNegocio() {
                    NomeErro = nameof(CartaoCreditoException),
                    MensagemErro = "Cartao de credito nao possui limite."
                };

                cartaoException.AdicionarErro(erroNegocio);
            }

            // Verificar se ha 3 transacoes em um intervalo de 2 minutos
            if (AltaFrequenciaPequenoIntervalo())
            {
                ErroNegocio erroNegocio = new ErroNegocio() {
                    NomeErro = nameof(CartaoCreditoException),
                    MensagemErro = "Cartao de credito utilizado muitas vezes em um periodo curto."
                };

                cartaoException.AdicionarErro(erroNegocio);
            }

            // Verificar se eh transacao duplicada
            if (TransacaoDuplicada(comerciante, valor))
            {
                ErroNegocio erroNegocio = new ErroNegocio() {
                    NomeErro = nameof(CartaoCreditoException),
                    MensagemErro = "Transacao duplicada."
                };

                cartaoException.AdicionarErro(erroNegocio);
            }


            // Lancar excecao caso haja alguma
            cartaoException.LancarExcecoes();
        }

        private Transacao CriarTransacao(Comerciante comerciante, decimal valor) => new Transacao {
                                                                                                    CartaoCredito = this,
                                                                                                    Comerciante = comerciante,
                                                                                                    Valor = valor
                                                                                                  };

        public void RealizarTransacao(string cnpj, decimal valor) 
        {
            Comerciante comerciante = new Comerciante() { Cnpj = cnpj };

            ValidarTransacao(comerciante, valor);
            Transacao transacao = CriarTransacao(comerciante, valor);
            this.Transacoes.Add(transacao);
            this.LimiteDisponivel -= valor;

        }




    }
}
