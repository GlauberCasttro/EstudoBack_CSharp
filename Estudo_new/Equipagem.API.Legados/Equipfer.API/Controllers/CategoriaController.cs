using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Equipfer.Domain.Entity;
using Equipfer.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Equipfer.API.Controllers
{
    [Route("api/v{version:apiVersion}/categorias")]
    [Produces("application/json")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _service;
        private readonly ILogger _logger;
        public CategoriaController(ICategoriaService categoriaService, ILogger<CategoriaController> logger)
        {
            _service = categoriaService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> GetAllAsync()
        {
            try
            {
                var categorias = await _service.GetAllAsync();

                if (categorias.Count > 0)
                    return Ok(categorias);

                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error: ");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet("{ferrovia}/{codigo}")]
        public async Task<ActionResult<Categoria>> GetAsync(string ferrovia, string codigo)
        {
            try
            {
                if (ferrovia == null || codigo == null)
                    return BadRequest();

                var categoria = await _service.GetAsync(new Categoria { CodigoFerrovia = ferrovia, Codigo = codigo.ToUpper() });

                if (categoria == null)
                    return NotFound();

                return Ok(categoria);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error: ");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}