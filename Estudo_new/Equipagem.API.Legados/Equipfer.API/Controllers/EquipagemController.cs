using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Equipfer.Domain.Entity;
using Equipfer.Domain.Entity.EquipagemAggregate;
using Equipfer.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Equipfer.API.Controllers
{
    [Route("api/v{version:apiVersion}/equipagens")]
    [Produces("application/json")]
    [ApiController]
    public class EquipagemController : ControllerBase
    {
        private readonly IEquipagemService _service;
        private readonly IAtividadeService _atividadeService;
        private readonly ILogger _logger;
        public EquipagemController(IEquipagemService service, ILogger<EquipagemController> logger, IAtividadeService atividadeService)
        {
            _service = service;
            _logger = logger;
            _atividadeService = atividadeService;
        }

        [HttpGet("disponiveis/{destacamento}")]
        public async Task<ActionResult<List<string>>> DisponiveisAsync(string destacamento)
        {
            try
            {
                if (destacamento == null)
                    return BadRequest();

                var matriculas = await _service.GetAllDisponiveisAsync(destacamento.ToUpper());

                if (matriculas.Count > 0)
                    return Ok(matriculas);

                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error: ");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{matricula}")]
        public async Task<ActionResult<Equipagem>> GetAsync(string matricula)
        {
            try
            {
                if (matricula == null)
                    return BadRequest();

                var equipagem = await _service.GetAsync(new Equipagem { Matricula = matricula });

                if (equipagem == null)
                    return NotFound("Este recurso não possui tarefa programada!");

                return Ok(equipagem);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error: ");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{matricula}/jornada")]
        public async Task<ActionResult<JornadaMensalAcumulada>> JornadaAcumuladaAsync(string matricula)
        {
            try
            {
                if (matricula == null)
                    return BadRequest();

                var jonada = await _atividadeService.AtividadesQuizenalMensalAsync(new Equipagem { Matricula = matricula });

                if (jonada == null)
                    return NotFound();

                return Ok(jonada);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error: ");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}