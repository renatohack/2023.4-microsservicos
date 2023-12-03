using MusicApp.Domain.Aplicativo.Aggregates;
using MusicApp.Domain.Conta.Aggregates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Application.Conta.Dto
{
    // USUARIO
    public class CriarContaDtoResponse
    {
        public Guid IdUsuario { get; set; }

    }

    public class ObterUsuarioPorIdDtoResponse
    {
        public Guid IdUsuario { get; set; }
        public String Nome { get; set; }
        public List<Assinatura> Assinaturas { get; set; }
        public List<Playlist> Playlists { get; set; }
        public List<Banda> BandasFavoritas { get; set; }
    }



    // CARTAO
    public class AdicionarCartaoCreditoDtoResponse
    {
        public Guid IdCartaoCredito { get; set; }
    }



}
