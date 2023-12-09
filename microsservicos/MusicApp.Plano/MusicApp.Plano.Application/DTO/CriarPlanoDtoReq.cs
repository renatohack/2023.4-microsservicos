using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Plano.Application.DTO
{
    public class CriarPlanoDtoReq
    {
        [Required]
        public string Nome { get; set; }
        
        [Required]
        public Decimal Valor { get; set; }
    }
}
