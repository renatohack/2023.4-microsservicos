using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Banda.Application.DTO
{
    public class ObterBandaPorIdDtoReq
    {
        [Required]
        public Guid IdBanda { get; set; }
    }
}