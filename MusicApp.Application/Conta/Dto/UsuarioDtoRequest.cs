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
        public Guid IdUsuario { get; set; }

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
        public Guid IdPlaylist { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public Guid IdUsuario { get; set; }

    }


    // CARTAO
    public class CartaoCreditoDto
    {
        public Guid IdCartaoCredito { get; set; }

        public string Numero { get; set; }

        public bool CartaoAtivo { get; set; }

        public decimal LimiteDisponivel { get; set; }

    }


}
