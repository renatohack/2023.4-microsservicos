using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Application.Conta.Dto
{

    // USUARIO
    public class CriarContaDtoRequest
    {
        [Required]
        public String Nome { get; set; }

        [Required]
        public Guid PlanoId { get; set; }

        [Required]
        public CartaoCreditoDto CartaoCredito { get; set; }
    }



    // PLAYLIST
    public class CriarPlaylistDtoRequest
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public Guid IdUsuario { get; set; }
    }


    // CARTAO
    public class AdicionarCartaoCreditoDtoRequest
    {
        [Required]
        public Guid IdUsuario { get; set; }

        [Required]
        public CartaoCreditoDto CartaoCredito { get; set; }
    }

    public class CartaoCreditoDto
    {
        public string Numero { get; set; }

        public bool CartaoAtivo { get; set; }

        public decimal LimiteDisponivel { get; set; }
    }



    // ASSINATURA
    public class AssinarPlanoDtoRequest
    {
        [Required]
        public Guid IdUsuario { get; set; }

        [Required]
        public Guid IdPlano { get; set; }

        [Required]
        public Guid IdCartaoCredito { get; set; }
    }




    // BANDAS
    public class FavoritarBandaDtoRequest
    {
        [Required]
        public Guid IdUsuario { get; set; }

        [Required]
        public Guid IdBanda { get; set; }
    }

    public class ObterBandasPorSubstringDtoRequest
    {
        [Required]
        public Guid IdUsuario { get; set; }

        [Required]
        public string Nome { get; set; }
    }

}
