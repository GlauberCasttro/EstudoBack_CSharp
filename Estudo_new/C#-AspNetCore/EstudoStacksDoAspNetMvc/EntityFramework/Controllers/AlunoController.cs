using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.Data;
using EntityFramework.Models;
using Microsoft.AspNetCore.Mvc;

namespace EntityFramework.Controllers
{
    public class AlunoController : Controller
    {
        private readonly MeuDbContext _contexto;

        public AlunoController(MeuDbContext context)
        {
            _contexto = context;
        }
        public IActionResult Index()
        {
            var aluno = new Aluno
            {
                Nome = "Glauber",
                Email = "GlauberCasttro@gmail.com",
                DataDeNascimento = DateTime.Now
            };

            //salvando na tabela aluno
            _contexto.Alunos.Add(aluno);
            _contexto.SaveChanges();//saveChanges retorna um int 
            //se retornar  um numero é a quantidade de lnhas afetadas, caso retorne 0 nao inseriu nada

            //buscando aluno no banco
            var aluno2 = _contexto.Alunos.Find(aluno.Id);


            //buscando o primeiro que achar ou nenhum por qualquer campo da tabela
            var aluno3 = _contexto.Alunos.FirstOrDefault(a => a.Email.Contains("GlauberCasttro@gmail.com"));
            // var aluno3 = _contexto.Alunos.FirstOrDefault(a => a.Email == "GlauberCasttro@gmail.com");


            //retorna uma coleção de alunos onde o nome é igual a glauber
            //dentro do Where está sendo passado um predicado para filtrar a busca
            var aluno4 = _contexto.Alunos.Where(a => a.Nome == "Glauber");

            //Atualizando
            aluno.Nome = "Joao";
            _contexto.Alunos.Update(aluno);
            _contexto.SaveChanges();


            //Removendo
            //o metodo remove é feito atraves da entidade 
            //se nao tiver a entidade é ncessario buscar no banco depois remover
            _contexto.Alunos.Remove(aluno);
            _contexto.SaveChanges();


            //Considerações sobre entiti
            return View();
        }
    }
}