using System.Collections.Generic;
using System.Web.Mvc;
using CaelumEstoque.DAO;
using CaelumEstoque.Models;

namespace CaelumEstoque.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Index()
        {
            ProdutosDAO dao = new ProdutosDAO();
            IList<Produto> produtos = dao.Lista();

            ViewBag.Produtos = produtos;

            return View();
        }

        public ActionResult Form()
        {
            ListarCategorias();
            ViewBag.Produto = new Produto();
            return View();
        }

        private void ListarCategorias()
        {
            CategoriasDAO categoriasDAO = new CategoriasDAO();
            IList<CategoriaDoProduto> categorias = categoriasDAO.Lista();

            ViewBag.Categorias = categorias;
        }

        [HttpPost]
        public ActionResult Adiciona(Produto produto)
        {
            int idInformatica = 1;
            if (produto.CategoriaId.Equals(idInformatica) && produto.Preco < 100)
            {
                ModelState.AddModelError("produto.Invalido", "Informatica com preço abaixo de 100 reais.");
            }
            if (ModelState.IsValid)
            {
                ProdutosDAO dao = new ProdutosDAO();
                dao.Adiciona(produto);
                return RedirectToAction("Index", "Produto");
            }
            else
            {
                ListarCategorias();
                ViewBag.Produto = produto;
                return View("Form");
            }
        }

        public ActionResult Visualiza(int produtoId)
        {
            ProdutosDAO dao = new ProdutosDAO();
            Produto produto = dao.BuscaPorId(produtoId);

            return View(produto);
        }
    }
}