using InjecaoDeDependecias.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace InjecaoDeDependecias.Controllers
{
    public class InjecaoDependenciaController : Controller
    {
        public OperacaoServices OperacaoServices1 { get; }
        public OperacaoServices OperacaoServices2 { get; }
        public InjecaoDependenciaController(OperacaoServices operacao1, OperacaoServices operacao2)
        {
            OperacaoServices1 = operacao1;
            OperacaoServices2 = operacao2;
        }
        public string Index()
        {
            return "Primera instãncia: " + Environment.NewLine +
                OperacaoServices1.Transient.OperacaoId + " Transient - Obtém uma nova instãncia do objeto para cada solicitação" + Environment.NewLine + 
                OperacaoServices1.Scoped.OperacaoId + " Scoped - Reutiliza a mesma instancia do objeto durante todo o request" + Environment.NewLine +
                OperacaoServices1.Singleton.OperacaoId + " Singleton - a mesma instancia pra toda aplicação" + Environment.NewLine +
                OperacaoServices1.SingletonInstance.OperacaoId +  " SingletonInstance - pré-definido" + Environment.NewLine +
                Environment.NewLine +
                Environment.NewLine +

                "Segunda instãncia: " + Environment.NewLine +
                OperacaoServices2.Transient.OperacaoId + " Transient" + Environment.NewLine +
                OperacaoServices2.Scoped.OperacaoId + " Scoped" + Environment.NewLine +
                OperacaoServices2.Singleton.OperacaoId + " Singleton" + Environment.NewLine +
                OperacaoServices2.SingletonInstance.OperacaoId + " SingletonInstance - pré-definido" + Environment.NewLine + 
                Environment.NewLine +
                Environment.NewLine;

        }
    }
}