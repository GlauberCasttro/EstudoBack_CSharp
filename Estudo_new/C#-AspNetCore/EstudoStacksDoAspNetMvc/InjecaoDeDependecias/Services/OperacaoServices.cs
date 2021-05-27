using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InjecaoDeDependecias.Services
{
    /// <summary>
    /// Classe para estudo sobre injeção de dependecia
    /// Singleton - Utiliza a mesma instancia para toda aplicação enquanto o sistema nao reinicializa
    /// Scoped - Reutiliza a mesma instancia do objeto durante todo request
    /// Transient - Obtem uma nova instancia do objeto para cada solicitação
    /// </summary>
    public class OperacaoServices
    {
        public IOperacaoScoped Scoped { get; }
        public IOperacaoSingleton Singleton { get; }
        public IOperacaoTransient Transient { get; }
        public IOperacaoSingletonInstance SingletonInstance { get; }
        public OperacaoServices(IOperacaoScoped scoped, 
            IOperacaoSingleton singleton, 
            IOperacaoTransient transient, 
            IOperacaoSingletonInstance singletonInstance)
        {
            Scoped = scoped;
            Singleton = singleton;
            Transient = transient;
            SingletonInstance = singletonInstance;
        }

    }
    public class Operacao : IOperacaoTransient, IOperacaoScoped, IOperacaoSingleton, IOperacaoSingletonInstance
    {
        public Operacao() : this(Guid.NewGuid())
        {

        }
        public Operacao(Guid id)
        {
            OperacaoId = id;
        }

        public Guid OperacaoId { get; private set; }
    }


    public interface IOperacao
    {
        Guid OperacaoId { get; }
    }



    public interface IOperacaoTransient : IOperacao { }
    public interface IOperacaoScoped : IOperacao { }
    public interface IOperacaoSingleton : IOperacao { }
    public interface IOperacaoSingletonInstance : IOperacao { }

}
