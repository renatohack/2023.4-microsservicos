using MusicApp.Banda.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Banda.Application.DTO
{
    public class ObterBandaPorIdDtoResp
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public List<Musica> Musicas { get; set; }
    }
}
