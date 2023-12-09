using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Plano.Application;
using MusicApp.Plano.Application;
using MusicApp.Plano.Application.DTO;
using MusicApp.Plano.Domain.Aggregates;

namespace MusicApp.Plano.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanoController : ControllerBase
    {

        private readonly ILogger<PlanoController> _logger;
        private readonly PlanoService _service = new PlanoService();

        public PlanoController(ILogger<PlanoController> logger)
        {
            _logger = logger;
        }


        [HttpPost("criar")]
        public IActionResult CriarPlano(CriarPlanoDtoReq dtoReq)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            CriarPlanoDtoResp dtoResp = _service.CriarPlano(dtoReq);

            return Created($"{dtoResp.IdPlano}", dtoResp);
        }


        [HttpGet("{idPlano}")]
        public IActionResult ObterPlanoPorId(Guid idPlano)
        {
            ObterPlanoPorIdDtoResp dtoResp = _service.ObterPlanoPorId(idPlano);

            return Ok(dtoResp);
        }


        [HttpGet("listar")]
        public IActionResult ListarPlanos()
        {
            ListarPlanosDtoResp dtoResp = _service.ListarPlanos();

            return Ok(dtoResp);
        }
        

    }
}
