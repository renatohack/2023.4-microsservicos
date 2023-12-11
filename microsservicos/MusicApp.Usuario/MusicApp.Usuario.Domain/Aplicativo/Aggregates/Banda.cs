using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MusicApp.Usuario.Domain.Aggregates {
    public class Banda 
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        [JsonIgnore]
        public List<Musica> Musicas { get; set; }

        public Banda() 
        {
            this.Id = Guid.NewGuid();

            this.Musicas = new List<Musica>();
        }


        public List<Musica> BuscarMusicas(string nome) => this.Musicas.Where(musica => musica.Nome.ToUpper().Contains(nome.ToUpper())).ToList();


        public void AdicionarMusicas(List<Musica> musicas)
        {
            this.Musicas.AddRange(musicas);
        }

        public void AdicionarMusicas(Musica musica)
        {
            this.Musicas.Add(musica);
        }

    }
}
