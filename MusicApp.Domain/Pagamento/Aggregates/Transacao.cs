using MusicApp.Domain.Pagamento.ValueObjects;
using MusicApp.Domain.Conta.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Domain.Pagamento.Aggregates {
    public class Transacao 
    {

        public Guid Id { get; set; }
        public CartaoCredito CartaoCredito { get; set;}
        public Comerciante Comerciante { get; set;}
        public decimal Valor { get; set;}
        public DateTime DataTransacao { get; set; }



        public Transacao() 
        {
            this.Id = Guid.NewGuid();
            this.DataTransacao = DateTime.Now;
        }

    }
}
