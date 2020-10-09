using orcamento_crud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace orcamento_crud.Interface
{
    public interface IProduct
    {
       public IEnumerable<Product> GetAllProducts();
       public void AddProduct(Product product);
       public void DeleteProduct(int? id);
       public void UpdateProduct(Product product);
       public Product GetProduct(int? id);
       
    }
}
