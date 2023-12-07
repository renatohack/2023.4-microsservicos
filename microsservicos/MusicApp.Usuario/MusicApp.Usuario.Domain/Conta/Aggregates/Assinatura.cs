using MusicApp.Usuario.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Usuario.Domain.Aggregates
{
    public class Assinatura
    {
        public Guid Id { get; set; }
        public Plano Plano { get; set; }
        public bool AssinaturaAtiva { get; set; }


        public Assinatura() {
            this.Id = Guid.NewGuid();
        }
    }


}
