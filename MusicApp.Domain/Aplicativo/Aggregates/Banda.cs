using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Domain.Aplicativo.Aggregates {
    public class Banda 
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
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
