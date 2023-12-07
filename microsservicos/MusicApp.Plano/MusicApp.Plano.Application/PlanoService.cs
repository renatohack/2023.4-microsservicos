using MusicApp.Core.Exception;
using domain = MusicApp.Plano.Domain.Aggregates;
using MusicApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Application
{
    public class PlanoService
    {

        private PlanoRepository planoRepository = new PlanoRepository();


        public domain.Plano ObterPlanoPorId(Guid id)
        {
            domain.Plano plano = this.planoRepository.ObterPlanoPorId(id);

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
