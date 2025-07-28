using Fiap.Web.Donation5.Data;
using Fiap.Web.Donation5.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.Donation5.Repository
{
    public class ProdutoRepository
    {

        private readonly DataContext _dataContext;

        public ProdutoRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ProdutoModel FindById(int id)
        {

            return _dataContext.Produtos.AsNoTracking().SingleOrDefault(p => p.ProdutoId == id);
        }

        public List<ProdutoModel> FindAll()
        {
            var produtos = _dataContext.Produtos.AsNoTracking().ToList();

            return produtos ?? new List<ProdutoModel>();
        }

        public List<ProdutoModel> FindAllWithCategoriaAndUsuario()
        {
            var produtos = _dataContext.Produtos.AsNoTracking()
                                .Include(c => c.Categoria) // INNER JOIN                                   
                                .Include(u => u.Usuarios)   // INNER JOIN 
                                .ToList();

            return produtos ?? new List<ProdutoModel>();
        }

        public List<ProdutoModel> FindAllWithCategoriaAndUsuarioByName(string nomeParcial)
        {
            var produtos = _dataContext.Produtos.AsNoTracking()
                                .Where(p => p.NomeProduto.ToLower().Contains(nomeParcial.ToLower()))
                                .Include(c => c.Categoria) // INNER JOIN                                   
                                .Include(u => u.Usuarios)   // INNER JOIN 
                                .ToList();

            return produtos ?? new List<ProdutoModel>();
        }

        public List<ProdutoModel> FindAllAvailableWithCategoriaAndUsuario()
        {
            var produtos = _dataContext.Produtos.AsNoTracking()
                                .Where(p=> p.Disponivel == true && p.DataExpiracao >= DateTime.UtcNow)
                                .Include(c => c.Categoria) // INNER JOIN                                   
                                .Include(u => u.Usuarios)   // INNER JOIN 
                                .ToList();

            return produtos ?? new List<ProdutoModel>();
        }

        public List<ProdutoModel> FindAllAvailableWithCategoriaAndUsuarioByUserId(int userId)
        {
            var produtos = _dataContext.Produtos.AsNoTracking()
                                .Where(p => p.Disponivel == true &&
                                            p.DataExpiracao >= DateTime.UtcNow &&
                                            p.UsuarioId == userId)
                                .Include(c => c.Categoria) // INNER JOIN                                   
                                .Include(u => u.Usuarios)   // INNER JOIN 
                                .ToList();

            return produtos ?? new List<ProdutoModel>();
        }

        public List<ProdutoModel> FindAllAvailableForChange(int userId)
        {
            var produtos = _dataContext.Produtos.AsNoTracking()
                                .Where(p => p.Disponivel == true &&
                                            p.DataExpiracao >= DateTime.UtcNow &&
                                            p.UsuarioId != userId)
                                .Include(c => c.Categoria) // INNER JOIN                                   
                                .Include(u => u.Usuarios)   // INNER JOIN 
                                .ToList();

            return produtos ?? new List<ProdutoModel>();
        }

        public int Insert(ProdutoModel produtoModel)
        {
            _dataContext.Produtos.Add(produtoModel);
            _dataContext.SaveChanges();

            return produtoModel.ProdutoId;
        }

        public void Update(ProdutoModel produtoModel)
        {
            _dataContext.Produtos.Update(produtoModel);
            _dataContext.SaveChanges();
        }

        public void Delete(int id)
        {
            //var categoria = new CategoriaModel()
            //{
            //    CategoriaId = id
            //};

            var produto = FindById(id);
            _dataContext.Produtos.Remove(produto);
            _dataContext.SaveChanges();
        }
    }
}
