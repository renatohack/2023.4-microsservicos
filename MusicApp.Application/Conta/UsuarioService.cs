using MusicApp.Application.Conta.Dto;
using MusicApp.Repository.Aplicativo;
using MusicApp.Repository.Conta;
using MusicApp.Domain.Aplicativo.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using SpotifyLike.Core.Exception;
using MusicApp.Domain.Conta.Aggregates;

namespace MusicApp.Application.Conta
{
    internal class UsuarioService
    {

        private PlanoRepository planoRepository = new PlanoRepository();
        private UsuarioRepository usuarioRepository = new UsuarioRepository();

        public CriarContaDto CriarConta(CriarContaDto conta)
        {
            // Pega plano no banco a partir do ID passado dentro da classe DTO
            Plano plano = this.planoRepository.ObterPlanoPorId(conta.PlanoId);

            if (plano == null)
            {
                ErroNegocio erroNegocio = new ErroNegocio() {
                    MensagemErro = "Plano não encontrado.",
                    NomeErro = nameof(CriarConta),
                };
                throw new BusinessException(erroNegocio);
            }


            // Gera um objeto cartão a partir da classe DTO
            CartaoCredito cartao = new CartaoCredito() {
                CartaoAtivo = conta.CartaoCredito.CartaoAtivo,
                LimiteDisponivel = conta.CartaoCredito.LimiteDisponivel,
                Numero = conta.CartaoCredito.Numero
            };


            // Cria usuario, passando cartao criado e plano retornado
            Usuario usuario = Usuario.CriarUsuario(conta.Nome, cartao, plano);

            // Gravar novo usuario na base
            this.usuarioRepository.SalvarUsuarioNaBase(usuario);
            conta.Id = usuario.Id;

            // Retornar Conta DTO, com o ID atualizado
            return conta;
            
        }

    }
}
