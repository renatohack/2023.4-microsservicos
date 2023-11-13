using MusicApp.Domain.Aplicativo.Aggregates;
using MusicApp.Domain.Aplicativo.Exception;
using MusicApp.Domain.Conta.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Test.Domain.Conta
{
    public class CartaoTest
    {

        [Fact]
        public void DeveRealizarTransacaoComSucesso()
        {
            CartaoCredito cartao = new CartaoCredito() {
                CartaoAtivo = true,
                LimiteDisponivel = 500M,
                Numero = "123"
            };

            cartao.RealizarTransacao("123", 250M);

            Assert.True(cartao.Transacoes.Count == 1);
            Assert.True(cartao.Transacoes[0].Comerciante.Cnpj == "123");

        }


        [Fact]
        public void DeveInterromperTransacaoCartaoInativo()
        {
            CartaoCredito cartao = new CartaoCredito() {
                CartaoAtivo = false,
                LimiteDisponivel = 500M,
                Numero = "123"
            };

            Assert.Throws<CartaoCreditoException>(() => cartao.RealizarTransacao("123", 100M));

        }


        [Fact]
        public void DeveInterromperTransacaoSemLimiteDisponivel()
        {
            CartaoCredito cartao = new CartaoCredito() {
                CartaoAtivo = true,
                LimiteDisponivel = 500M,
                Numero = "123"
            };

            Assert.Throws<CartaoCreditoException>(() => cartao.RealizarTransacao("123", 600M));

        }


        [Fact]
        public void DeveInterromperTransacaoAltaFrequencia()
        {
            CartaoCredito cartao = new CartaoCredito() {
                CartaoAtivo = true,
                LimiteDisponivel = 500M,
                Numero = "123"
            };

            cartao.RealizarTransacao("123", 250M);
            cartao.RealizarTransacao("124", 250M);
            cartao.RealizarTransacao("125", 250M);

            Assert.Throws<CartaoCreditoException>(() => cartao.RealizarTransacao("123", 250M));

        }

        [Fact]
        public void DeveInterromperTransacaoDuplicada()
        {
            CartaoCredito cartao = new CartaoCredito() {
                CartaoAtivo = true,
                LimiteDisponivel = 500M,
                Numero = "123"
            };

            cartao.RealizarTransacao("123", 250M);

            Assert.Throws<CartaoCreditoException>(() => cartao.RealizarTransacao("123", 250M));

        }

    }
}
