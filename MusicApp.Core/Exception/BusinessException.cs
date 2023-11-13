using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Core.Exception
{
    public class BusinessException : System.Exception
    {
        public List<ErroNegocio> ListErros { get; set; } = new List<ErroNegocio>();

        public BusinessException() { }
        public BusinessException(ErroNegocio erroNegocio)
        {
            this.AdicionarErro(erroNegocio);
        }

        public void AdicionarErro(ErroNegocio erroNegocio)
        {
            this.ListErros.Add(erroNegocio);
        }

        public void LancarExcecoes()
        {
            if (this.ListErros.Any())
                throw this;
        }
    }


    public class ErroNegocio
    {
        public string NomeErro { get; set; } = "Erros de Validação";
        public string MensagemErro { get; set; }
    }
}
