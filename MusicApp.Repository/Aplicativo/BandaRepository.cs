using MusicApp.Domain.Aplicativo.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Repository.Aplicativo
{
    public class BandaRepository
    {

        private static List<Banda> _bandas;

        public BandaRepository()
        {

            if (BandaRepository._bandas == null)
            {
                BandaRepository._bandas = new List<Banda>();

                Banda banda = new Banda()
                {
                    Nome = "Queen",
                    Id = new Guid("BE431A65-6715-492A-A22C-4CC54CA9B029")
                };

                Musica musica1 = new Musica()
                {
                    Banda = banda,
                    Nome = "Bohemian Rhapsody",
                };

                Musica musica2 = new Musica()
                {
                    Banda = banda,
                    Nome = "Hammer to Fall",
                };

                Musica musica3 = new Musica()
                {
                    Banda = banda,
                    Nome = "Radio Ga Ga",
                };

                banda.AdicionarMusicas(new List<Musica>() { musica1, musica2, musica3 });

                BandaRepository._bandas.Add(banda);
            }
        }


        public Banda ObterBandaPorId(Guid idBanda) => BandaRepository._bandas.FirstOrDefault(banda => banda.Id == idBanda);

    }
}
