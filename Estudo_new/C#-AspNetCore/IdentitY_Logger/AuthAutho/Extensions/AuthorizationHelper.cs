using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAutho.Extensions
{
    /// <summary>
    /// Classe responsavel por criar forma de autenticaçao com clains em que 
    /// voce tem uma claim do tipo Permissao e tem os valores para essa permissao
    /// PodeLer, PodeEscrever, PodeExcluir. 
    /// esse tipo de implementação é a recomendação da microsoft
    /// tem que fazer a implemetação aqui e tb na startup
    /// </summary>
    public class PermissaoNecessaria : IAuthorizationRequirement
    {
        public string Permissao { get; set; }
        public PermissaoNecessaria(string permissao)
        {
           Permissao = permissao;
        }
    }

    /// <summary>
    /// AuthorizationHelper implementa a IAuthorizationHandler para injeção de dependencia
    /// </summary>
    public class PermissaoNecessariaHandler : AuthorizationHandler<PermissaoNecessaria>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            PermissaoNecessaria requisito)
        {
            if(context.User.HasClaim(c=> c.Type == "Permissao" && c.Value.Contains(requisito.Permissao)))
            {
                context.Succeed(requisito);
            }
            return Task.CompletedTask;
        }
    }
}
