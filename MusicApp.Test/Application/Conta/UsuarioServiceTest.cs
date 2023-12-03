using MusicApp.Application.Conta.Dto;
using MusicApp.Application.Conta;
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

            Assert.True(contaDto.IdUsuario.ToString() == Guid.Empty.ToString());

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
            ObterUsuarioPorIdResponse usuarioResponse = usuarioService.ObterUsuarioPorId(usuario.Id);

            Assert.True(usuarioResponse.IdUsuario == usuario.Id);
        }



        // PLAYLIST
        [Fact]
        public void CriarPlaylistComSucesso()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            Usuario usuario = new Usuario();
            usuarioRepository.SalvarUsuarioNaBase(usuario);

            CriarPlaylistDto playlistDto = new CriarPlaylistDto()
            {
                IdUsuario = usuario.Id,
                Nome = "TESTE"
            };


            Assert.True(playlistDto.IdPlaylist.ToString() == Guid.Empty.ToString());

            UsuarioService usuarioService = new UsuarioService();
            playlistDto = usuarioService.CriarPlaylist(playlistDto);


            Assert.True(playlistDto.IdPlaylist.ToString() != Guid.Empty.ToString());
            Assert.True(usuario.Playlists.Select(pl => pl.Nome).Where(nome => nome == "TESTE").Count() == 1);
        }



        // CARTAO
        [Fact]
        public void DeveAdicionarCartaoCreditoComSucesso()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            Usuario usuario = new Usuario();
            usuarioRepository.SalvarUsuarioNaBase(usuario);

            UsuarioDto contaDto = new UsuarioDto()
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

            UsuarioService usuarioService = new UsuarioService();
            usuarioService.AssinarPlano(contaDto);


            Assert.True(usuario.Assinaturas.Count == 1);
            Assert.True(usuario.Assinaturas.Last().AssinaturaAtiva);
        }




    }
}
