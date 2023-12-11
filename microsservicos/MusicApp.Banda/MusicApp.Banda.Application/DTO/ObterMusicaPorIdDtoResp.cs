using domain = MusicApp.Banda.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MusicApp.Banda.Application.DTO
{
    public class ObterMusicaPorIdDtoResp
    {
        public Guid Id { get; set; }

        public String Nome { get; set; }

        public domain.Banda Banda { get; set; }
    }
}
