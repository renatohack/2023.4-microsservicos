using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Usuario.Application.Dto.Musica
{
    public class FavoritarMusicaDtoReq
    {
        public Guid IdUsuario { get; set; }

        [Required]
        public Guid IdMusica { get; set; }
    }
}
