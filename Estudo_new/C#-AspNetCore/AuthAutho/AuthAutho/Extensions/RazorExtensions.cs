using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAutho.Extensions
{
    /// <summary>
    /// Tres metodos de extensao de razor page
    /// 
    /// </summary>
    public static class RazorExtensions
    {

        /// <summary>
        /// se quiser validar algo na view do razor, baseado em uma claim
        /// </summary>
        /// <param name="page"></param>
        /// <param name="claimName"></param>
        /// <param name="claimValue"></param>
        /// <returns></returns>
        public static  bool IfClaim(this RazorPage page, string claimName, string claimValue)
        {
            return CustomAuthorization.ValidarClaimsUsuario(page.Context, claimName, claimValue);
        }

        /// <summary>
        /// Se eu quiser desabilitar algo na view razor, por exemplo desabilitar um botao
        /// </summary>
        /// <param name="page"></param>
        /// <param name="claimName"></param>
        /// <param name="claimValue"></param>
        /// <returns></returns>
        public static string IfClaimShow(this RazorPage page, string claimName, string claimValue)
        {
            return CustomAuthorization.ValidarClaimsUsuario(page.Context, claimName, claimValue) ? "" : "disabled";
        }


        /// <summary>
        /// se eu quiser esconder algo,por exempllo se eu quiser mostrar o link se o usuario tiver determinada claim
        /// </summary>
        /// <param name="page"></param>
        /// <param name="context"></param>
        /// <param name="claimName"></param>
        /// <param name="claimValue"></param>
        /// <returns></returns>
        public static IHtmlContent IfClaimShow(this IHtmlContent page, HttpContext context,  string claimName, string claimValue)
        {
            return CustomAuthorization.ValidarClaimsUsuario(context, claimName, claimValue) ? page : null;
        }
    }
}
