using MusicApp.Usuario.Application;
using MusicApp.Usuario.Repository;
using MusicApp.Core.Exception;
using domain = MusicApp.Usuario.Domain.Aggregates;
using MusicApp.Usuario.Application.Dto;
using MusicApp.Usuario.Domain.Exception;

namespace MusicApp.Test.Application.Conta
{
    public class UsuarioServiceTest
    {

        // USUARIO
        [Fact]
        public async void CriarUsuarioComSucesso()
        {

            UsuarioRepository usuarioRepository = new UsuarioRepository();

            CriarContaDtoReq contaDto = new CriarContaDtoReq() {
                Nome = "Dummy",
                PlanoId = new Guid("8D044595-D4A6-4E1A-9F09-DAB92205C71C"),
                Cartao = new CartaoDto() {
                    Numero = "123",
                    CartaoAtivo = true,
                    LimiteDisponivel = 50M
                }
            };


            UsuarioService usuarioService = new UsuarioService();
            CriarContaDtoResp contaDtoResponse = await usuarioService.CriarConta(contaDto);

            Assert.True(contaDtoResponse.IdUsuario.ToString() != Guid.Empty.ToString());
        }


        [Fact]
        public void NaoDeveCriarUsuarioPlanoInexistente()
        {

            CriarContaDtoReq contaDto = new CriarContaDtoReq() {
                Nome = "Dummy",
                PlanoId = new Guid("8D044595-D4A6-4E1A-9F09-DAB92205C711"),
                Cartao = new CartaoDto() {
                    Numero = "123",
                    CartaoAtivo = true,
                    LimiteDisponivel = 50M
                }
            };

            
            UsuarioService usuarioService = new UsuarioService();
            Assert.ThrowsAsync<BusinessException>(() => usuarioService.CriarConta(contaDto));

        }


        [Fact]
        public void NaoDeveCriarUsuarioCartaoInativo()
        {

            CriarContaDtoReq contaDto = new CriarContaDtoReq() {
                Nome = "Dummy",
                PlanoId = new Guid("8D044595-D4A6-4E1A-9F09-DAB92205C71C"),
                Cartao = new CartaoDto() {
                    Numero = "123",
                    CartaoAtivo = false,
                    LimiteDisponivel = 50M
                }
            };


            UsuarioService usuarioService = new UsuarioService();
            Assert.ThrowsAsync<CartaoException>(() => usuarioService.CriarConta(contaDto));

        }


        [Fact]
        public void NaoDeveCriarUsuarioLimiteInsuficiente()
        {

            CriarContaDtoReq contaDto = new CriarContaDtoReq() {
                Nome = "Dummy",
                PlanoId = new Guid("8D044595-D4A6-4E1A-9F09-DAB92205C71C"),
                Cartao = new CartaoDto() {
                    Numero = "123",
                    CartaoAtivo = true,
                    LimiteDisponivel = 10M
                }
            };


            UsuarioService usuarioService = new UsuarioService();
            Assert.ThrowsAsync<CartaoException>(() => usuarioService.CriarConta(contaDto));

        }


        [Fact]
        public void DeveRetornarUsuarioComSucesso()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            domain.Usuario usuario = new domain.Usuario();
            usuarioRepository.SalvarUsuarioNaBase(usuario);

            UsuarioService usuarioService = new UsuarioService();
            ObterUsuarioPorIdDtoResp usuarioResponse = usuarioService.ObterUsuarioPorId(usuario.Id);

            Assert.True(usuarioResponse.IdUsuario == usuario.Id);
        }



        // PLAYLIST
        [Fact]
        public void CriarPlaylistComSucesso()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            domain.Usuario usuario = new domain.Usuario();
            usuarioRepository.SalvarUsuarioNaBase(usuario);

            CriarPlaylistDtoReq playlistDtoRequest = new CriarPlaylistDtoReq()
            {
                IdUsuario = usuario.Id,
                Nome = "TESTE"
            };


            UsuarioService usuarioService = new UsuarioService();
            CriarPlaylistDtoResp playlistDtoResponse = usuarioService.CriarPlaylist(playlistDtoRequest);


