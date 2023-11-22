using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Application.Conta.Dto
{
    public class CriarPlaylistDto
    {
        public Guid IdPlaylist { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public Guid IdUsuario { get; set; }

    }
}
