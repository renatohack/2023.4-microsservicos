using MusicApp.Domain.Aplicativo.Aggregates;
using MusicApp.Domain.Aplicativo.Exception;
using MusicApp.Domain.Conta.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Test.Domain.Conta
{
    public class UsuarioTest
    {

        [Fact]
        public void DeveCriarUsuarioComSucesso()
        {
            CartaoCredito cartao = new CartaoCredito() {
                CartaoAtivo = true,
                LimiteDisponivel = 500M,
                Numero = "123"
            };

            Plano plano = new Plano() {
                Nome = "basico",
                Valor = 250M
            };

            Usuario usuario = Usuario.CriarUsuario("Dummy", cartao, plano);

            Assert.NotNull(usuario);

            Assert.True(!String.IsNullOrEmpty(usuario.Nome));

            Assert.True(usuario.Cartoes != null);
            Assert.True(usuario.Cartoes.Any());
            Assert.True(usuario.Cartoes.Count == 1);
            Assert.True(usuario.Cartoes[0].CartaoAtivo);

            Assert.True(usuario.Assinaturas != null);
            Assert.True(usuario.Assinaturas.Any());
            Assert.True(usuario.Assinaturas.Count == 1);
            Assert.True(usuario.Assinaturas[0].AssinaturaAtiva);

            Assert.True(usuario.Playlists != null);
            Assert.True(usuario.Playlists.Any());
            Assert.True(usuario.Playlists.Count == 1);
            Assert.True(usuario.Playlists[0].Nome == "Favoritas");

            Assert.True(usuario.BandasFavoritas != null);

        }

        [Fact]
        public void NaoDeveCriarUsuarioComCartaoInativo()
        {
            CartaoCredito cartao = new CartaoCredito() {
                CartaoAtivo = false,
                LimiteDisponivel = 500M,
                Numero = "123"
            };

            Plano plano = new Plano() {
                Nome = "basico",
                Valor = 250M
            };

            Assert.Throws<CartaoCreditoException>(() => Usuario.CriarUsuario("Dummy", cartao, plano));
        }

        [Fact]
        public void NaoDeveCriarUsuarioComLimiteInsuficiente()
        {
            CartaoCredito cartao = new CartaoCredito() {
                CartaoAtivo = false,
                LimiteDisponivel = 125M,
                Numero = "123"
            };

            Plano plano = new Plano() {
                Nome = "basico",
                Valor = 250M
            };

            Assert.Throws<CartaoCreditoException>(() => Usuario.CriarUsuario("Dummy", cartao, plano));
        }

    }
}
