using Microsoft.AspNetCore.Components;
using WestWindLibrary.BLL;
using WestWindLibrary.Entities;

namespace WestWindWeb.Components.Pages
{
    public partial class ProductsList
    {
        private List<Product> products = [];
        private List<string> errorMsgs = [];
        private List<Category> categories = [];
        private int categoryId;
        private bool noProducts;

        [Inject]
        ProductServices _productServices { get; set; }
        [Inject]
        CategoryServices _categoryServices { get; set; }

        protected override void OnInitialized()
        {
            // Every time you connect or use one your servieces you need a try/catch
            try
            {
                categories = _categoryServices.GetCategories();
            }
            catch (Exception ex)
            {
                errorMsgs.Add($"Data exception: {ex.Message}");
            }

            try
            {
                products = _productServices.GetAllProducts();
            }
            catch (Exception ex)
            {
                errorMsgs.Add($"Data exception: {ex.Message}");
            }
            base.OnInitialized();
        }
        private void LoadProductsByCateory()
        {
            errorMsgs.Clear();
            noProducts = false;

            if (categoryId == 0)
            {
                errorMsgs.Add("Please select a category to search by.");
                return;
            }

            try
            {
                products = _productServices.GetProducts_ByCategory(categoryId);
                if (products.Count == 0)
                {
                    noProducts = true;
                }
            }
            catch (Exception ex)
            {
                errorMsgs.Add($"Data exception: {ex.Message}");
            }
        }
    }
}
