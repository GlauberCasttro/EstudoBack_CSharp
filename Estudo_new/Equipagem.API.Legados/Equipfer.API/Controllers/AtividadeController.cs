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
    [Route("api/v{version:apiVersion}/atividades")]
    [Produces("application/json")]
    [ApiController]
    public class AtividadeController : ControllerBase
    {
        private readonly IAtividadeService _service;
        private readonly ILogger _logger;
        public AtividadeController(IAtividadeService atividadeService, ILogger<AtividadeController> logger)
        {
            _service = atividadeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Atividade>>> GetAllAsync()
        {
            try
            {
                var atividades = await _service.GetAllAsync();

                if (atividades.Count > 0)
                    return Ok(atividades);

                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error: ");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{codigo}")]
        public async Task<ActionResult<Atividade>> GetAsync(string codigo)
        {
            try
            {
                if (codigo == null)
                    return BadRequest();

                var atividade = await _service.GetAsync(new Atividade { Codigo = codigo });

                if (atividade == null)
                    return NotFound();

                return Ok(atividade);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error: ");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet("jornada-atual/{matricula}")]
        public async Task<ActionResult<List<AtividadeJornada>>> GetAtividadesJornadaAtual(string matricula)
        {
            try
            {
                if (matricula == null)
                    return BadRequest();

                var atividades = await _service.AtividadeJornadaAtualAsync(new Equipagem { Matricula = matricula });

                if (atividades == null || atividades.Count < 1)
                    return NotFound();

                return Ok(atividades);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error: ");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet("jornada-anterior/{matricula}")]
        public async Task<ActionResult<List<AtividadeJornada>>> GetAtividadesJornadaAnteriorAsync(string matricula)
        {
            try
            {
                if (matricula == null)
                    return BadRequest();

                var atividades = await _service.AtividadeJornadaAnteriorAsync(new Equipagem { Matricula = matricula });

                if (atividades == null || atividades.Count < 1)
                    return NotFound();

                return Ok(atividades);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error: ");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}