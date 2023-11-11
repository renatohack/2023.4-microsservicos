using MusicApp.Domain.Pagamento.Aggregates;
using MusicApp.Domain.Pagamento.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Domain.Usuario.Aggregates
{
    public class CartaoCredito
    {

        private const int MAX_FREQUENCIA = 3;
        private const int MIN_INTERVALO_MINUTOS = 2;


        // Info
        public Guid Id { get; set; }
        public bool CartaoAtivo { get; set; }
        public decimal LimiteDisponivel { get; set; }
        public string Numero { get; set; }

        // Transacoes
        public List<Pagamento.Aggregates.Transacao> Transacoes { get; set; }


        // Construtor
        public CartaoCredito() 
        {
            this.Id = Guid.NewGuid();

            this.Transacoes = new List<Pagamento.Aggregates.Transacao>();
        }


        // METODOS
        private bool PossuiLimiteDisponivel(decimal valor) => this.LimiteDisponivel >= valor;

        private bool AltaFrequenciaPequenoIntervalo() 
        {
            if (this.Transacoes.Count >= MAX_FREQUENCIA) return false;
            
            DateTime horarioAntepenultimaTransacao = this.Transacoes[-3].DataTransacao;
            DateTime horarioAtual = DateTime.Now;

            if (horarioAtual.Subtract(horarioAntepenultimaTransacao).TotalMinutes < 2)
            {
                return true;
            }

            return false;
        }

        private bool TransacaoDuplicada(Comerciante comerciante, decimal valor) 
        {
            if (!this.Transacoes.Any()) return false;

            DateTime horarioAtual = DateTime.Now;

            Transacao ultimaTransacao = this.Transacoes[-1];
            decimal ultimoValor = ultimaTransacao.Valor;
            string ultimoCnpj = ultimaTransacao.Comerciante.Cnpj;
            DateTime ultimoHorario = ultimaTransacao.DataTransacao;

            if (ultimoValor == valor && 
                ultimoCnpj == comerciante.Cnpj && 
                horarioAtual.Subtract(ultimoHorario).TotalMinutes < 2) return true;

            return false;

        }

        private void ValidarTransacao(Comerciante comerciante, decimal valor)
        {
            if (!this.CartaoAtivo) ;                        // ADD EXCEÇAO NA LISTA
            if (!PossuiLimiteDisponivel(valor)) ;           // ADD EXCEÇAO NA LISTA
            if (AltaFrequenciaPequenoIntervalo()) ;         // ADD EXCEÇAO NA LISTA
            if (TransacaoDuplicada(comerciante, valor)) ;   // ADD EXCEÇAO NA LISTA

            // SE HOUVER EXCEÇÃO NA LISTA, THROW
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

        }




    }
}
