using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Banda.Application;
using MusicApp.Banda.Application.DTO;

namespace MusicApp.Banda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandaController : ControllerBase
    {

        private readonly ILogger<BandaController> _logger;
        private readonly BandaService _service = new BandaService();

        public BandaController(ILogger<BandaController> logger)
        {
            _logger = logger;
        }



        // BANDA 
        [HttpPost("criar")]
        public IActionResult CriarBanda(CriarBandaDtoReq dtoReq)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            CriarBandaDtoResp dtoResp = _service.CriarBanda(dtoReq);

            return Created($"{dtoResp.Id}", dtoResp);
        }


        [HttpGet("bandas/{idBanda}")]
        public IActionResult ObterBandaPorId(Guid idBanda)
        {
            ObterBandaPorIdDtoResp dtoResp = _service.ObterBandaPorId(idBanda);

            return Ok(dtoResp);
        }


        [HttpGet("bandas")]
        public IActionResult ListarBandas()
        {
            ListarBandasDtoResp dtoResp = _service.ListarBandas();

            return Ok(dtoResp);
        }






        // MUSICAS
        [HttpPost("{idBanda}/adicionar-musicas")]
        public IActionResult AdicionarMusicas(Guid idBanda, AdicionarMusicasDtoReq dtoReq)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            _service.AdicionarMusicas(idBanda, dtoReq);

            return Ok();
        }


        [HttpGet("{idBanda}/buscar-musicas")]
        public IActionResult BuscarMusicasPorNome(Guid idBanda, string nome)
        {
            BuscarMusicasPorNomeDtoResp dtoResp = _service.ObterMusicasPorNome(idBanda, nome);

            return Ok(dtoResp);
        }


        [HttpGet("musicas/{idMusica}")]
        public IActionResult ObterMusicaPorId(Guid idMusica)
        {
            ObterMusicaPorIdDtoResp dtoResp = _service.ObterMusicaPorId(idMusica);

            return Ok(dtoResp);
        }

    }
}
