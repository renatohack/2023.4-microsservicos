using MusicApp.Usuario.Application.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Usuario.Application.Dto
{
    public class CriarContaDtoReq
    {
        [Required]
        public String Nome { get; set; }

        [Required]
        public Guid PlanoId { get; set; }

        [Required]
        public CartaoDto Cartao { get; set; }
    }
}
