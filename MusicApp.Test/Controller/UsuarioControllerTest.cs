using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicApp.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicApp.Domain.Conta.Aggregates;
using MusicApp.Repository.Conta;
using MusicApp.Domain.Aplicativo.Aggregates;
using MusicApp.Repository.Aplicativo;
using MusicApp.Application.Conta.Dto;

namespace SpotifyLike.Tests.Controller
{
    public class UsuarioControllerTests
    {

        // USUARIO
        [Fact]
        public void DeveChamarPostCriarUsuarioComSucesso()
        {
            CriarContaDtoRequest dto = new CriarContaDtoRequest()
            {
                Nome = "Lorem Ipsum do teste",
                CartaoCredito = new CartaoCreditoDto()
                {
                    CartaoAtivo = true,
                    LimiteDisponivel = 100,
                    Numero = "5248581002684983"
                },
                PlanoId = new Guid("8D044595-D4A6-4E1A-9F09-DAB92205C71C")
            };

            var logger = LoggerFactory.Create(logger => logger.AddConsole())
                                      .CreateLogger<UsuarioController>();

            var controller = new UsuarioController(logger);

            var response = controller.CriarConta(dto);

            Assert.True(response is CreatedResult);

            var responseContent = (response as CreatedResult).Value;
            Assert.True(responseContent is CriarContaDtoResponse);
            Assert.True((responseContent as CriarContaDtoResponse).IdUsuario != Guid.Empty);
        }

        [Fact]
        public void DeveChamarGetObterUsuarioPorIdComSucesso()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            Usuario usuario = new Usuario();
            usuarioRepository.SalvarUsuarioNaBase(usuario);

            var logger = LoggerFactory.Create(logger => logger.AddConsole())
                                      .CreateLogger<UsuarioController>();

            var controller = new UsuarioController(logger);

            var response = controller.ObterUsuarioPorId(usuario.Id);

            Assert.True(response is OkObjectResult);

            var responseContent = (response as OkObjectResult).Value;
            Assert.True(responseContent is ObterUsuarioPorIdDtoResponse);
            Assert.True((responseContent as ObterUsuarioPorIdDtoResponse).IdUsuario == usuario.Id);
        }


        // PLAYLIST
        [Fact]
        public void DeveChamarPostCriarPlaylistComSucesso()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            Usuario usuario = new Usuario();
            usuarioRepository.SalvarUsuarioNaBase(usuario);

            CriarPlaylistDtoRequest dto = new CriarPlaylistDtoRequest()
            {
                IdUsuario = usuario.Id,
                Nome = "TESTE"
            };

            var logger = LoggerFactory.Create(logger => logger.AddConsole())
                                      .CreateLogger<UsuarioController>();

            var controller = new UsuarioController(logger);

            var response = controller.CriarPlaylist(dto);

            Assert.True(response is CreatedResult);

            var responseContent = (response as CreatedResult).Value;
            Assert.True(responseContent is CriarPlaylistDtoResponse);
            Assert.True((responseContent as CriarPlaylistDtoResponse).IdPlaylist != Guid.Empty);
        }


        // CARTAO
        [Fact]
        public void DeveChamarPostAdicionarCartaoComSucesso()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            Usuario usuario = new Usuario();
            usuarioRepository.SalvarUsuarioNaBase(usuario);

            AdicionarCartaoCreditoDtoRequest dto = new AdicionarCartaoCreditoDtoRequest()
            {
                IdUsuario = usuario.Id,
                CartaoCredito = new CartaoCreditoDto()
                {
                    CartaoAtivo = true,
                    LimiteDisponivel = 100M,
                    Numero = "1"
                }
            };

            var logger = LoggerFactory.Create(logger => logger.AddConsole())
                                      .CreateLogger<UsuarioController>();

            var controller = new UsuarioController(logger);

            var response = controller.AdicionarCartaoCredito(dto);

            Assert.True(response is CreatedResult);

            var responseContent = (response as CreatedResult).Value;
            Assert.True(responseContent is AdicionarCartaoCreditoDtoResponse);
            Assert.True((responseContent as AdicionarCartaoCreditoDtoResponse).IdCartaoCredito != Guid.Empty);
        }


        // ASSINATURA
        [Fact]
        public void DeveChamarPostAssinarPlanoComSucesso()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();

            Usuario usuario = new Usuario();
            CartaoCredito cartao = new CartaoCredito()
            {
                CartaoAtivo = true,
                LimiteDisponivel = 1000M,
                Numero = "1",
            };
            usuario.AdicionarCartaoCredito(cartao);
            usuarioRepository.SalvarUsuarioNaBase(usuario);

            AssinarPlanoDtoRequest contaDto = new AssinarPlanoDtoRequest()
            {
                IdUsuario = usuario.Id,
                IdPlano = new Guid("8D044595-D4A6-4E1A-9F09-DAB92205C71C"),
                IdCartaoCredito = cartao.Id,
            };


            var logger = LoggerFactory.Create(logger => logger.AddConsole())
                                      .CreateLogger<UsuarioController>();
            var controller = new UsuarioController(logger);
            var response = controller.AssinarPlano(contaDto);

            Assert.True(response is OkObjectResult);

            var responseContent = (response as OkObjectResult).Value;
            Assert.True(responseContent is AssinarPlanoDtoResponse);
            Assert.True((responseContent as AssinarPlanoDtoResponse).IdAssinatura != Guid.Empty);

        }


        // BANDA
        [Fact]
        public void DeveChamarPostFavoritarBandaComSucesso()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            Usuario usuario = new Usuario();
            usuarioRepository.SalvarUsuarioNaBase(usuario);

            FavoritarBandaDtoRequest contaDto = new FavoritarBandaDtoRequest
            {
                IdUsuario = usuario.Id,
                IdBanda = new Guid("BE431A65-6715-492A-A22C-4CC54CA9B029"),
            };

            var logger = LoggerFactory.Create(logger => logger.AddConsole())
                                      .CreateLogger<UsuarioController>();
            var controller = new UsuarioController(logger);
            var response = controller.FavoritarBanda(contaDto);

            Assert.True(response is OkObjectResult);
            var responseContent = (response as OkObjectResult).Value;

            Assert.True(responseContent is FavoritarBandasDtoResponse);
            Assert.True((responseContent as FavoritarBandasDtoResponse).BandasFavoritas.Count > 0);

        }

        [Fact]
        public void DeveChamarGetObterBandasComSucesso()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            BandaRepository bandaRepository = new BandaRepository();

            Usuario usuario = new Usuario();
            Banda banda = bandaRepository.ObterBandaPorId(new Guid("BE431A65-6715-492A-A22C-4CC54CA9B029"));

            usuario.FavoritarBanda(banda);
            usuarioRepository.SalvarUsuarioNaBase(usuario);

            var logger = LoggerFactory.Create(logger => logger.AddConsole())
                                      .CreateLogger<UsuarioController>();
            var controller = new UsuarioController(logger);
            var response = controller.ObterBandas(usuario.Id, "que");

            Assert.True(response is OkObjectResult);
            var responseContent = (response as OkObjectResult).Value;

            Assert.True(responseContent is ObterBandasDtoResponse);
            Assert.True((responseContent as ObterBandasDtoResponse).Bandas.Count > 0);

        }
    }
}
