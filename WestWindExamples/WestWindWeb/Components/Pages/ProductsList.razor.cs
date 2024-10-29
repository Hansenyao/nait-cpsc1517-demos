using Microsoft.AspNetCore.Components;
using WestWindLibrary.BLL;
using WestWindLibrary.Entities;

namespace WestWindWeb.Components.Pages
{
    public partial class ProductsList
    {
        private List<Product> products = [];
        private List<string> errorMsgs = [];

        [Inject]
        ProductServices _productServices { get; set; }

        protected override void OnInitialized()
        {
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
    }
}
