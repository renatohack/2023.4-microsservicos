using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicApp.Domain.Aplicativo.Aggregates;


namespace MusicApp.Domain.Usuario.Aggregates
{
    public class Usuario
    {

        public Guid Id { get; set; }
        public String Nome { get; set; }
        public CartaoCredito CartaoCredito { get; set;}
        public List<Assinatura> Assinatura { get; set; }
        public List<Aplicativo.Aggregates.Playlist> Playlists { get; set; }
        public List<Assinatura> Assinatura { get; set; }

    }
}
