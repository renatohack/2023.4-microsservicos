using MusicApp.Domain.Pagamento.Aggregates;
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

    }
}
