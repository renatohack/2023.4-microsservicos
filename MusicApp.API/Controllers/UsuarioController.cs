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



        // POST

        [HttpPost]
        public IActionResult CriarConta(UsuarioDto contaDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            contaDto = this._service.CriarConta(contaDto);

            return Created($"/usuario/{contaDto.Id}", contaDto);
        }


        [HttpPost("playlist")]
        public IActionResult CriarPlaylist(CriarPlaylistDto playlistDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            playlistDto = this._service.CriarPlaylist(playlistDto);

            return Created($"/playlist/{playlistDto.IdPlaylist}", playlistDto);
        }


        [HttpGet("{idUsuario}")]
        public IActionResult ObterUsuario(Guid idUsuario)
        {
            var usuario = this._service.ObterUsuarioPorId(idUsuario);

            if (usuario == null) 
                return null;

            UsuarioDto result = new UsuarioDto()
            {
                Id = usuario.Id,
                CartaoCredito = new CartaoCreditoDto()
                {
                    CartaoAtivo = usuario.Cartoes.FirstOrDefault().CartaoAtivo,
                    LimiteDisponivel = usuario.Cartoes.FirstOrDefault().LimiteDisponivel,
                    Numero = "xxxx-xxxx-xxxx-xxxx"
                },
                Nome = usuario.Nome
            };

            return Ok(result);

        }

    }
}
