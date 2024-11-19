using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestWindLibrary.BLL;
using WestWindLibrary.DAL;

namespace WestWindLibrary
{
    public static class WestWindExtensions
    {
        //Constructor for the ExtensionServices
        public static void WestWindExtensionServices(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContext<WestWindContext>(options);

            services.AddScoped<ProductServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();
                return new ProductServices(context);
            });

            services.AddScoped<CategoryServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();
                return new CategoryServices(context);
            });

            services.AddScoped<SupplierServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WestWindContext>();
                return new SupplierServices(context);
            });
        }
    }
}
