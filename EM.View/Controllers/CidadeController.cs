using EM.Domain;
using EM.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EM.View.Controllers
{
    public class CidadeController(IRepositorioAbstrato<Aluno> alunoRepositorio, IRepositorioAbstrato<Cidade> _cidadeRepositorio) : Controller
    {
        private readonly IRepositorioAbstrato<Aluno> _alunoRepositorio = alunoRepositorio;
        private readonly IRepositorioAbstrato<Cidade> _cidadeRepositorio = _cidadeRepositorio;
        public IActionResult TabelaCidades()
        {
            IEnumerable<Cidade> listaCidades = _cidadeRepositorio.GetAll();
            return View(listaCidades);
        }

        public IActionResult CadastraCidade(int? id)
        {
            if (id != null)
            {
                Cidade? cidade = _cidadeRepositorio.Get(c => c.Id == id).FirstOrDefault();

                ViewBag.IsEdicao = true;
                return View(cidade);
            }
            ViewBag.IsEdicao = false;
            return View(new Cidade());

        }

        [HttpPost]
        public IActionResult CadastraCidade(Cidade cidade)
        {
            if (ModelState.IsValid)
            {
                if (cidade.Id > 0)
                {
                    _cidadeRepositorio.Update(cidade);
                }
                else
                {
                    _cidadeRepositorio.Add(cidade);
                }

                return RedirectToAction("TabelaCidades");
            }
            ViewBag.IsEdicao = cidade.Id > 0;
            ViewBag.Cidades = _cidadeRepositorio.GetAll().ToList();
            return View(cidade);
        }

    }
}
