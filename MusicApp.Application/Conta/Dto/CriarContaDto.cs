using MusicApp.Domain.Conta.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MusicApp.Application.Conta.Dto
{
    public class CriarContaDto
    {

        public Guid Id { get; set; }

        [Required]
        public String Nome { get; set; }

        [Required]
        public Guid PlanoId { get; set; }

        public CartaoCreditoDto CartaoCredito { get; set; }


        public class CartaoCreditoDto
        {
            [Required]
            public string Numero { get; set; }
            
            [Required]
            public bool CartaoAtivo { get; set; }
            
            [Required]
            public decimal LimiteDisponivel { get; set; }

        }
    }
}
