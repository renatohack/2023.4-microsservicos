﻿using MusicApp.Application.Conta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicApp.Repository.Conta;
using MusicApp.Core.Exception;
using MusicApp.Domain.Aplicativo.Exception;
using MusicApp.Domain.Aplicativo.Aggregates;
using MusicApp.Domain.Conta.Aggregates;
using MusicApp.Repository.Aplicativo;
using MusicApp.Application.Conta.Dto;


namespace MusicApp.Test.Application.Conta
{
    public class UsuarioServiceTest
    {

        // USUARIO
        [Fact]
        public void CriarUsuarioComSucesso()
        {

            UsuarioRepository usuarioRepository = new UsuarioRepository();

            CriarContaDtoRequest contaDto = new CriarContaDtoRequest() {
                Nome = "Dummy",
                PlanoId = new Guid("8D044595-D4A6-4E1A-9F09-DAB92205C71C"),
                CartaoCredito = new CartaoCreditoDto() {
                    Numero = "123",
                    CartaoAtivo = true,
                    LimiteDisponivel = 50M
                }
            };


            UsuarioService usuarioService = new UsuarioService();
            CriarContaDtoResponse contaDtoResponse = usuarioService.CriarConta(contaDto);

            Assert.True(contaDtoResponse.IdUsuario.ToString() != Guid.Empty.ToString());
        }


        [Fact]
        public void NaoDeveCriarUsuarioPlanoInexistente()
        {

            CriarContaDtoRequest contaDto = new CriarContaDtoRequest() {
                Nome = "Dummy",
                PlanoId = new Guid("8D044595-D4A6-4E1A-9F09-DAB92205C711"),
                CartaoCredito = new CartaoCreditoDto() {
                    Numero = "123",
                    CartaoAtivo = true,
                    LimiteDisponivel = 50M
                }
            };

            
            UsuarioService usuarioService = new UsuarioService();
            Assert.Throws<BusinessException>(() => usuarioService.CriarConta(contaDto));

        }


        [Fact]
        public void NaoDeveCriarUsuarioCartaoInativo()
        {

            CriarContaDtoRequest contaDto = new CriarContaDtoRequest() {
                Nome = "Dummy",
                PlanoId = new Guid("8D044595-D4A6-4E1A-9F09-DAB92205C71C"),
                CartaoCredito = new CartaoCreditoDto() {
                    Numero = "123",
                    CartaoAtivo = false,
                    LimiteDisponivel = 50M
                }
            };


            UsuarioService usuarioService = new UsuarioService();
            Assert.Throws<CartaoCreditoException>(() => usuarioService.CriarConta(contaDto));

        }


        [Fact]
        public void NaoDeveCriarUsuarioLimiteInsuficiente()
        {

            CriarContaDtoRequest contaDto = new CriarContaDtoRequest() {
                Nome = "Dummy",
                PlanoId = new Guid("8D044595-D4A6-4E1A-9F09-DAB92205C71C"),
                CartaoCredito = new CartaoCreditoDto() {
                    Numero = "123",
                    CartaoAtivo = true,
                    LimiteDisponivel = 10M
                }
            };


            UsuarioService usuarioService = new UsuarioService();
            Assert.Throws<CartaoCreditoException>(() => usuarioService.CriarConta(contaDto));

        }


        [Fact]
        public void DeveRetornarUsuarioComSucesso()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            Usuario usuario = new Usuario();
            usuarioRepository.SalvarUsuarioNaBase(usuario);

            UsuarioService usuarioService = new UsuarioService();
            ObterUsuarioPorIdDtoResponse usuarioResponse = usuarioService.ObterUsuarioPorId(usuario.Id);

            Assert.True(usuarioResponse.IdUsuario == usuario.Id);
        }



        // PLAYLIST
        [Fact]
        public void CriarPlaylistComSucesso()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            Usuario usuario = new Usuario();
            usuarioRepository.SalvarUsuarioNaBase(usuario);

            CriarPlaylistDtoRequest playlistDtoRequest = new CriarPlaylistDtoRequest()
            {
                IdUsuario = usuario.Id,
                Nome = "TESTE"
            };


            UsuarioService usuarioService = new UsuarioService();
            CriarPlaylistDtoResponse playlistDtoResponse = usuarioService.CriarPlaylist(playlistDtoRequest);


            Assert.True(playlistDtoResponse.IdPlaylist.ToString() != Guid.Empty.ToString());
            Assert.True(usuario.Playlists.Select(pl => pl.Nome).Where(nome => nome == "TESTE").Count() == 1);
        }



        // CARTAO
        [Fact]
        public void DeveAdicionarCartaoCreditoComSucesso()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            Usuario usuario = new Usuario();
            usuarioRepository.SalvarUsuarioNaBase(usuario);

            AdicionarCartaoCreditoDtoRequest contaDto = new AdicionarCartaoCreditoDtoRequest()
            {
                IdUsuario = usuario.Id,
                CartaoCredito = new CartaoCreditoDto()
                {
                    CartaoAtivo = true,
                    LimiteDisponivel = 1M,
                    Numero = "1"
                }
            };

            UsuarioService usuarioService = new UsuarioService();
            usuarioService.AdicionarCartaoCredito(contaDto);

            Assert.True(usuario.Cartoes.Count == 1);
        }



        // ASSINATURA
        [Fact]
        public void DeveAssinarPlanoComSucesso()
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

            UsuarioService usuarioService = new UsuarioService();
            AssinarPlanoDtoResponse contaDtoResponse = usuarioService.AssinarPlano(contaDto);


            Assert.True(usuario.Assinaturas.Count == 1);
            Assert.True(usuario.Assinaturas.Last().AssinaturaAtiva);
        }


        // BANDAS
        [Fact]
        public void DeveFavoritarBandaComSucesso()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            Usuario usuario = new Usuario();
            usuarioRepository.SalvarUsuarioNaBase(usuario);

            int counterInicio = usuario.BandasFavoritas.Count;

            FavoritarBandaDtoRequest contaDto = new FavoritarBandaDtoRequest
            {
                IdUsuario = usuario.Id,
                IdBanda = new Guid("BE431A65-6715-492A-A22C-4CC54CA9B029"),
            };


            UsuarioService usuarioService = new UsuarioService();
            usuarioService.FavoritarBanda(contaDto);

            Assert.True(usuario.BandasFavoritas.Count - counterInicio == 1);

        }

        [Fact]
        public void DeveBuscarBandaComSucesso()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            BandaRepository bandaRepository = new BandaRepository();

            Usuario usuario = new Usuario();
            Banda banda = bandaRepository.ObterBandaPorId(new Guid("BE431A65-6715-492A-A22C-4CC54CA9B029"));

            usuario.FavoritarBanda(banda);
            usuarioRepository.SalvarUsuarioNaBase(usuario);

            ObterBandasDtoRequest bandaDtoRequest = new ObterBandasDtoRequest
            {
                IdUsuario = usuario.Id,
                Nome = "que"
            };

            UsuarioService usuarioService = new UsuarioService();
            ObterBandasDtoResponse bandaDtoResponse = usuarioService.ObterBandas(bandaDtoRequest);

            Assert.True(bandaDtoResponse.Bandas.FirstOrDefault(b => b.Nome.ToLower() == "queen") != null);

        }
    }
}
