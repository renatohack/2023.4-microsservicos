using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MusicApp.Banda.Domain.Aggregates
{
    public class Musica
    {
        public Guid Id { get; set; }

        public String Nome { get; set; }

        [JsonIgnore]
        public Banda Banda { get; set; }


        public Musica() 
        {
            this.Id = Guid.NewGuid();
        }
    }
}
