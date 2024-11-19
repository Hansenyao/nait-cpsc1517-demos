using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        public Product GetProduct_ByProductId(int productId)
        {
            return _context.Products.Where(x => x.ProductID == productId).Include(p => p.SupplierID).Include(p => p.CategoryID).FirstOrDefault();
        }
        #endregion // Quries

        #region  CRUD
        // Add a new product into database
        public int Product_AddProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("You must supply the new prodcut information.");
            }

            bool exists = _context.Products.Any(x => x.SupplierID == product.SupplierID
                                                  && x.ProductName == product.ProductName);
            if (exists)
            {
                throw new ArgumentException("Product already exists, cannot add.");
            }

            //Staging
            //IMPORTANT - Remember this is only local, meaning it is only local in memory not in the database
            //This product will not have an ID
            _context.Add(product);

            //Commit
            //This sends the data to the database
            //The info for the changes or inserted records is updated in our DBSet (Products)
            _context.SaveChanges();

            //Now the new product will have an ID
            return product.ProductID;
        }
        // Update product records in database.
        // Return how many rows have been updated.
        public int Product_UpdateProduct(Product product)
        {
            //Check if you got data
            if (product == null)
            {
                throw new ArgumentNullException("You must supply the new product information.");
            }

            // Need to make sure the data exist in the database
            bool exist = _context.Products.Any(x => x.ProductID == product.ProductID);
            if (!exist)
            {
                throw new ArgumentException($"Product {product.ProductName} (ID: {product.ProductID}) is no longer in the database.");
            }

            // Need to make sure the new product does not match another product
            exist = _context.Products.Any(x => x.SupplierID == product.SupplierID
                                            && x.ProductName == product.ProductName
                                            && x.ProductID != product.ProductID);
            if (exist)
            {
                throw new ArgumentException($"Product {product.ProductName} (ID: {product.ProductID}) alread exists in the database.");
            }

            //Staging
            EntityEntry<Product> updating = _context.Entry(product);
            updating.State = EntityState.Modified;

            //Commit
            //This return will return how many records were updated in the database.
            return _context.SaveChanges();
        }

        #endregion // CRUD
    }
}
