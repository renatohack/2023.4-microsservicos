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
        public IActionResult CriarConta(CriarContaDtoRequest dto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            CriarContaDtoResponse dtoResponse = this._service.CriarConta(dto);

            return Created($"/usuario/{dtoResponse.IdUsuario}", dtoResponse);
        }


        [HttpGet("{idUsuario}")]
        public IActionResult ObterUsuarioPorId(Guid idUsuario)
        {
            ObterUsuarioPorIdDtoResponse dtoResponse = this._service.ObterUsuarioPorId(idUsuario);

            return Ok(dtoResponse);

        }






        // CARTOES
        [HttpPost("cartoes/adicionar")]
        public IActionResult AdicionarCartaoCredito(AdicionarCartaoCreditoDtoRequest dto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            AdicionarCartaoCreditoDtoResponse dtoResponse = this._service.AdicionarCartaoCredito(dto);

            return Created($"/cartoes/{dtoResponse.IdCartaoCredito}", dtoResponse);
        }






        // PLAYLISTS
        [HttpPost("playlists/criar")]
        public IActionResult CriarPlaylist(CriarPlaylistDtoRequest dto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            CriarPlaylistDtoResponse dtoResponse = this._service.CriarPlaylist(dto);

            return Created($"/playlists/{dtoResponse.IdPlaylist}", dtoResponse);
        }





        // BANDAS
        [HttpPost("bandas/favoritar")]
        public IActionResult FavoritarBanda(FavoritarBandaDtoRequest dto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }


            FavoritarBandasDtoResponse dtoResponse = this._service.FavoritarBanda(dto);

            return Ok(dtoResponse);
        }


        [HttpGet("{idUsuario}/{banda}")]
        public IActionResult ObterBandas(Guid idUsuario, string banda)
        {

            ObterBandasDtoRequest bandaDtoRequest = new ObterBandasDtoRequest
            {
                IdUsuario = idUsuario,
                Nome = banda,
            };

            ObterBandasDtoResponse dtoResponse = this._service.ObterBandas(bandaDtoRequest);


            return Ok(dtoResponse);

        }





        // ASSINATURAS
        [HttpPost("plano/assinar")]
        public IActionResult AssinarPlano(AssinarPlanoDtoRequest dto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }


            AssinarPlanoDtoResponse dtoResponse = this._service.AssinarPlano(dto);

            return Ok(dtoResponse);
        }


    }
}
