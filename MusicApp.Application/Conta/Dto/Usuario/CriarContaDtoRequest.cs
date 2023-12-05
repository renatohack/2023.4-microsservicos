using MusicApp.Application.Conta.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Application.Conta.Dto
{
    public class CriarContaDtoRequest
    {
        [Required]
        public String Nome { get; set; }

        [Required]
        public Guid PlanoId { get; set; }

        [Required]
        public CartaoCreditoDto CartaoCredito { get; set; }
    }
}
