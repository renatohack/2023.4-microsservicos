﻿using Microsoft.AspNetCore.Http;
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


        [HttpPost]
        public IActionResult CriarConta(CriarContaDto contaDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            contaDto = this._service.CriarConta(contaDto);

            return Created($"/usuario/{contaDto.Id}", contaDto);
        }

    }
}
