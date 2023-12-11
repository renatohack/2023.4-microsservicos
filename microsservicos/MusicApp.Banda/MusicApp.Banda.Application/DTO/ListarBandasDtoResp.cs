using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain = MusicApp.Banda.Domain.Aggregates;

namespace MusicApp.Banda.Application.DTO
{
    public class ListarBandasDtoResp
    {
        public List<domain.Banda> Bandas { get; set; }
    }
}
