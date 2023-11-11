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
            this.Transacoes = new List<Pagamento.Aggregates.Transacao>();
        }

        private bool PossuiLimiteDisponivel(CartaoCredito cartao, decimal valor) 
        {
            return true;
        }

        private bool AltaFrequenciaPequenoIntervalo(CartaoCredito cartao, Comerciante comerciante, decimal valor) 
        {
            return true;
        }

        private bool TransacaoDuplicada(CartaoCredito cartao, Comerciante comerciante, decimal valor) 
        {
            return true;
        }

        public void CriarTransacao(CartaoCredito cartao, Comerciante comerciante, decimal valor) 
        {

        }


        public void ValidarTransacao(CartaoCredito cartao, Comerciante comerciante, decimal valor) 
        { 
            
        }

        public void RealizarTransacao(CartaoCredito cartao, Comerciante comerciante, decimal valor) 
        {

        }




    }
}
