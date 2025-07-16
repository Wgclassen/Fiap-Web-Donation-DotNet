using Fiap.Web.Donation5.Data;
using Fiap.Web.Donation5.Models;
using Fiap.Web.Donation5.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Web.Donation5.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoRepository _produtoRepository;

        public ProdutoController(DataContext context)
        {
            _produtoRepository = new ProdutoRepository(context);
        }


        [HttpGet]
        public IActionResult Index()
        {
            // SELECT * FROM prod ...
            // Armazenar o SELECT
            // Enviar para a View

            var produtos = _produtoRepository.FindAll();
            return View(produtos);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var produto = _produtoRepository.FindById(id);

            return View(produto);
        }

        [HttpPost]
        public IActionResult Edit(ProdutoModel produtoModel)
        {
            if (string.IsNullOrEmpty(produtoModel.Descricao))
            {
                ViewBag.ErrorMessage = "A descrição é requerida";
                return View(produtoModel);
            } else
            {

                TempData["SuccessMessage"] = $"O produto {produtoModel.NomeProduto} foi alterado com sucesso";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var produto = _produtoRepository.FindById(id);

            return View(produto);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ProdutoModel());
        }

        [HttpPost]
        public IActionResult Create(ProdutoModel produtoModel)
        {

            if (string.IsNullOrEmpty(produtoModel.Descricao))
            {
                ViewBag.ErrorMessage = "A descrição é requerida";
                return View(produtoModel);
            }
            else
            {
                TempData["SuccessMessage"] = $"O produto {produtoModel.NomeProduto} foi cadastrado com sucesso";
                _produtoRepository.Insert(produtoModel);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var produto = _produtoRepository.FindById(id);

            TempData["SuccessMessage"] = $"O produto {produto!.NomeProduto} foi removido com sucesso";
            return RedirectToAction(nameof(Index));
        }

        private List<ProdutoModel> ListarProdutosMock()
        {
            // SELECT * FROM produtos ...
            var produtos = new List<ProdutoModel>{
        new ProdutoModel()
        {
            ProdutoId = 1,
            NomeProduto = "Iphone 11",
            CategoriaId = 1,
            Disponivel = true,
            DataExpiracao = DateTime.Now,
        },
        new ProdutoModel()
        {
            ProdutoId = 2,
            NomeProduto = "Iphone 12",
            CategoriaId = 2,
            Disponivel = true,
            DataExpiracao = DateTime.Now,
        },
        new ProdutoModel()
        {
            ProdutoId = 3,
            NomeProduto = "Iphone 13",
            CategoriaId = 1,
            Disponivel = true,
            DataExpiracao = DateTime.Now,
        },
        new ProdutoModel()
        {
            ProdutoId = 4,
            NomeProduto = "Iphone 14",
            CategoriaId = 1,
            Disponivel = false,
            DataExpiracao = DateTime.Now,
        },
    };

            return produtos;

        }

    }
}
