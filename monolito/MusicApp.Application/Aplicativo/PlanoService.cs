using MusicApp.Core.Exception;
using MusicApp.Domain.Aplicativo.Aggregates;
using MusicApp.Repository.Aplicativo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Application.Aplicativo
{
    public class PlanoService
    {

        private PlanoRepository planoRepository = new PlanoRepository();


        public Plano ObterPlanoPorId(Guid id)
        {
            Plano plano = this.planoRepository.ObterPlanoPorId(id);

            if (plano == null)
            {
                ErroNegocio erroNegocio = new ErroNegocio()
                {
                    MensagemErro = "Plano não encontrado.",
                    NomeErro = nameof(ObterPlanoPorId),
                };
                throw new BusinessException(erroNegocio);
            }

            return plano;
        }

    }
}
