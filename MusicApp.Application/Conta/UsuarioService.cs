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


        // USUARIO
        public CriarContaDtoResponse CriarConta(CriarContaDtoRequest contaDto)
        {

            // Pega plano no banco a partir do ID passado dentro da classe DTO
            Plano plano = planoService.ObterPlanoPorId(contaDto.PlanoId);

            // Gera um objeto cartão a partir da classe DTO, para ser usado na criação do usuário
            CartaoCredito cartao = GerarObjetoCartaoCredito(contaDto.CartaoCredito);

            // Cria usuario, passando cartao criado e plano retornado
            Usuario usuarioCriado = Usuario.CriarUsuario(contaDto.Nome, cartao, plano);

            // Gravar novo usuarioCriado na base
            this.usuarioRepository.SalvarUsuarioNaBase(usuarioCriado);

            // Retornar Conta DTO, com o ID atualizado
            CriarContaDtoResponse contaDtoResponse = new CriarContaDtoResponse()
            {
                IdUsuario = usuarioCriado.Id,
            };
            return contaDtoResponse;
            
        }

        public ObterUsuarioPorIdDtoResponse ObterUsuarioPorId(Guid idUsuario)
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

            ObterUsuarioPorIdDtoResponse usuarioResponse = new ObterUsuarioPorIdDtoResponse()
            {
                IdUsuario = usuario.Id,
                Nome = usuario.Nome,
                Assinaturas = usuario.Assinaturas,
                Playlists = usuario.Playlists,
                BandasFavoritas = usuario.BandasFavoritas,
            };

            return usuarioResponse;
        }






        // CARTOES
        public AdicionarCartaoCreditoDtoResponse AdicionarCartaoCredito(AdicionarCartaoCreditoDtoRequest contaDto)
        {
            Usuario usuario = usuarioRepository.ObterUsuarioPorId(contaDto.IdUsuario);

            CartaoCredito cartao = this.GerarObjetoCartaoCredito(contaDto.CartaoCredito);

            usuario.AdicionarCartaoCredito(cartao);

            this.usuarioRepository.SalvarUsuarioNaBase(usuario);

            AdicionarCartaoCreditoDtoResponse contaDtoResponse = new AdicionarCartaoCreditoDtoResponse()
            {
                IdCartaoCredito = cartao.Id,
            };

            return contaDtoResponse;
        }





        //PLAYLISTS
        public CriarPlaylistDtoResponse CriarPlaylist(CriarPlaylistDtoRequest playlistDto)
        {
            Usuario usuario = usuarioRepository.ObterUsuarioPorId(playlistDto.IdUsuario);

            Playlist playlist = usuario.CriarPlaylist(playlistDto.Nome);

            this.usuarioRepository.SalvarUsuarioNaBase(usuario);

            CriarPlaylistDtoResponse playlistDtoResponse = new CriarPlaylistDtoResponse()
            {
                IdPlaylist = playlist.Id,
            };

            return playlistDtoResponse;
        }





        // BANDAS





        // ASSINATURAS
        public UsuarioDto AssinarPlano(UsuarioDto contaDto)
        {
            Plano plano = planoService.ObterPlanoPorId(contaDto.PlanoId);
            CartaoCredito cartao = GerarObjetoCartaoCredito(contaDto);
            Usuario usuario = this.ObterUsuarioPorId(contaDto.IdUsuario);
            Empresa empresa = new Empresa();

            usuario.AssinarPlano(empresa.Cnpj, plano, cartao);

            this.usuarioRepository.SalvarUsuarioNaBase(usuario);
            contaDto.Assinaturas = usuario.Assinaturas;

            return contaDto;
        }






        // AUX
        public CartaoCredito GerarObjetoCartaoCredito(CartaoCreditoDto cartaoDto)
        {
            CartaoCredito cartao =  new CartaoCredito() {
                CartaoAtivo = cartaoDto.CartaoAtivo,
                LimiteDisponivel = cartaoDto.LimiteDisponivel,
                Numero = cartaoDto.Numero
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
