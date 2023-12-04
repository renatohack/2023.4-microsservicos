using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Application.Conta;
using MusicApp.Application.Conta.Dto;

namespace MusicApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly UsuarioService _service = new UsuarioService();

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }



        // USUARIO

        [HttpPost("criar")]
        public IActionResult CriarConta(CriarContaDtoRequest contaDtoRequest)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            CriarContaDtoResponse contaDtoResponse = this._service.CriarConta(contaDtoRequest);

            return Created($"/usuario/{contaDtoResponse.IdUsuario}", contaDtoResponse);
        }


        [HttpGet("{idUsuario}")]
        public IActionResult ObterUsuarioPorId(Guid idUsuario)
        {
            ObterUsuarioPorIdDtoResponse usuarioDtoResponse = this._service.ObterUsuarioPorId(idUsuario);

            return Ok(usuarioDtoResponse);

        }






        // CARTOES
        [HttpPost("cartoes/adicionar")]
        public IActionResult AdicionarCartaoCredito(AdicionarCartaoCreditoDtoRequest contaDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            AdicionarCartaoCreditoDtoResponse contaDtoResponse = this._service.AdicionarCartaoCredito(contaDto);

            return Created($"/cartoes/{contaDtoResponse.IdCartaoCredito}", contaDtoResponse);
        }






        // PLAYLISTS
        [HttpPost("playlists/criar")]
        public IActionResult CriarPlaylist(CriarPlaylistDtoRequest playlistDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            CriarPlaylistDtoResponse playlistDtoResponse = this._service.CriarPlaylist(playlistDto);

            return Created($"/playlists/{playlistDtoResponse.IdPlaylist}", playlistDtoResponse);
        }





        // BANDAS
        [HttpPost("bandas/favoritar")]
        public IActionResult FavoritarBanda(FavoritarBandaDtoRequest contaDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }


            FavoritarBandasDtoResponse contaDtoResponse = this._service.FavoritarBanda(contaDto);

            return Ok(contaDtoResponse);
        }


        [HttpGet("{idUsuario}/{banda}")]
        public IActionResult ObterBandasPorSubstring(Guid idUsuario, string banda)
        {

            ObterBandasPorSubstringDtoRequest bandaDtoRequest = new ObterBandasPorSubstringDtoRequest
            {
                IdUsuario = idUsuario,
                Nome = banda,
            };

            ObterBandasPorSubstringDtoResponse bandasDtoResponse = this._service.ObterBandasPorSubstring(bandaDtoRequest);


            return Ok(bandasDtoResponse);

        }





        // ASSINATURAS
        [HttpPost("plano/assinar")]
        public IActionResult AssinarPlano(AssinarPlanoDtoRequest contaDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }


            AssinarPlanoDtoResponse contaDtoResponse = this._service.AssinarPlano(contaDto);

            return Ok(contaDtoResponse);
        }


    }
}
