using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelsValidations.Models;

namespace ModelsValidations.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var filme = new Filme
            {
                Id = 1,
                Titulo = "Teste",
                Valor = 1000,
                Avaliacao = 10,
                DataLancamento = DateTime.Now,
                Genero = "masculino"
            };
            return RedirectToAction("Privacy", filme);
            return View();
        }

        public IActionResult Privacy( Filme filme)
        {
            if (ModelState.IsValid)
            {
           
            }

            foreach (var item in ModelState.Values.SelectMany(erro=> erro.Errors))//pegando os erros da model state
            {
                _logger.LogError(item.ErrorMessage);
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
