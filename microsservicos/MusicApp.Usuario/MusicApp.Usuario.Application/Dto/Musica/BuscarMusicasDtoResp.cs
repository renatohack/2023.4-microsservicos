using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain = MusicApp.Usuario.Domain.Aggregates;

namespace MusicApp.Usuario.Application.Dto.Musica
{
    public class BuscarMusicasDtoResp
    {
        public List<domain.Musica> Musicas { get; set; }
    }
}
