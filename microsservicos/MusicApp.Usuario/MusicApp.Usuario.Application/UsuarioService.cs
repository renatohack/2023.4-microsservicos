using MusicApp.Usuario.Repository;
using domain = MusicApp.Usuario.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using MusicApp.Core.Exception;
using MusicApp.Usuario.Application;
using MusicApp.Usuario.Application.Dto;
using MusicApp.Usuario.Application.Dto.Playlist;
using MusicApp.Usuario.Application.Dto.Musica;

namespace MusicApp.Usuario.Application
{
    public class UsuarioService
    {

        private UsuarioRepository usuarioRepo = new UsuarioRepository();
        private PlanoRepository planoRepo = new PlanoRepository();
        private BandaRepository bandaRepo = new BandaRepository();


        // USUARIO
        public async Task<CriarContaDtoResp> CriarConta(CriarContaDtoReq dtoReq)
        {

            // Pega plano no banco a partir do ID passado dentro da classe DTO
            domain.Plano plano = await planoRepo.ObterPlanoPorId(dtoReq.PlanoId);
            if (plano == null)
            {
                ErroNegocio erroNegocio = new ErroNegocio()
                {
                    MensagemErro = "Plano não encontrado.",
                    NomeErro = nameof(CriarConta),
                };
                throw new BusinessException(erroNegocio);
            }

            // Gera um objeto cartão a partir da classe DTO, para ser usado na criação do usuário
            domain.Cartao cartao = GerarObjetoCartao(dtoReq.Cartao);

            // Cria usuario, passando cartao criado e plano retornado
            domain.Usuario usuarioCriado = domain.Usuario.CriarUsuario(dtoReq.Nome, cartao, plano);

            // Gravar novo usuarioCriado na base
            this.usuarioRepo.SalvarUsuarioNaBase(usuarioCriado);

            // Retornar Conta DTO, com o ID atualizado
            CriarContaDtoResp dtoResp = new CriarContaDtoResp()
            {
                IdUsuario = usuarioCriado.Id,
            };

            return dtoResp;
            
        }

        public ObterUsuarioPorIdDtoResp ObterUsuarioPorId(Guid idUsuario)
        {
            domain.Usuario usuario = usuarioRepo.ObterUsuarioPorId(idUsuario);

            if (usuario == null)
            {
                ErroNegocio erroNegocio = new ErroNegocio()
                {
                    MensagemErro = "Usuário não encontrado.",
                    NomeErro = nameof(ObterUsuarioPorId),
                };
                throw new BusinessException(erroNegocio);
            }

            ObterUsuarioPorIdDtoResp dtoResp = new ObterUsuarioPorIdDtoResp()
            {
                IdUsuario = usuario.Id,
                Nome = usuario.Nome,
                Assinaturas = usuario.Assinaturas,
                Playlists = usuario.Playlists,
                BandasFavoritas = usuario.BandasFavoritas,
                CartoesCredito = usuario.Cartoes,
            };

            return dtoResp;
        }






        // CARTOES
        public AdicionarCartaoDtoResp AdicionarCartao(AdicionarCartaoDtoReq dtoReq)
        {
            domain.Usuario usuario = usuarioRepo.ObterUsuarioPorId(dtoReq.IdUsuario);
            if (usuario == null)
            {
                ErroNegocio erroNegocio = new ErroNegocio()
                {
                    MensagemErro = "Usuário não encontrado.",
                    NomeErro = nameof(AdicionarCartao),
                };
                throw new BusinessException(erroNegocio);
            }

            domain.Cartao cartao = this.GerarObjetoCartao(dtoReq.Cartao);

            usuario.AdicionarCartao(cartao);

            this.usuarioRepo.SalvarUsuarioNaBase(usuario);

            AdicionarCartaoDtoResp dtoResp = new AdicionarCartaoDtoResp()
            {
                IdCartao = cartao.Id,
            };

            return dtoResp;
        }





