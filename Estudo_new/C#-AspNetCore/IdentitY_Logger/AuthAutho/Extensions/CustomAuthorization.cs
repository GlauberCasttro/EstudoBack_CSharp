using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using System.Security.Claims;

namespace AuthAutho.Extensions
{

    /// <summary>
    /// Essa classe serve apenas para validar as claims
    /// </summary>
    public class CustomAuthorization
    {
        public static bool ValidarClaimsUsuario(HttpContext context, string ClaimName, string claimValue)
        {
            return context.User.Identity.IsAuthenticated &&
                context.User.Claims.Any(c => c.Type == ClaimName && c.Value.Contains(claimValue));
        }
    }

    /// <summary>
    /// Essa classe é responsavel de fazer o filtro funcionar como atributo
    /// </summary>
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string  claimName, string claimValue) : base(typeof(RequisitoClaimFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }


    /// <summary>
    /// Essa é a classe filtro do aspnet como ele nao vai ser um filtro global nao precisamos registrar ele
    /// será usado apenas como atributo
    /// </summary>
    public class RequisitoClaimFilter : IAuthorizationFilter
    {
        readonly Claim _claim;
        public RequisitoClaimFilter(Claim claim)
        {
            _claim = claim;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            //Caso queira um redirecionamento personalizado
            //exemplo abaixo
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Identity", page = "/Account/Login", 
                    ReturnUrl = context.HttpContext.Request.Path.ToString() }));
                return;
            }

            if(!CustomAuthorization.ValidarClaimsUsuario(context.HttpContext, _claim.Type, _claim.Value))
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}
