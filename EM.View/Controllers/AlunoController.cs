using EM.Domain;
using EM.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EM.View.Controllers
{
    public class AlunoController(IRepositorioAbstrato<Aluno> alunoRepositorio, IRepositorioAluno<Aluno> repositorioAluno, IRepositorioAbstrato<Cidade> repositorioCidade) : Controller
    {
        private readonly IRepositorioAbstrato<Aluno> _alunoRepositorio = alunoRepositorio;
        private readonly IRepositorioAluno<Aluno> _repositorioAluno = repositorioAluno;
        private readonly IRepositorioAbstrato<Cidade> _repositorioCidade = repositorioCidade;

        public IActionResult CadastraAluno(int? id)
        {
            ViewBag.Cidades = _repositorioCidade.GetAll().ToList();
            if (id != null)
            {
                Aluno? aluno = _alunoRepositorio.Get(c => c.Matricula == id).FirstOrDefault();

                ViewBag.IsEdicao = true;
                return View(aluno);
            }
            ViewBag.IsEdicao = false;
            return View("CadastraAluno");

        }

        [HttpPost]
        public IActionResult CadastraAluno(Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                if (aluno.Matricula > 0)
                {
                    _alunoRepositorio.Update(aluno);
                }
                else
                {
                    _alunoRepositorio.Add(aluno);
                }

                return RedirectToAction("TabelaAlunos", "Home");
            }
            ViewBag.IsEdicao = aluno.Matricula > 0;
            ViewBag.Cidades = _repositorioCidade.GetAll().ToList();
            return View(aluno);
        }

        [HttpPost]
        public IActionResult Delete(Aluno aluno)
        {
            _repositorioAluno.Remove(aluno);
            return RedirectToAction("TabelaAlunos", "Home");
        }
    }
}
