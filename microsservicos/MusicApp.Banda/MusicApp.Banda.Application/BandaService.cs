using MusicApp.Banda.Application.DTO;
using MusicApp.Banda.Domain.Aggregates;
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
                Id = banda.Id,
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
                    MensagemErro = "Banda não encontrada.",
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


        public ListarBandasDtoResp ListarBandas()
        {
            ListarBandasDtoResp dtoResp = new ListarBandasDtoResp()
            {
                Bandas = bandaRepo.ListarBandas(),
            };

            return dtoResp;
        }


        // MUSICA 
        public void AdicionarMusicas(Guid idBanda, AdicionarMusicasDtoReq dtoReq)
        {
            domain.Banda banda = bandaRepo.ObterBandaPorId(idBanda);

            if (banda == null)
            {
                ErroNegocio erroNegocio = new ErroNegocio()
                {
                    MensagemErro = "Banda não encontrada.",
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

                musicaDto.Id = musica.Id;

                musicasParaIncluir.Add(musica);
            }

            banda.AdicionarMusicas(musicasParaIncluir);
            bandaRepo.SalvarBanda(banda);

        }


        public BuscarMusicasPorNomeDtoResp ObterMusicasPorNome(Guid idBanda, string nome)
        {
            domain.Banda banda = bandaRepo.ObterBandaPorId(idBanda);

            if (banda == null)
            {
                ErroNegocio erroNegocio = new ErroNegocio()
                {
                    MensagemErro = "Banda não encontrada.",
                    NomeErro = nameof(ObterBandaPorId),
                };

                throw new BusinessException(erroNegocio);
            }

            BuscarMusicasPorNomeDtoResp dtoResp = new BuscarMusicasPorNomeDtoResp()
            {
                Musicas = new List<MusicaDto>(),
            };

            List<domain.Musica> musicas = banda.ObterMusicasPorNome(nome);
            
            foreach (domain.Musica musica in musicas)
            {
                MusicaDto musicaDto = new MusicaDto()
                {
                    Id = musica.Id,
                    Nome = musica.Nome,
                };

                dtoResp.Musicas.Add(musicaDto);
            }

            return dtoResp;
        }

        public ObterMusicaPorIdDtoResp ObterMusicaPorId(Guid idMusica)
        {
            List<domain.Banda> bandas = bandaRepo.ListarBandas();

            domain.Musica musicaResult = null;

            foreach (domain.Banda banda in bandas) 
            { 
                foreach (domain.Musica musica in banda.Musicas)
                {
                    if (musica.Id == idMusica)
                    {
                        musicaResult = musica; 
                        break;
                    }
                }
            }

            if (musicaResult == null)
            {
                ErroNegocio erroNegocio = new ErroNegocio()
                {
                    MensagemErro = "Musica não encontrada.",
                    NomeErro = nameof(ObterMusicaPorId),
                };

                throw new BusinessException(erroNegocio);
            }

            ObterMusicaPorIdDtoResp dtoResp = new ObterMusicaPorIdDtoResp()
            {
                Id = idMusica,
                Banda = musicaResult.Banda,
                Nome = musicaResult.Nome,
            };

            return dtoResp;
        }


    }
}
