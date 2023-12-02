using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Application.Conta.Dto
{
    public class CartaoCreditoDto
    {
        public Guid IdCartaoCredito { get; set; }

        public string Numero { get; set; }

        public bool CartaoAtivo { get; set; }

        public decimal LimiteDisponivel { get; set; }

    }
}
