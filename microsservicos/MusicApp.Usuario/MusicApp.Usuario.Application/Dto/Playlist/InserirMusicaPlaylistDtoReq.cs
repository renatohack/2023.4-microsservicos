using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Usuario.Application.Dto.Playlist
{
    public class InserirMusicaPlaylistDtoReq
    {
        public Guid IdUsuario { get; set; }
        public Guid IdPlaylist { get; set; }
        public Guid IdMusica { get; set; }
    }
}
