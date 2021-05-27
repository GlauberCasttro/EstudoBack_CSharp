using Equipfer.Domain.Entity.EquipagemAggregate;
using Equipfer.Domain.Entity;
using Equipfer.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Equipfer.API.Controllers
{
    [Route("api/v{version:apiVersion}/tarefas")]
    [Produces("application/json")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _service;
        private readonly ILogger _logger;
        public TarefaController(ITarefaService service, ILogger<TarefaController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet("escala-mensal/{matricula}")]
        public async Task<ActionResult<List<TarefaEscalaProgramada>>> EscalaMensalAsync(string matricula)
        {
            try
            {
                if (matricula == null)
                    return BadRequest();

                var tarefas = await _service.EscalaMensalAsync(new Equipagem { Matricula = matricula });

                if (tarefas == null || tarefas.Count < 1)
                    return NotFound();

                return Ok(tarefas);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error: ");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult<List<Tarefa>>> GetAllAsync()
        {
            try
            {
                var tarefas = await _service.GetAllAsync();

                if (tarefas == null || tarefas.Count < 1)
                    return NotFound();

                return Ok(tarefas);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error: ");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet("{codigoFerrovia}/{codigoTarefa}")]
        public async Task<ActionResult<Tarefa>> GetAsync(string codigoFerrovia, string codigoTarefa)
        {
            try
            {
                if (codigoFerrovia == null || codigoTarefa == null)
                    return BadRequest();

                var tarefa = await _service.GetAsync(new Tarefa { CodigoFerrovia = codigoFerrovia, Codigo = codigoTarefa.ToUpper() });

                if (tarefa == null)
                    return NotFound();

                return Ok(tarefa);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error: ");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
