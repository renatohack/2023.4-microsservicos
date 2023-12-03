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
            ObterUsuarioPorIdResponse usuarioResponseDto = this._service.ObterUsuarioPorId(idUsuario);

            //if (usuarioResponseDto == null)
            //    return null;

            return Ok(usuarioResponseDto);

        }






        // CARTOES
        [HttpPost("cartoes/adicionar")]
        public IActionResult AdicionarCartaoCredito(UsuarioDto contaDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            contaDto = this._service.AdicionarCartaoCredito(contaDto);

            return Created($"/cartoes/{contaDto.CartaoCredito.IdCartaoCredito}", contaDto);
        }






        // PLAYLISTS
        [HttpPost("playlist/criar")]
        public IActionResult CriarPlaylist(CriarPlaylistDto playlistDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            playlistDto = this._service.CriarPlaylist(playlistDto);

            return Created($"/playlist/{playlistDto.IdPlaylist}", playlistDto);
        }





        // BANDAS





        // ASSINATURAS
        [HttpPost("plano/assinar")]
        public IActionResult AssinarPlano(UsuarioDto contaDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }


            contaDto = this._service.AssinarPlano(contaDto);

            return Created($"/plano/{contaDto.Assinaturas.Last().Id}", contaDto);
        }


    }
}
