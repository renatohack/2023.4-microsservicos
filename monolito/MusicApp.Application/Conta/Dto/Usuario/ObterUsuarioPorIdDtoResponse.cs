using MusicApp.Domain.Aplicativo.Aggregates;
using MusicApp.Domain.Conta.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Application.Conta.Dto
{
    public class ObterUsuarioPorIdDtoResponse
    {
        public Guid IdUsuario { get; set; }
        public string Nome { get; set; }
        public List<Assinatura> Assinaturas { get; set; }
        public List<Playlist> Playlists { get; set; }
        public List<Banda> BandasFavoritas { get; set; }
        public List<CartaoCredito> CartoesCredito { get; set; }
    }
}
