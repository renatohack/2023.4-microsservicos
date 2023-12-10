using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Usuario.Application;
using MusicApp.Usuario.Application.Dto;
using MusicApp.Usuario.Domain.Aggregates;


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
        public async Task<IActionResult> CriarConta(CriarContaDtoReq dtoReq)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            CriarContaDtoResp dtoResp = await this._service.CriarConta(dtoReq);

            return Created($"/{dtoResp.IdUsuario}", dtoResp);
        }


        [HttpGet("{idUsuario}")]
        public IActionResult ObterUsuarioPorId(Guid idUsuario)
        {
            ObterUsuarioPorIdDtoResp dtoResp = this._service.ObterUsuarioPorId(idUsuario);

            return Ok(dtoResp);

        }






        // CARTOES
        [HttpPost("{idUsuario}/cartoes/adicionar")]
        public IActionResult AdicionarCartaoCredito(Guid idUsuario, AdicionarCartaoDtoReq dtoReq)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            dtoReq.IdUsuario = idUsuario;

            AdicionarCartaoDtoResp dtoResp = this._service.AdicionarCartao(dtoReq);

            return Created($"/{idUsuario}/cartoes/{dtoResp.IdCartao}", dtoResp);
        }






        // PLAYLISTS
        [HttpPost("{idUsuario}/playlists/criar")]
        public IActionResult CriarPlaylist(Guid idUsuario, CriarPlaylistDtoReq dtoReq)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            dtoReq.IdUsuario = idUsuario;

            CriarPlaylistDtoResp dtoResp = this._service.CriarPlaylist(dtoReq);

            return Created($"/{idUsuario}/playlists/{dtoResp.IdPlaylist}", dtoResp);
        }





        // BANDAS
        [HttpPost("{idUsuario}/bandas/favoritar")]
        public async Task<IActionResult> FavoritarBanda(Guid idUsuario, FavoritarBandaDtoReq dtoReq)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            dtoReq.IdUsuario = idUsuario;

            FavoritarBandasDtoResp dtoResp = await this._service.FavoritarBanda(dtoReq);

            return Ok(dtoResp);
        }


        [HttpGet("{idUsuario}/bandas/buscar")]
        public IActionResult BuscarBandas(Guid idUsuario, string banda)
        {

            BuscarBandasDtoReq dtoReq = new BuscarBandasDtoReq
            {
                IdUsuario = idUsuario,
                Nome = banda,
            };

            BuscarBandasDtoResp dtoResp = this._service.BuscarBandas(dtoReq);


            return Ok(dtoResp);

        }





        // ASSINATURAS
        [HttpPost("{idUsuario}/planos/assinar")]
        public async Task<IActionResult> AssinarPlano(Guid idUsuario, AssinarPlanoDtoReq dtoReq)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            dtoReq.IdUsuario = idUsuario;

            AssinarPlanoDtoResp dtoResp = await this._service.AssinarPlano(dtoReq);

            return Ok(dtoResp);
        }


    }
}
