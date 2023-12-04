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
        public string Nome { get; set; }
        public List<Assinatura> Assinaturas { get; set; }
        public List<Playlist> Playlists { get; set; }
        public List<Banda> BandasFavoritas { get; set; }
        public List<CartaoCredito> CartoesCredito { get; set; }
    }



    // CARTAO
    public class AdicionarCartaoCreditoDtoResponse
    {
        public Guid IdCartaoCredito { get; set; }
    }




    // PLAYLIST
    public class CriarPlaylistDtoResponse
    {
        public Guid IdPlaylist { get; set; }
    }



    // ASSINATURA
    public class AssinarPlanoDtoResponse
    {
        public Guid IdAssinatura { get; set; }
    }




    // BANDAS
    public class FavoritarBandasDtoResponse
    {
        public Guid IdUsuario { get; set; }

        public List<Banda> BandasFavoritas { get; set; }
    }

    public class ObterBandasPorSubstringDtoResponse
    {
        public List<Banda> Bandas { get; set; }
    }


}
