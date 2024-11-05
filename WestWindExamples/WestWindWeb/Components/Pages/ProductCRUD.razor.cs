using WestWindLibrary.Entities;

namespace WestWindWeb.Components.Pages
{
    public partial class ProductCRUD
    {
        private Product product = new Product();
        private string feedback = string.Empty;

        private void OnValidSubmit()
        {
            feedback = "This is valid!";
        }

        private void OnInvalidSubmit()
        {
            feedback = "This is invalid!";
        }
    }
}
