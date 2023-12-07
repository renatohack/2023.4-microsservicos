using MusicApp.Usuario.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Usuario.Application.Dto
{
    public class FavoritarBandasDtoResponse
    {
        public Guid IdUsuario { get; set; }

        public List<Banda> BandasFavoritas { get; set; }
    }
}