        //PLAYLISTS
        public CriarPlaylistDtoResp CriarPlaylist(CriarPlaylistDtoReq dtoReq)
        {
            domain.Usuario usuario = usuarioRepo.ObterUsuarioPorId(dtoReq.IdUsuario);
            if (usuario == null)
            {
                ErroNegocio erroNegocio = new ErroNegocio()
                {
                    MensagemErro = "Usuário não encontrado.",
                    NomeErro = nameof(CriarPlaylist),
                };
                throw new BusinessException(erroNegocio);
            }

            domain.Playlist playlist = usuario.CriarPlaylist(dtoReq.Nome);

            this.usuarioRepo.SalvarUsuarioNaBase(usuario);

            CriarPlaylistDtoResp dtoResp = new CriarPlaylistDtoResp()
            {
                IdPlaylist = playlist.Id,
            };

            return dtoResp;
        }


        public async Task<FavoritarMusicaDtoResp> FavoritarMusica(FavoritarMusicaDtoReq dtoReq)
        {
            domain.Usuario usuario = usuarioRepo.ObterUsuarioPorId(dtoReq.IdUsuario);
            if (usuario == null)
            {
                ErroNegocio erroNegocio = new ErroNegocio()
                {
                    MensagemErro = "Usuário não encontrado.",
                    NomeErro = nameof(FavoritarMusica),
                };
                throw new BusinessException(erroNegocio);
            }

            domain.Musica musica = await bandaRepo.ObterMusicaPorId(dtoReq.IdMusica);
            if (musica == null)
            {
                ErroNegocio erroNegocio = new ErroNegocio()
                {
                    MensagemErro = "Música não encontrada.",
                    NomeErro = nameof(FavoritarMusica),
                };
                throw new BusinessException(erroNegocio);
            }

            usuario.FavoritarMusica(musica);
            usuarioRepo.SalvarUsuarioNaBase(usuario);

            FavoritarMusicaDtoResp dtoResp = new FavoritarMusicaDtoResp()
            {
                IdUsuario = dtoReq.IdUsuario,
                MusicasFavoritas = usuario.Playlists.Where(pl => pl.Nome == "Favoritas").ToList()[0].Musicas,
            };

            return dtoResp;
        }

        public async void InserirMusicaPlaylist(InserirMusicaPlaylistDtoReq dtoReq)
        {
            domain.Usuario usuario = usuarioRepo.ObterUsuarioPorId(dtoReq.IdUsuario);
            if (usuario == null)
            {
                ErroNegocio erroNegocio = new ErroNegocio()
                {
                    MensagemErro = "Usuário não encontrado.",
                    NomeErro = nameof(InserirMusicaPlaylist),
                };
                throw new BusinessException(erroNegocio);
            }

            domain.Playlist playlist = usuario.ObterPlaylistPorId(dtoReq.IdPlaylist);
            if (playlist == null)
            {
                ErroNegocio erroNegocio = new ErroNegocio()
                {
                    MensagemErro = "Playlist não encontrada.",
                    NomeErro = nameof(InserirMusicaPlaylist),
                };
                throw new BusinessException(erroNegocio);
            }

            domain.Musica musica = await bandaRepo.ObterMusicaPorId(dtoReq.IdMusica);
            if (musica == null)
            {
                ErroNegocio erroNegocio = new ErroNegocio()
                {
                    MensagemErro = "Música não encontrada.",
                    NomeErro = nameof(InserirMusicaPlaylist),
                };
                throw new BusinessException(erroNegocio);
            }

            playlist.AdicionarMusica(musica);
            usuarioRepo.SalvarUsuarioNaBase(usuario);
        }




        // BANDAS
        public async Task<FavoritarBandasDtoResp> FavoritarBanda(FavoritarBandaDtoReq dtoReq)
        {
            domain.Usuario usuario = usuarioRepo.ObterUsuarioPorId(dtoReq.IdUsuario);
            if (usuario == null)
            {
                ErroNegocio erroNegocio = new ErroNegocio()
                {
                    MensagemErro = "Usuário não encontrado.",
                    NomeErro = nameof(FavoritarBanda),
                };
                throw new BusinessException(erroNegocio);
            }

            domain.Banda banda = await bandaRepo.ObterBandaPorId(dtoReq.IdBanda);
            if (banda == null)
            {
                ErroNegocio erroNegocio = new ErroNegocio()
                {
                    MensagemErro = "Banda não encontrada.",
                    NomeErro = nameof(FavoritarBanda),
                };
                throw new BusinessException(erroNegocio);
            }

            usuario.FavoritarBanda(banda);
            usuarioRepo.SalvarUsuarioNaBase(usuario);

            FavoritarBandasDtoResp dtoResp = new FavoritarBandasDtoResp()
            {
                IdUsuario = dtoReq.IdUsuario,
                BandasFavoritas = usuario.BandasFavoritas,
            };

            return dtoResp;
        }


