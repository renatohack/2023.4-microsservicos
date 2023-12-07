using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Application.Conta.Dto
{
    public class AdicionarCartaoCreditoDtoRequest
    {
        [Required]
        public Guid IdUsuario { get; set; }

        [Required]
        public CartaoCreditoDto CartaoCredito { get; set; }
    }


}
