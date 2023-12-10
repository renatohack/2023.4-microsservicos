using MusicApp.Usuario.Domain.Aggregates;
using MusicApp.Usuario.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Usuario.Application.Dto
{
    public class ObterUsuarioPorIdDtoResp
    {
        public Guid IdUsuario { get; set; }
        public string Nome { get; set; }
        public List<Assinatura> Assinaturas { get; set; }
        public List<Playlist> Playlists { get; set; }
        public List<Banda> BandasFavoritas { get; set; }
        public List<Cartao> CartoesCredito { get; set; }
    }
}
