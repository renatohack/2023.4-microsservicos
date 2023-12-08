using MusicApp.Banda.Application.DTO;
using MusicApp.Banda.Repository;
using MusicApp.Core.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using domain = MusicApp.Banda.Domain.Aggregates;

namespace MusicApp.Banda.Application
{
    public class BandaService
    {

        BandaRepository bandaRepo = new BandaRepository();

        // BANDA
        public CriarBandaDtoResp CriarBanda(CriarBandaDtoReq dtoReq)
        {
            domain.Banda banda = new domain.Banda
            {
                Nome = dtoReq.Nome,
            };

            bandaRepo.SalvarBanda(banda);

            CriarBandaDtoResp dtoResp = new CriarBandaDtoResp()
            {
                IdBanda = banda.Id,
            };

            return dtoResp;
        }

        public ObterBandaPorIdDtoResp ObterBandaPorId(Guid idBanda)
        {
            domain.Banda banda = bandaRepo.ObterBandaPorId(idBanda);

            if (banda == null)
            {
                ErroNegocio erroNegocio = new ErroNegocio()
                {
                    MensagemErro = "Plano não encontrado.",
                    NomeErro = nameof(ObterBandaPorId),
                };

                throw new BusinessException(erroNegocio);
            }

            ObterBandaPorIdDtoResp dtoResp = new ObterBandaPorIdDtoResp()
            {
                Id = idBanda,
                Nome = banda.Nome,
                Musicas = banda.Musicas,
            };

            return dtoResp;
        }



        // MUSICA 
        public AdicionarMusicasDtoResp AdicionarMusicas(Guid idBanda, AdicionarMusicasDtoReq dtoReq)
        {
            domain.Banda banda = bandaRepo.ObterBandaPorId(idBanda);

            if (banda == null)
            {
                ErroNegocio erroNegocio = new ErroNegocio()
                {
                    MensagemErro = "Plano não encontrado.",
                    NomeErro = nameof(ObterBandaPorId),
                };

                throw new BusinessException(erroNegocio);
            }

            List<domain.Musica> musicasParaIncluir = new List<domain.Musica>();

            foreach (MusicaDto musicaDto in dtoReq.Musicas)
            {
                domain.Musica musica = new domain.Musica()
                {
                    Nome = musicaDto.Nome,
                    Banda = banda,
                };

                musicaDto.IdMusica = musica.Id;

                musicasParaIncluir.Add(musica);
            }

            banda.AdicionarMusicas(musicasParaIncluir);
            bandaRepo.SalvarBanda(banda);

            AdicionarMusicasDtoResp dtoResp = new AdicionarMusicasDtoResp()
            {
                Musicas = dtoReq.Musicas,
            };

            return dtoResp;
        }


        public ObterMusicasPorNomeDtoResp ObterMusicasPorNome(Guid idBanda, string nome)
        {
            domain.Banda banda = bandaRepo.ObterBandaPorId(idBanda);

            if (banda == null)
            {
                ErroNegocio erroNegocio = new ErroNegocio()
                {
                    MensagemErro = "Plano não encontrado.",
                    NomeErro = nameof(ObterBandaPorId),
                };

                throw new BusinessException(erroNegocio);
            }

            ObterMusicasPorNomeDtoResp dtoResp = new ObterMusicasPorNomeDtoResp()
            {
                Musicas = new List<MusicaDto>(),
            };

            List<domain.Musica> musicas = banda.ObterMusicasPorNome(nome);
            
            foreach (domain.Musica musica in musicas)
            {
                MusicaDto musicaDto = new MusicaDto()
                {
                    IdMusica = musica.Id,
                    Nome = musica.Nome,
                };

                dtoResp.Musicas.Add(musicaDto);
            }

            return dtoResp;
        }

    }
}
