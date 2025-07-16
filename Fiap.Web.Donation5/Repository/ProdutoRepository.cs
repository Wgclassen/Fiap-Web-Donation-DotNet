using Fiap.Web.Donation5.Data;
using Fiap.Web.Donation5.Models;

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

            return _dataContext.Produtos.Find(id);
        }

        public IList<ProdutoModel> FindAll()
        {
            return _dataContext.Produtos.ToList() ?? new List<ProdutoModel>();
        }

        public int Insert(ProdutoModel produtoModel)
        {
            _dataContext.Produtos.Add(produtoModel);
            _dataContext.SaveChanges();

            return produtoModel.CategoriaId;
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
