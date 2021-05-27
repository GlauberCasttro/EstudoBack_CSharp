using KissLog;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAutho.Extensions
{
    public class AuditoriaFilter : IActionFilter
    {

        private readonly ILogger _logger;
        public AuditoriaFilter(ILogger logger)
        {
            _logger = logger;
        }
        //Após a execução do filtto
        /// <summary>
        /// Criação de um logger de auditoria
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var message = context.HttpContext.User.Identity.Name + " Acessou: " +
                    context.HttpContext.Request.GetDisplayUrl();
                _logger.Info(message);
            }
        }

       
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
