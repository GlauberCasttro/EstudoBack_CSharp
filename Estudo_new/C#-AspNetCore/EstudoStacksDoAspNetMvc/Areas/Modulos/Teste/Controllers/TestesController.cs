using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Areas.Modulos.Teste.Controllers
{
    [Area("Teste")]
    [Route("{area}/cadastro/{action}")]
    public class TestesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}