using MusicApp.Domain.Conta.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


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
