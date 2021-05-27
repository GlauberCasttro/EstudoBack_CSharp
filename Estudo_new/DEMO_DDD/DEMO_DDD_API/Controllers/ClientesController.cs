using System;
using AutoMapper;
using DEMO_DDD.APPLICATION.Interfaces;
using DEMO_DDD.APPLICATION.ViewModels;
using DEMO_DDD.DOMAIN.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace DEMO_DDD_API.Controllers
{
    [ApiController]
    [Route("api/{Controller}/")]
    public class ClientesController : Controller
    {

        private readonly IMapper _autoMapper;
        private readonly IClienteAppService _clieentAppService;

        public ClientesController(IClienteAppService clieentAppService, IMapper mapper)
        {
            _clieentAppService = clieentAppService;
            _autoMapper = mapper;
        }


        [HttpGet()]
        [Route("ListarTodos")]
        public async Task<IEnumerable<ClienteViewModel>> Index()
        {

            var Clientes = _autoMapper.Map<IEnumerable<ClienteViewModel>>(await _clieentAppService.ObterTodos());

            return Clientes;

        }

        [HttpGet()]
        [Route("ObterClientePorNome")]
        public async Task<ActionResult<IEnumerable<ClienteViewModel>>> ObterPedidoFiltro(string nome, DateTime? dataCadastro)
        {
            if (string.IsNullOrEmpty(nome) || dataCadastro != null) return BadRequest();

            try
            {
                return Ok(_autoMapper.Map<IEnumerable<ClienteViewModel>>(await _clieentAppService.ObterPorNome(nome, dataCadastro)));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpGet()]
        [Route("Especiais")]
        public async Task<IEnumerable<ClienteViewModel>> Especiais()
        {
            return _autoMapper.Map<IEnumerable<ClienteViewModel>>(await _clieentAppService.ObterClientesEspeciaisAsync());
        }

        [HttpGet()]
        [Route("ClientesProdutos")]
        public async Task<IEnumerable<ClienteViewModel>> ClientesProdutos()
        {
            return _autoMapper.Map<IEnumerable<ClienteViewModel>>(await _clieentAppService.ObterClientesProdutos());
        }

        [HttpPost()]
        [Route("Adicionar")]
        public async Task<ActionResult<ClienteViewModel>> Adicionar(ClienteViewModel produto)
        {
            if (!ModelState.IsValid)
            {
                return NoContent();
            }
            await _clieentAppService.Adcionar(_autoMapper.Map<Cliente>(produto));

            return Ok();

        }


        //// GET: Clientes/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Clientes/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Clientes/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Clientes/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Clientes/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Clientes/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Clientes/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}



    }
    public class DomainException : Exception
    {
        public DomainException()
        { }

        public DomainException(string message) : base(message)
        { }

        public DomainException(string message, Exception innerException) : base(message, innerException)
        { }
    }

}