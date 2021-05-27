using Equipfer.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace Equipfer.API.Controllers
{
    [Route("api/v{version:apiVersion}/trens")]
    [Produces("application/json")]
    [ApiController]
    public class TremController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _http;
        public TremController(ILogger<TremController> logger, IHttpClientFactory http)
        {
            _logger = logger;
            _http = http;
        }
        [HttpGet("{estacao}")]
        public async Task<ActionResult<PrevisoesTrens>> GetTrens(string estacao)
        {
            try
            {
                if (estacao == null || estacao == "")
                    return BadRequest();

                var client = _http.CreateClient("CCO_API");

                var result = client.GetStreamAsync($"/apiportalcco/api/v1/previsoes/trens/{estacao}");

                var serializer = new DataContractJsonSerializer(typeof(PrevisoesTrens));

                var response = serializer.ReadObject(await result) as PrevisoesTrens;

                if (response != null)
                    return Ok(response);

                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error: ");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
