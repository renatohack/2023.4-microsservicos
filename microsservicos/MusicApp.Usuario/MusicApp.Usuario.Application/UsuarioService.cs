using MusicApp.Usuario.Repository;
using domain = MusicApp.Usuario.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using MusicApp.Core.Exception;
using MusicApp.Usuario.Application;
using MusicApp.Usuario.Application.Dto;

namespace MusicApp.Usuario.Application
{
    public class UsuarioService
    {

        private UsuarioRepository usuarioRepository = new UsuarioRepository();
        private BandaRepository bandaRepository = new BandaRepository();

        private PlanoService planoService = new PlanoService();


        // USUARIO
        public CriarContaDtoResponse CriarConta(CriarContaDtoRequest contaDto)
        {

            // Pega plano no banco a partir do ID passado dentro da classe DTO
            domain.Plano plano = planoService.ObterPlanoPorId(contaDto.PlanoId);

            // Gera um objeto cartão a partir da classe DTO, para ser usado na criação do usuário
            domain.Cartao cartao = GerarObjetoCartao(contaDto.Cartao);

            // Cria usuario, passando cartao criado e plano retornado
            domain.Usuario usuarioCriado = domain.Usuario.CriarUsuario(contaDto.Nome, cartao, plano);

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
            domain.Usuario usuario = usuarioRepository.ObterUsuarioPorId(idUsuario);

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
                CartoesCredito = usuario.Cartoes,
            };

            return usuarioResponse;
        }






        // CARTOES
        public AdicionarCartaoDtoResponse AdicionarCartao(AdicionarCartaoDtoRequest contaDto)
        {
            domain.Usuario usuario = usuarioRepository.ObterUsuarioPorId(contaDto.IdUsuario);

            domain.Cartao cartao = this.GerarObjetoCartao(contaDto.Cartao);

            usuario.AdicionarCartao(cartao);

            this.usuarioRepository.SalvarUsuarioNaBase(usuario);

            AdicionarCartaoDtoResponse contaDtoResponse = new AdicionarCartaoDtoResponse()
            {
                IdCartao = cartao.Id,
            };

            return contaDtoResponse;
        }





        //PLAYLISTS
        public CriarPlaylistDtoResponse CriarPlaylist(CriarPlaylistDtoRequest playlistDto)
        {
            domain.Usuario usuario = usuarioRepository.ObterUsuarioPorId(playlistDto.IdUsuario);

            domain.Playlist playlist = usuario.CriarPlaylist(playlistDto.Nome);

            this.usuarioRepository.SalvarUsuarioNaBase(usuario);

            CriarPlaylistDtoResponse playlistDtoResponse = new CriarPlaylistDtoResponse()
            {
                IdPlaylist = playlist.Id,
            };

            return playlistDtoResponse;
        }





        // BANDAS
        public FavoritarBandasDtoResponse FavoritarBanda(FavoritarBandaDtoRequest contaDto)
        {
            domain.Usuario usuario = usuarioRepository.ObterUsuarioPorId(contaDto.IdUsuario);
            domain.Banda banda = bandaRepository.ObterBandaPorId(contaDto.IdBanda);

            usuario.FavoritarBanda(banda);
            usuarioRepository.SalvarUsuarioNaBase(usuario);

            FavoritarBandasDtoResponse contaDtoResponse = new FavoritarBandasDtoResponse()
            {
                IdUsuario = contaDto.IdUsuario,
                BandasFavoritas = usuario.BandasFavoritas,
            };

            return contaDtoResponse;
        }


        public BuscarBandasDtoResponse BuscarBandas(BuscarBandasDtoRequest bandaDto)
        {
            domain.Usuario usuario = usuarioRepository.ObterUsuarioPorId(bandaDto.IdUsuario);
            List<domain.Banda> bandas = usuario.BuscarBanda(bandaDto.Nome);

            BuscarBandasDtoResponse bandaDtoResponse = new BuscarBandasDtoResponse
            {
                Bandas = bandas,
            };

            return bandaDtoResponse;
        }




        // ASSINATURAS
        public AssinarPlanoDtoResponse AssinarPlano(AssinarPlanoDtoRequest contaDto)
        {
            domain.Plano plano = planoService.ObterPlanoPorId(contaDto.IdPlano);
            domain.Usuario usuario = usuarioRepository.ObterUsuarioPorId(contaDto.IdUsuario);
            domain.Cartao cartao = usuario.BuscarCartaoPorId(contaDto.IdCartao);
            domain.Empresa empresa = new domain.Empresa();

            domain.Assinatura assinatura = usuario.AssinarPlano(empresa.Cnpj, plano, cartao);

            this.usuarioRepository.SalvarUsuarioNaBase(usuario);

            
            AssinarPlanoDtoResponse planoDtoResponse = new AssinarPlanoDtoResponse
            {
                IdAssinatura = assinatura.Id,
            };

            return planoDtoResponse;
        }






        // AUX
        public domain.Cartao GerarObjetoCartao(CartaoDto cartaoDto)
        {
            domain.Cartao cartao =  new domain.Cartao() {
                CartaoAtivo = cartaoDto.CartaoAtivo,
                LimiteDisponivel = cartaoDto.LimiteDisponivel,
                Numero = cartaoDto.Numero
            };

            return cartao;
        }

        public domain.Playlist GerarObjetoPlaylist(String nome)
        {
            domain.Playlist playlist = new domain.Playlist()
            {
                Nome = nome
            };

            return playlist;
        }
    }
}
