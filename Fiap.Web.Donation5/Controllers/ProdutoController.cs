using Fiap.Web.Donation5.Controllers.Filters;
using Fiap.Web.Donation5.Data;
using Fiap.Web.Donation5.Models;
using Fiap.Web.Donation5.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fiap.Web.Donation5.Controllers
{
    [Autenticado]
    public class ProdutoController : BaseController
    {
        private readonly ProdutoRepository _produtoRepository;
        private readonly CategoriaRepository _categoriaRepository;

        public ProdutoController(DataContext context)
        {
            _produtoRepository = new ProdutoRepository(context);
            _categoriaRepository = new CategoriaRepository(context);
        }


        [HttpGet]
        public IActionResult Index()
        {
            // SELECT * FROM prod ...
            // Armazenar o SELECT
            // Enviar para a View

            var produtos = _produtoRepository.FindAllAvailableWithCategoriaAndUsuario();
            return View(produtos);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var produto = _produtoRepository.FindById(id);

            LoadCategoriasCombo();

            return View(produto);
        }

        [HttpPost]
        public IActionResult Edit(ProdutoModel produtoModel)
        {
            if (string.IsNullOrEmpty(produtoModel.Descricao))
            {
                produtoModel.UsuarioId = UsuarioLogado.UsuarioId;

                ViewBag.ErrorMessage = "A descrição é requerida";
                return View(produtoModel);
            } else
            {
                _produtoRepository.Update(produtoModel);

                produtoModel.UsuarioId = UsuarioLogado.UsuarioId;
                _produtoRepository.Update(produtoModel);
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
            LoadCategoriasCombo();
            return View(new ProdutoModel());
        }

        [HttpPost]
        public IActionResult Create(ProdutoModel produtoModel)
        {

            produtoModel.UsuarioId = UsuarioLogado.UsuarioId;

            if (ModelState.IsValid)
            {
                _produtoRepository.Insert(produtoModel);
                TempData["SuccessMessage"] = $"O produto {produtoModel.NomeProduto} foi cadastrado com sucesso";
                return RedirectToAction(nameof(Index));
            } 
            else
            {
                LoadCategoriasCombo();
                return View(new ProdutoModel());
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

        private void LoadCategoriasCombo()
        {
            var categorias = _categoriaRepository.FindAll();
            var selectCategorias = new SelectList(categorias, "CategoriaId", "NomeCategoria");
            ViewBag.Categorias = selectCategorias;
        }

    }
}
