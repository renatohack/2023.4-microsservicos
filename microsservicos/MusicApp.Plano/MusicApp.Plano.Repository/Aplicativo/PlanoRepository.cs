using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain = MusicApp.Plano.Domain.Aggregates;

namespace MusicApp.Plano.Repository
{
    public class PlanoRepository
    {

        private static List<domain.Plano> _planos;

        public PlanoRepository() 
        { 

            if (PlanoRepository._planos == null)
            {
                PlanoRepository._planos = new List<domain.Plano>();

                domain.Plano planoBase = new domain.Plano() {
                    Nome = "Plano Base",
                    Valor = 20M,
                    Id = new Guid("8D044595-D4A6-4E1A-9F09-DAB92205C71C")
                };

                PlanoRepository._planos.Add(planoBase);
            }
        }


        public void SalvarPlano(domain.Plano plano)
        {
            domain.Plano planoBanco = this.ObterPlanoPorId(plano.Id);

            if (planoBanco == null)
            {
                PlanoRepository._planos.Add(plano);
            }
            else
            {
                int indexToUpdate = PlanoRepository._planos.FindIndex(p => p.Id.Equals(plano.Id));
                PlanoRepository._planos[indexToUpdate] = plano;
            }
        }


        public domain.Plano ObterPlanoPorId(Guid idPlano) => PlanoRepository._planos.FirstOrDefault(plano => plano.Id == idPlano);

        public List<domain.Plano> ListarPlanos() => PlanoRepository._planos.Select(plano => plano).ToList();

    }
}
