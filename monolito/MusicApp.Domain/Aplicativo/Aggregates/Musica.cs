using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Domain.Aplicativo.Aggregates
{
    public class Musica
    {
        public Guid Id { get; set; }
        public String Nome { get; set; }
        public Banda Banda { get; set; }
        public List<Playlist> Playlists { get; set; }

        public Musica() 
        {
            this.Id = Guid.NewGuid();

            this.Playlists = new List<Playlist>();
        }
    }
}