            Assert.True(playlistDtoResponse.IdPlaylist.ToString() != Guid.Empty.ToString());
            Assert.True(usuario.Playlists.Select(pl => pl.Nome).Where(nome => nome == "TESTE").Count() == 1);
        }



        // CARTAO
        [Fact]
        public void DeveAdicionarCartaoComSucesso()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            domain.Usuario usuario = new domain.Usuario();
            usuarioRepository.SalvarUsuarioNaBase(usuario);

            AdicionarCartaoDtoReq contaDto = new AdicionarCartaoDtoReq()
            {
                IdUsuario = usuario.Id,
                Cartao = new CartaoDto()
                {
                    CartaoAtivo = true,
                    LimiteDisponivel = 1M,
                    Numero = "1"
                }
            };

            UsuarioService usuarioService = new UsuarioService();
            usuarioService.AdicionarCartao(contaDto);

            Assert.True(usuario.Cartoes.Count == 1);
        }



        // ASSINATURA
        [Fact]
        public async void DeveAssinarPlanoComSucesso()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();

            domain.Usuario usuario = new domain.Usuario();
            domain.Cartao cartao = new domain.Cartao()
            {
                CartaoAtivo = true,
                LimiteDisponivel = 1000M,
                Numero = "1",
            };
            usuario.AdicionarCartao(cartao);
            usuarioRepository.SalvarUsuarioNaBase(usuario);

            AssinarPlanoDtoReq contaDto = new AssinarPlanoDtoReq()
            {
                IdUsuario = usuario.Id,
                IdPlano = new Guid("8D044595-D4A6-4E1A-9F09-DAB92205C71C"),
                IdCartao = cartao.Id,
            };

            UsuarioService usuarioService = new UsuarioService();
            AssinarPlanoDtoResp contaDtoResponse = await usuarioService.AssinarPlano(contaDto);


            Assert.True(usuario.Assinaturas.Count == 1);
            Assert.True(usuario.Assinaturas.Last().AssinaturaAtiva);
        }


        // BANDAS
        [Fact]
        public async void DeveFavoritarBandaComSucesso()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            domain.Usuario usuario = new domain.Usuario();
            usuarioRepository.SalvarUsuarioNaBase(usuario);

            int counterInicio = usuario.BandasFavoritas.Count;

            FavoritarBandaDtoReq contaDto = new FavoritarBandaDtoReq()
            {
                IdUsuario = usuario.Id,
                IdBanda = new Guid("BE431A65-6715-492A-A22C-4CC54CA9B029"),
            };


            UsuarioService usuarioService = new UsuarioService();
            await usuarioService.FavoritarBanda(contaDto);

            Assert.True(usuario.BandasFavoritas.Count - counterInicio == 1);

        }

        [Fact]
        public async void DeveBuscarBandaComSucesso()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            BandaRepository bandaRepository = new BandaRepository();

            domain.Usuario usuario = new domain.Usuario();
            domain.Banda banda = await bandaRepository.ObterBandaPorId(new Guid("BE431A65-6715-492A-A22C-4CC54CA9B029"));

            usuario.FavoritarBanda(banda);
            usuarioRepository.SalvarUsuarioNaBase(usuario);

            BuscarBandasDtoReq bandaDtoRequest = new BuscarBandasDtoReq
            {
                IdUsuario = usuario.Id,
                Nome = "que"
            };

            UsuarioService usuarioService = new UsuarioService();
            BuscarBandasDtoResp bandaDtoResponse = usuarioService.BuscarBandas(bandaDtoRequest);

            Assert.True(bandaDtoResponse.Bandas.FirstOrDefault(b => b.Nome.ToLower() == "queen") != null);

        }


        /*
        [Fact]
        public async void DeveFavoritarMusicaComSucesso()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            BandaRepository bandaRepository = new BandaRepository();

            domain.Usuario usuario = new domain.Usuario();
            domain.Musica banda = await bandaRepository.ObterMusicaPorId(new Guid("BE431A65-6715-492A-A22C-4CC54CA9B029"));

            usuario.FavoritarBanda(banda);
            usuarioRepository.SalvarUsuarioNaBase(usuario);

            BuscarBandasDtoReq bandaDtoRequest = new BuscarBandasDtoReq
            {
                IdUsuario = usuario.Id,
                Nome = "que"
            };

            UsuarioService usuarioService = new UsuarioService();
            BuscarBandasDtoResp bandaDtoResponse = usuarioService.BuscarBandas(bandaDtoRequest);

            Assert.True(bandaDtoResponse.Bandas.FirstOrDefault(b => b.Nome.ToLower() == "queen") != null);
        }
        */

    }
}
