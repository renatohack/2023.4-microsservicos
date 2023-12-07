﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain = MusicApp.Plano.Domain.Aggregates;

namespace MusicApp.Repository
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


        public domain.Plano ObterPlanoPorId(Guid idPlano) => PlanoRepository._planos.FirstOrDefault(plano => plano.Id == idPlano);

    }
}
