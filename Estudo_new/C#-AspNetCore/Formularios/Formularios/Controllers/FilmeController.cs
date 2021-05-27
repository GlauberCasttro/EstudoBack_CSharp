using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Formularios.Models;
using Microsoft.AspNetCore.Mvc;

namespace Formularios.Controllers
{
    public class FilmeController : Controller
    {
        [HttpGet]
        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Adicionar(Filme filme)
        {
            if (ModelState.IsValid)
            {
                //alguma coisa
            }
            return View(filme);
        }
    }
}