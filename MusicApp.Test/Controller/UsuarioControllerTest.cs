using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicApp.Application.Conta.Dto;
using MusicApp.API.Controllers;
using MusicApp.Application.Conta.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MusicApp.Application.Conta.Dto.UsuarioDto;
using MusicApp.Domain.Conta.Aggregates;
using MusicApp.Repository.Conta;

namespace SpotifyLike.Tests.Controller
{
    public class UsuarioControllerTests
    {
        [Fact]
        public void DeveChamarPostCriarUsuarioComSucesso()
        {
            UsuarioDto dto = new UsuarioDto()
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
            Assert.True(responseContent is UsuarioDto);
            Assert.True((responseContent as UsuarioDto).IdUsuario != Guid.Empty);
        }


        [Fact]
        public void DeveChamarPostCriarPlaylistComSucesso()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            Usuario usuario = new Usuario();
            usuarioRepository.SalvarUsuarioNaBase(usuario);

            CriarPlaylistDto dto = new CriarPlaylistDto()
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
            Assert.True(responseContent is CriarPlaylistDto);
            Assert.True((responseContent as CriarPlaylistDto).IdPlaylist != Guid.Empty);
        }


        [Fact]
        public void DeveChamarPostAdicionarCartaoComSucesso()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            Usuario usuario = new Usuario();
            usuarioRepository.SalvarUsuarioNaBase(usuario);

            UsuarioDto dto = new UsuarioDto()
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
            Assert.True(responseContent is UsuarioDto);
            Assert.True((responseContent as UsuarioDto).CartaoCredito.IdCartaoCredito != Guid.Empty);
        }


        [Fact]
        public void DeveChamarPostAssinarPlanoComSucesso()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();

            Usuario usuario = new Usuario();
            usuarioRepository.SalvarUsuarioNaBase(usuario);

            UsuarioDto contaDto = new UsuarioDto()
            {
                IdUsuario = usuario.Id,
                PlanoId = new Guid("8D044595-D4A6-4E1A-9F09-DAB92205C71C"),
                CartaoCredito = new CartaoCreditoDto()
                {
                    CartaoAtivo = true,
                    LimiteDisponivel = 1000M,
                    Numero = "1"
                },
            };


            var logger = LoggerFactory.Create(logger => logger.AddConsole())
                                      .CreateLogger<UsuarioController>();

            var controller = new UsuarioController(logger);

            var response = controller.AssinarPlano(contaDto);

            Assert.True(response is CreatedResult);

            var responseContent = (response as CreatedResult).Value;
            Assert.True(responseContent is UsuarioDto);
            Assert.True((responseContent as UsuarioDto).Assinaturas.Count == 1);
            Assert.True((responseContent as UsuarioDto).Assinaturas.Last().AssinaturaAtiva);
            Assert.True((responseContent as UsuarioDto).Assinaturas.Last().Id != Guid.Empty);

        }



    }
}
