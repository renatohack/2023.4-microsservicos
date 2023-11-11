using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Domain.Aplicativo.Aggregates {
    public class Playlist 
    {

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public List<Musica> Musicas { get; set; }

        public Playlist() 
        {
            this.Musicas = new List<Musica>();
        }

        
        public void AdicionarMusica(Musica musica) { }

        public List <Musica> BuscarMusicas() { return null; }

    }
}
