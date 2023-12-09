using MusicApp.Core.Exception;
using domain = MusicApp.Plano.Domain.Aggregates;
using MusicApp.Plano.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicApp.Plano.Application.DTO;

namespace MusicApp.Plano.Application
{
    public class PlanoService
    {

        private PlanoRepository planoRepository = new PlanoRepository();


        public CriarPlanoDtoResp CriarPlano(CriarPlanoDtoReq dtoReq)
        {
            domain.Plano plano = new domain.Plano()
            {
                Nome = dtoReq.Nome,
                Valor = dtoReq.Valor,
            };

            planoRepository.SalvarPlano(plano);

            CriarPlanoDtoResp dtoResp = new CriarPlanoDtoResp()
            {
                IdPlano = plano.Id,
            };

            return dtoResp;
        }



        public ObterPlanoPorIdDtoResp ObterPlanoPorId(Guid id)
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

            PlanoDto planoDto = new PlanoDto()
            {
                IdPlano = plano.Id,
                Nome = plano.Nome,
                Valor = plano.Valor,
            };

            ObterPlanoPorIdDtoResp dtoResp = new ObterPlanoPorIdDtoResp()
            {
                Plano = planoDto
            };

            return dtoResp;
        }



        public ListarPlanosDtoResp ListarPlanos()
        {
            List<domain.Plano> planosRepo = planoRepository.ListarPlanos();

            ListarPlanosDtoResp dtoResp = new ListarPlanosDtoResp()
            {
                Planos = new List<PlanoDto>(),
            };
            

            foreach (domain.Plano plano in planosRepo)
            {
                PlanoDto planoDto = new PlanoDto
                {
                    IdPlano = plano.Id,
                    Nome = plano.Nome,
                    Valor = plano.Valor,
                };

                dtoResp.Planos.Add(planoDto);
            }

            return dtoResp;
        }

    }
}
