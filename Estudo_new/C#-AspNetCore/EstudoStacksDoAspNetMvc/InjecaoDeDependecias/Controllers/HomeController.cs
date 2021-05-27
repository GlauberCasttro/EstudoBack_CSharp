using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InjecaoDeDependecias.Models;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace InjecaoDeDependecias.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPedidoRepository _pedidoRepository;

        public HomeController(ILogger<HomeController> logger, 
            IPedidoRepository pedidoRepository)
        {
            _logger = logger;
            _pedidoRepository = pedidoRepository;
        }

        public IActionResult Index()
        {
            try
            {
               
                var pedido = _pedidoRepository.ObterPedido();
                _logger.LogInformation("Deu certo...");
            }
            catch (Exception erro)
            {
                _logger.LogError("O Erro ocorreu aqui..."+ $"{erro.StackTrace.LastIndexOf(":line")}");
                throw erro;
                
            }
           
           
         
            return View();
        }


        //outra forma de injeçao de dependecia
        //nao recomendado e utilizado apenas quando nao se pode mexer no construtor da classe
        public IActionResult Privacy([FromServices]IPedidoRepository pedido)
        {
            var pedidorepositorio = pedido.ObterPedido();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
