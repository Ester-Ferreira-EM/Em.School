using EM.Domain;
using EM.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EM.View.Controllers
{
    public class HomeController(IRepositorioAbstrato<Aluno> alunoRepositorio, IRepositorioAluno<Aluno> repositorioAluno) : Controller
    {
            private readonly IRepositorioAbstrato<Aluno> _alunoRepositorio = alunoRepositorio;
            private readonly IRepositorioAluno<Aluno> _repositorioAluno = repositorioAluno;

        public IActionResult TabelaAlunos()
        {
            IEnumerable<Aluno> listaAluno = _alunoRepositorio.GetAll();
            return View(listaAluno);
        }

        [HttpPost]
        public ActionResult Search(string searchTerm, string searchType)
        {
            if (searchType != null)
            {
                if (searchType.Equals("matricula", StringComparison.CurrentCultureIgnoreCase) && int.TryParse(searchTerm, out int matricula))
                {
                    Aluno aluno = _repositorioAluno.GetByMatricula(matricula);
                    Console.WriteLine($"Aluno encontrado: {aluno}");
                    IEnumerable<Aluno> alunos = aluno != null ? new List<Aluno> { aluno } : [];
                    return View("TabelaAlunos", alunos);
                }
                else if (searchType.Equals("nome", StringComparison.CurrentCultureIgnoreCase))
                {
                    IEnumerable<Aluno> alunos = _repositorioAluno.GetByContendoNoNome(searchTerm);
                    return View("TabelaAlunos", alunos);
                }
            }
            return View(new List<Aluno>());
        }
    }
}
