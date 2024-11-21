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
            return _context.Products.Where(x => x.ProductID == productId).Include(p => p.Supplier).Include(p => p.Category).FirstOrDefault();
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

        // For a delete we always again return an int. This is not a identifier,this is how
        // many rows were affected by the delete.
        public int Product_PhysicalDelete(Product product)
        {
            // Physical Delete
            // Remove the record from the database
            // If there are child records to prevent the record removal
            //      you must remove the child first.
            //      Cascade the Delete
            //      Database can have cascade deletes set up in the database, this is not always this case
            // If there are child records AND the child record are required
            //      You cannot delete the record!
            //      Potential Business rules
            if (product == null)
            {
                throw new ArgumentNullException("You must supply the product information.");
            }

            // Need to make sure the data exist in the database
            // Return the product if it exists, or null if it does not
            Product? exist = _context.Products.FirstOrDefault(x => x.ProductID == product.ProductID);
            if (exist == null)
            {
                throw new ArgumentException($"Product {product.ProductName} (ID: {product.ProductID}) is no longer in the database.");
            }

            // Potential Check Needed
            // Check if Child Records exists, this is genernally good practice.
            int existingChildren = exist.ManifestItems.Count;
            existingChildren += exist.OrderDetails.Count;

            // Example Business Rule
            // If a child record exists you cannot delete the product
            // If an order contained this product, it cannot be delete.
            if (existingChildren > 0)
            {
                throw new ArgumentException($"Product {product.ProductName} (ID: {product.ProductID}) has child records in the database.");
            }

            // Staging
            EntityEntry<Product> deleting = _context.Entry(product);
            deleting.State = EntityState.Deleted;

            // Commit
            // The returned value from the database for a physical delete is the number of affected rows
            return _context.SaveChanges();
        }

        // Logical Delete - Discontinue the product
        public int Product_LogicalDelete(Product product)
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

            // Can have business rule logic for a logical delete
            //      Ex: Cannot discontinue where there is an active order, etc.

            // We need to change the record to make the logical delete true(or false depending on the record)
            //      Ex: Discontined = true OR Active = false
            product.Discontinued = true;

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
