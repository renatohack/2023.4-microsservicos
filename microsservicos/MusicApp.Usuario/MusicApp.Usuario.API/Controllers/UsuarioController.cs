using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Usuario.Application;
using MusicApp.Usuario.Application.Dto;


namespace MusicApp.Usuario.API.Controllers
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
        public IActionResult CriarConta(CriarContaDtoReq dtoReq)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            CriarContaDtoResp dtoResp = this._service.CriarConta(dtoReq);

            return Created($"/usuario/{dtoResp.IdUsuario}", dtoResp);
        }


        [HttpGet("{idUsuario}")]
        public IActionResult ObterUsuarioPorId(Guid idUsuario)
        {
            ObterUsuarioPorIdDtoResponse dtoResponse = this._service.ObterUsuarioPorId(idUsuario);

            return Ok(dtoResponse);

        }






        // CARTOES
        [HttpPost("cartoes/adicionar")]
        public IActionResult AdicionarCartaoCredito(AdicionarCartaoDtoRequest dto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            AdicionarCartaoDtoResponse dtoResponse = this._service.AdicionarCartao(dto);

            return Created($"/cartoes/{dtoResponse.IdCartao}", dtoResponse);
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
