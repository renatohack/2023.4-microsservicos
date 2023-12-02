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
using MusicApp.Application.Aplicativo;

namespace MusicApp.Application.Conta
{
    public class UsuarioService
    {

        private UsuarioRepository usuarioRepository = new UsuarioRepository();
        private PlanoService planoService = new PlanoService();


        // FUNCIONALIDADES
        public UsuarioDto CriarConta(UsuarioDto contaDto)
        {

            // Pega plano no banco a partir do ID passado dentro da classe DTO
            Plano plano = planoService.ObterPlanoPorId(contaDto.PlanoId);

            // Gera um objeto cartão a partir da classe DTO, para ser usado na criação do usuário
            CartaoCredito cartao = GerarObjetoCartaoCredito(contaDto);

            // Cria usuario, passando cartao criado e plano retornado
            Usuario usuarioCriado = Usuario.CriarUsuario(contaDto.Nome, cartao, plano);

            // Gravar novo usuarioCriado na base
            this.usuarioRepository.SalvarUsuarioNaBase(usuarioCriado);
            contaDto.IdUsuario = usuarioCriado.Id;

            // Retornar Conta DTO, com o ID atualizado
            return contaDto;
            
        }


        public UsuarioDto AdicionarCartaoCredito(UsuarioDto contaDto)
        {
            Usuario usuario = ObterUsuarioPorId(contaDto.IdUsuario);

            CartaoCredito cartao = GerarObjetoCartaoCredito(contaDto);

            usuario.AdicionarCartaoCredito(cartao);
            
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
        public CartaoCredito GerarObjetoCartaoCredito(UsuarioDto contaDto)
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
