using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestWindLibrary.DAL;
using WestWindLibrary.Entities;

namespace WestWindLibrary.BLL
{
    public class ProductServices
    {
        private readonly WestWindContext _context;

        // Create a constructor that take in the context (map to the database)
        public ProductServices(WestWindContext context)
        {
            _context = context;
        }

        #region Quries
        public List<Product> GetAllProducts()
        {
            // JOIN tables
            // Address is only Included as an example.
            // We can only include a full entity!! DO NOT Try and include a field, this will break
            return _context.Products.Include(p => p.Category).Include(x => x.Supplier)
                .ThenInclude(y => y.Address).ToList();
            //return _context.Products.ToList();
        }
        public List<Product> GetProducts_ByCategory(int categoryId)
        {
            return _context.Products.Where(x => x.CategoryID.Equals(categoryId)).ToList();
        }
        #endregion // Quries
    }
}
