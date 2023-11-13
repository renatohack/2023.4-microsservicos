using MusicApp.Application.Conta.Dto;
using MusicApp.Application.Conta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicApp.Repository.Conta;
using SpotifyLike.Core.Exception;
using MusicApp.Domain.Aplicativo.Exception;

namespace MusicApp.Test.Application.Conta
{
    public class UsuarioServiceTest
    {

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
                CartaoCredito = new CriarContaDto.CartaoCreditoDto() 
                {
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


    }
}
