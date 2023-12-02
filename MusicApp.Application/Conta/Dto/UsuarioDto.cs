using MusicApp.Domain.Conta.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MusicApp.Application.Conta.Dto
{
    public class UsuarioDto
    {

        public Guid IdUsuario { get; set; }
        
        public String Nome { get; set; }
        
        public Guid PlanoId { get; set; }

        public CartaoCreditoDto CartaoCredito { get; set; }

        public List<Assinatura> Assinaturas { get; set; }

    }

    public class CartaoCreditoDto
    {
        public Guid IdCartaoCredito { get; set; }
        
        public string Numero { get; set; }
        
        public bool CartaoAtivo { get; set; }
        
        public decimal LimiteDisponivel { get; set; }

    }
}
