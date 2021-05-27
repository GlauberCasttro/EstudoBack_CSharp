using AuthAutho.Extensions;
using AuthAutho.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KissLog;
using System;

namespace AuthAutho.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly ILogger _logger;

        public HomeController(ILogger logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            try
            {
                //Testando o logger do kissLogger
                throw new Exception("Algo Horrível ocorreu... ");
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
         
        }

       
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Secrety()
        {
            return View();
        }

        [Authorize(Policy = "PodeExcluir")]
        public IActionResult SecretyClaim()
        {
            return View();
        }

        [Authorize(Policy = "PodeEscrever")]
        public IActionResult SecretyClaimGravar()
        {
            return View();
        }

        //Customizando Claims
        //Home = chave
        //Segredo = valor da claims,poderia ser n valores
        [ClaimsAuthorize("Home","Segredo")]
        public IActionResult ClaimsCustom()
        {
            return View("SecretyClaim");
        }

        [ClaimsAuthorize("Produtos", "Leer")] 
        public IActionResult ClaimsCustomProdutos()
        {
            return View();
        }



        [Route("erro/{idErro:length(3,3)}")]
        public IActionResult Error(int idErro)
        {
            var model = new ErrorViewModel();
            switch (idErro)
            {
                case 500:
                    model.Mensagem = "Ocorreu um erro!!! Tente novamente mais tarde ou contate nosso suporte";
                    model.Titulo = "Ocorreu um erro!!!";
                    model.ErroCode = idErro;
                    break;
                case 404:

                    model.Mensagem = "A página que está procurando nao foi encontrada. <br/>Em caso de dúvidas entre em contato com nosso suporte";
                    model.Titulo = "Opss!!! Página não encontrada!!!";
                    model.ErroCode = idErro;
                    break;
                case 403:

                    model.Mensagem = "Você não tem permissão para acessar essa pagina. <br/>Em caso de dúvidas entre em contato com nosso suporte";
                    model.Titulo = "Opss!!! Você não tem permissão para fazer isto.";
                    model.ErroCode = idErro;
                    break;
                default:
                    model.Mensagem = "Ocorreu um erro inesperado.";
                    model.Titulo = "=(";
                    model.ErroCode = idErro;
                    break;
            }

            return View("Error", model);
        }
    }
}
