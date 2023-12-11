using domain = MusicApp.Usuario.Domain.Aggregates;
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
        public List<domain.Assinatura> Assinaturas { get; set; }
        public List<domain.Playlist> Playlists { get; set; }
        public List<domain.Banda> BandasFavoritas { get; set; }
        public List<domain.Cartao> CartoesCredito { get; set; }
    }
}
