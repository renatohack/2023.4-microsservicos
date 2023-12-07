using MusicApp.Usuario.Domain.ValueObjects;
using MusicApp.Usuario.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Usuario.Domain.Aggregates {
    public class Transacao 
    {

        public Guid Id { get; set; }
        public Cartao Cartao { get; set;}
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
