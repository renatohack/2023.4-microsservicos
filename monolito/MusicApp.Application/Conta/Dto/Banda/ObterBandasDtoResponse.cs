using MusicApp.Domain.Aplicativo.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Application.Conta.Dto
{
    public class ObterBandasDtoResponse
    {
        public List<Banda> Bandas { get; set; }
    }
}
