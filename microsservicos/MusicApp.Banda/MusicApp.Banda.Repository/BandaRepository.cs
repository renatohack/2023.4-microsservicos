﻿using domain = MusicApp.Banda.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Banda.Repository
{
    public class BandaRepository
    {

        private static List<domain.Banda> _bandas;

        public BandaRepository()
        {

            if (BandaRepository._bandas == null)
            {
                BandaRepository._bandas = new List<domain.Banda>();

                domain.Banda banda = new domain.Banda()
                {
                    Nome = "Queen",
                    Id = new Guid("BE431A65-6715-492A-A22C-4CC54CA9B029")
                };

                domain.Musica musica1 = new domain.Musica()
                {
                    Banda = banda,
                    Nome = "Bohemian Rhapsody",
                };

                domain.Musica musica2 = new domain.Musica()
                {
                    Banda = banda,
                    Nome = "Hammer to Fall",
                };

                domain.Musica musica3 = new domain.Musica()
                {
                    Banda = banda,
                    Nome = "Radio Ga Ga",
                };

                banda.AdicionarMusicas(new List<domain.Musica>() { musica1, musica2, musica3 });

                BandaRepository._bandas.Add(banda);
            }
        }


        public domain.Banda ObterBandaPorId(Guid idBanda) => BandaRepository._bandas.FirstOrDefault(banda => banda.Id == idBanda);

        public void SalvarBanda(domain.Banda banda)
        {
            domain.Banda bandaBanco = this.ObterBandaPorId(banda.Id);

            if (bandaBanco == null)
            {
                BandaRepository._bandas.Add(banda);
            }
            else
            {
                int indexToUpdate = BandaRepository._bandas.FindIndex(b => b.Id.Equals(banda.Id));
                BandaRepository._bandas[indexToUpdate] = banda;
            }
        }

        public List<domain.Banda> ListarBandas() => BandaRepository._bandas;
    }
}
