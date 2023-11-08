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

        public Guid Id { get; set; }
        public List<Pagamento.Aggregates.Transacao> Transacoes { get; set; }
        public bool CartaoAtivo { get; set; }
        public decimal LimiteDisponivel { get; set; }
        public CartaoCredito() {
            this.Transacoes = new List<Pagamento.Aggregates.Transacao>();
        }

    }
}
