using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCoreExemplo.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ASPNETCoreExemplo.Controllers
{
    public class HomeController : Controller
    {
        private readonly MongoDB.Driver.MongoClient mongoDbClient;

        public HomeController(MongoDB.Driver.MongoClient mongoDbClient)
        {
            this.mongoDbClient = mongoDbClient;
        }



        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Add()
        {
            var collection = mongoDbClient.GetDatabase("admin").GetCollection<Aluno>("aluno");
            collection.InsertMany(new[] {
                new Aluno() { Nome = "Aluno17", Idade = 17 },
                new Aluno() { Nome = "Aluno18", Idade = 18 },
                new Aluno() { Nome = "Aluno19", Idade = 19 }
            });
            return Ok("3 alunos adicionados");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            var listaAluno = mongoDbClient.GetDatabase("admin").GetCollection<Aluno>("aluno").AsQueryable().ToList();
            return View(listaAluno);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
