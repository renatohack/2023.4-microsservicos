using MusicApp.Banda.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Banda.Application.DTO
{
    public class AdicionarMusicasDtoReq
    {
        [Required]
        public List<MusicaDto> Musicas {  get; set; }
    }
}
