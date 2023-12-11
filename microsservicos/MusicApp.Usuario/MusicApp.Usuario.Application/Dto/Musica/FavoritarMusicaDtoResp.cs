using MusicApp.Usuario.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain = MusicApp.Usuario.Domain.Aggregates;

namespace MusicApp.Usuario.Application.Dto.Musica
{
    public class FavoritarMusicaDtoResp
    {
        public Guid IdUsuario { get; set; }

        public List<domain.Musica> MusicasFavoritas { get; set; }
    }
}
