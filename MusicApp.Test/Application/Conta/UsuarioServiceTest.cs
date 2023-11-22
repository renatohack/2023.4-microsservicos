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
using MusicApp.Domain.Conta.Aggregates;

namespace MusicApp.Test.Application.Conta
{
    public class UsuarioServiceTest
    {

        // CRIAR USUARIO

        [Fact]
        public void CriarUsuarioComSucesso()
        {

            UsuarioRepository usuarioRepository = new UsuarioRepository();

            CriarContaDto contaDto = new CriarContaDto() {
                Nome = "Dummy",
                PlanoId = new Guid("8D044595-D4A6-4E1A-9F09-DAB92205C71C"),
                CartaoCredito = new CriarContaDto.CartaoCreditoDto() {
                    Numero = "123",
                    CartaoAtivo = true,
                    LimiteDisponivel = 50M
                }
            };

            Assert.True(contaDto.Id.ToString() == Guid.Empty.ToString());

            UsuarioService usuarioService = new UsuarioService();
            contaDto = usuarioService.CriarConta(contaDto);

            Assert.True(contaDto.Id.ToString() != Guid.Empty.ToString());
            Assert.True(usuarioRepository.RetornarNumeroUsuarioNaBase() == 1);

        }


        [Fact]
        public void NaoDeveCriarUsuarioPlanoInexistente()
        {

            CriarContaDto contaDto = new CriarContaDto() {
                Nome = "Dummy",
                PlanoId = new Guid("8D044595-D4A6-4E1A-9F09-DAB92205C711"),
                CartaoCredito = new CriarContaDto.CartaoCreditoDto() {
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

            CriarContaDto contaDto = new CriarContaDto() {
                Nome = "Dummy",
                PlanoId = new Guid("8D044595-D4A6-4E1A-9F09-DAB92205C71C"),
                CartaoCredito = new CriarContaDto.CartaoCreditoDto() {
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

            CriarContaDto contaDto = new CriarContaDto() {
                Nome = "Dummy",
                PlanoId = new Guid("8D044595-D4A6-4E1A-9F09-DAB92205C71C"),
                CartaoCredito = new CriarContaDto.CartaoCreditoDto() {
                    Numero = "123",
                    CartaoAtivo = true,
                    LimiteDisponivel = 10M
                }
            };


            UsuarioService usuarioService = new UsuarioService();
            Assert.Throws<CartaoCreditoException>(() => usuarioService.CriarConta(contaDto));

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





    }
}
