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

        private UsuarioRepository usuarioRepo = new UsuarioRepository();
        private PlanoRepository planoRepo = new PlanoRepository();
        private BandaRepository bandaRepo = new BandaRepository();


        // USUARIO
        public async Task<CriarContaDtoResp> CriarConta(CriarContaDtoReq dtoReq)
        {

            // Pega plano no banco a partir do ID passado dentro da classe DTO
            domain.Plano plano = await planoRepo.ObterPlanoPorId(dtoReq.PlanoId);

            // Gera um objeto cartão a partir da classe DTO, para ser usado na criação do usuário
            domain.Cartao cartao = GerarObjetoCartao(dtoReq.Cartao);

            // Cria usuario, passando cartao criado e plano retornado
            domain.Usuario usuarioCriado = domain.Usuario.CriarUsuario(dtoReq.Nome, cartao, plano);

            // Gravar novo usuarioCriado na base
            this.usuarioRepo.SalvarUsuarioNaBase(usuarioCriado);

            // Retornar Conta DTO, com o ID atualizado
            CriarContaDtoResp dtoResp = new CriarContaDtoResp()
            {
                IdUsuario = usuarioCriado.Id,
            };

            return dtoResp;
            
        }

        public ObterUsuarioPorIdDtoResp ObterUsuarioPorId(Guid idUsuario)
        {
            domain.Usuario usuario = usuarioRepo.ObterUsuarioPorId(idUsuario);

            if (usuario == null)
            {
                ErroNegocio erroNegocio = new ErroNegocio()
                {
                    MensagemErro = "Usuário não encontrado.",
                    NomeErro = nameof(CriarPlaylist),
                };
                throw new BusinessException(erroNegocio);
            }

            ObterUsuarioPorIdDtoResp usuarioResponse = new ObterUsuarioPorIdDtoResp()
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
        public AdicionarCartaoDtoResp AdicionarCartao(AdicionarCartaoDtoReq contaDto)
        {
            domain.Usuario usuario = usuarioRepo.ObterUsuarioPorId(contaDto.IdUsuario);

            domain.Cartao cartao = this.GerarObjetoCartao(contaDto.Cartao);

            usuario.AdicionarCartao(cartao);

            this.usuarioRepo.SalvarUsuarioNaBase(usuario);

            AdicionarCartaoDtoResp contaDtoResponse = new AdicionarCartaoDtoResp()
            {
                IdCartao = cartao.Id,
            };

            return contaDtoResponse;
        }





        //PLAYLISTS
        public CriarPlaylistDtoResp CriarPlaylist(CriarPlaylistDtoReq playlistDto)
        {
            domain.Usuario usuario = usuarioRepo.ObterUsuarioPorId(playlistDto.IdUsuario);

            domain.Playlist playlist = usuario.CriarPlaylist(playlistDto.Nome);

            this.usuarioRepo.SalvarUsuarioNaBase(usuario);

            CriarPlaylistDtoResp playlistDtoResponse = new CriarPlaylistDtoResp()
            {
                IdPlaylist = playlist.Id,
            };

            return playlistDtoResponse;
        }





        // BANDAS
        public async Task<FavoritarBandasDtoResp> FavoritarBanda(FavoritarBandaDtoReq contaDto)
        {
            domain.Usuario usuario = usuarioRepo.ObterUsuarioPorId(contaDto.IdUsuario);
            domain.Banda banda = await bandaRepo.ObterBandaPorId(contaDto.IdBanda);

            usuario.FavoritarBanda(banda);
            usuarioRepo.SalvarUsuarioNaBase(usuario);

            FavoritarBandasDtoResp contaDtoResponse = new FavoritarBandasDtoResp()
            {
                IdUsuario = contaDto.IdUsuario,
                BandasFavoritas = usuario.BandasFavoritas,
            };

            return contaDtoResponse;
        }


        public BuscarBandasDtoResp BuscarBandas(BuscarBandasDtoReq bandaDto)
        {
            domain.Usuario usuario = usuarioRepo.ObterUsuarioPorId(bandaDto.IdUsuario);
            List<domain.Banda> bandas = usuario.BuscarBanda(bandaDto.Nome);

            BuscarBandasDtoResp bandaDtoResponse = new BuscarBandasDtoResp
            {
                Bandas = bandas,
            };

            return bandaDtoResponse;
        }




        // ASSINATURAS
        public async Task<AssinarPlanoDtoResp> AssinarPlano(AssinarPlanoDtoReq contaDto)
        {
            domain.Plano plano = await planoRepo.ObterPlanoPorId(contaDto.IdPlano);
            domain.Usuario usuario = usuarioRepo.ObterUsuarioPorId(contaDto.IdUsuario);
            domain.Cartao cartao = usuario.BuscarCartaoPorId(contaDto.IdCartao);
            domain.Empresa empresa = new domain.Empresa();

            domain.Assinatura assinatura = usuario.AssinarPlano(empresa.Cnpj, plano, cartao);

            this.usuarioRepo.SalvarUsuarioNaBase(usuario);

            
            AssinarPlanoDtoResp planoDtoResponse = new AssinarPlanoDtoResp
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
