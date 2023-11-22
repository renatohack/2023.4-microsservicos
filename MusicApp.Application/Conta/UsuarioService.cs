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
using MusicApp.Core.Exception;
using MusicApp.Domain.Conta.Aggregates;

namespace MusicApp.Application.Conta
{
    public class UsuarioService
    {

        private PlanoRepository planoRepository = new PlanoRepository();
        private UsuarioRepository usuarioRepository = new UsuarioRepository();




        // FUNCIONALIDADES
        public CriarContaDto CriarConta(CriarContaDto contaDto)
        {

            // Pega plano no banco a partir do ID passado dentro da classe DTO
            Plano plano = ObterPlanoPorId(contaDto.PlanoId);

            // Gera um objeto cartão a partir da classe DTO, para ser usado na criação do usuário
            CartaoCredito cartao = GerarObjetoCartaoCredito(contaDto);

            // Cria usuario, passando cartao criado e plano retornado
            Usuario usuarioCriado = Usuario.CriarUsuario(contaDto.Nome, cartao, plano);

            // Gravar novo usuarioCriado na base
            this.usuarioRepository.SalvarUsuarioNaBase(usuarioCriado);
            contaDto.Id = usuarioCriado.Id;

            // Retornar Conta DTO, com o ID atualizado
            return contaDto;
            
        }


        public CriarPlaylistDto CriarPlaylist(CriarPlaylistDto playlistDto)
        {
            Usuario usuario = ObterUsuarioPorId(playlistDto.IdUsuario);

            Playlist playlist = usuario.CriarPlaylist(playlistDto.Nome);
            playlistDto.IdPlaylist = playlist.Id;

            return playlistDto;
        }





        // RECUPERAR DO BANCO
        public Plano ObterPlanoPorId(Guid id)
        {
            Plano plano = this.planoRepository.ObterPlanoPorId(id);

            if (plano == null)
            {
                ErroNegocio erroNegocio = new ErroNegocio() {
                    MensagemErro = "Plano não encontrado.",
                    NomeErro = nameof(CriarConta),
                };
                throw new BusinessException(erroNegocio);
            }

            return plano;
        }

        public Usuario ObterUsuarioPorId(Guid idUsuario)
        {
            Usuario usuario = usuarioRepository.ObterUsuarioPorId(idUsuario);

            if (usuario == null)
            {
                ErroNegocio erroNegocio = new ErroNegocio()
                {
                    MensagemErro = "Usuário não encontrado.",
                    NomeErro = nameof(CriarPlaylist),
                };
                throw new BusinessException(erroNegocio);
            }

            return usuario;
        }





        // AUX
        public CartaoCredito GerarObjetoCartaoCredito(CriarContaDto contaDto)
        {
            CartaoCredito cartao =  new CartaoCredito() {
                CartaoAtivo = contaDto.CartaoCredito.CartaoAtivo,
                LimiteDisponivel = contaDto.CartaoCredito.LimiteDisponivel,
                Numero = contaDto.CartaoCredito.Numero
            };

            return cartao;
        }

        public Playlist GerarObjetoPlaylist(String nome)
        {
            Playlist playlist = new Playlist()
            {
                Nome = nome
            };

            return playlist;
        }
    }
}
