using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Areas.Areas.Produtos.Controllers
{
    [Area("Produtos")]
    [Route("{area}/cadastroProdutos/{action}")]
    public class CadastroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Busca()
        {
            return View("Cadastro");
        }
    }
}