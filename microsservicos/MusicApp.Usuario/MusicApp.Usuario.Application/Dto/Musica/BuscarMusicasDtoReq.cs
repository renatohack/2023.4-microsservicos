using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Usuario.Application.Dto.Musica
{
    public class BuscarMusicasDtoReq
    {
        public Guid IdUsuario { get; set; }
        public string Nome{ get; set; }
    }
}
