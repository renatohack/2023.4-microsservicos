using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain = MusicApp.Plano.Domain.Aggregates;

namespace MusicApp.Plano.Application.DTO
{
    public class ListarPlanosDtoResp
    {
        public List<domain.Plano> Planos { get; set; }
    }
}