        public BuscarBandasDtoResp BuscarBandas(BuscarBandasDtoReq dtoReq)
        {
            domain.Usuario usuario = usuarioRepo.ObterUsuarioPorId(dtoReq.IdUsuario);
            if (usuario == null)
            {
                ErroNegocio erroNegocio = new ErroNegocio()
                {
                    MensagemErro = "Usuário não encontrado.",
                    NomeErro = nameof(BuscarBandas),
                };
                throw new BusinessException(erroNegocio);
            }

            List<domain.Banda> bandas = usuario.BuscarBanda(dtoReq.Nome);

            BuscarBandasDtoResp dtoResp = new BuscarBandasDtoResp
            {
                Bandas = bandas,
            };

            return dtoResp;
        }


        public BuscarMusicasDtoResp BuscarMusicas(BuscarMusicasDtoReq dtoReq)
        {
            domain.Usuario usuario = usuarioRepo.ObterUsuarioPorId(dtoReq.IdUsuario);
            if (usuario == null)
            {
                ErroNegocio erroNegocio = new ErroNegocio()
                {
                    MensagemErro = "Usuário não encontrado.",
                    NomeErro = nameof(BuscarMusicas),
                };
                throw new BusinessException(erroNegocio);
            }

            List<domain.Musica> musicas = usuario.BuscarMusicas(dtoReq.Nome);

            BuscarMusicasDtoResp dtoResp = new BuscarMusicasDtoResp
            {
                Musicas = musicas,
            };

            return dtoResp;
        }




        // ASSINATURAS
        public async Task<AssinarPlanoDtoResp> AssinarPlano(AssinarPlanoDtoReq dtoReq)
        {
            domain.Plano plano = await planoRepo.ObterPlanoPorId(dtoReq.IdPlano);
            if (plano == null)
            {
                ErroNegocio erroNegocio = new ErroNegocio()
                {
                    MensagemErro = "Plano não encontrado.",
                    NomeErro = nameof(AssinarPlano),
                };
                throw new BusinessException(erroNegocio);
            }

            domain.Usuario usuario = usuarioRepo.ObterUsuarioPorId(dtoReq.IdUsuario);
            if (usuario == null)
            {
                ErroNegocio erroNegocio = new ErroNegocio()
                {
                    MensagemErro = "Usuário não encontrado.",
                    NomeErro = nameof(AssinarPlano),
                };
                throw new BusinessException(erroNegocio);
            }

            domain.Cartao cartao = usuario.BuscarCartaoPorId(dtoReq.IdCartao);
            domain.Empresa empresa = new domain.Empresa();

            domain.Assinatura assinatura = usuario.AssinarPlano(empresa.Cnpj, plano, cartao);

            this.usuarioRepo.SalvarUsuarioNaBase(usuario);

            
            AssinarPlanoDtoResp dtoResp = new AssinarPlanoDtoResp
            {
                IdAssinatura = assinatura.Id,
            };

            return dtoResp;
        }






        // AUX
        public domain.Cartao GerarObjetoCartao(CartaoDto cartaoDto)
        {
            domain.Cartao cartao =  new domain.Cartao() {
                CartaoAtivo = cartaoDto.CartaoAtivo,
                LimiteDisponivel = cartaoDto.LimiteDisponivel,
                Numero = cartaoDto.Numero
            };

            return cartao;
        }

        public domain.Playlist GerarObjetoPlaylist(String nome)
        {
            domain.Playlist playlist = new domain.Playlist()
            {
                Nome = nome
            };

            return playlist;
        }
    }
}
