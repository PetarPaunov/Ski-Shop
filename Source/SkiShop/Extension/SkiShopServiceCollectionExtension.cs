namespace Microsoft.Extensions.DependencyInjection
{
    using SkiShop.Data.Common;
    using SkiShop.Core.Services;
    using SkiShop.Core.Contracts;
    using SkiShop.Core.Services.Email;
    using SkiShop.Core.Services.Admin;
    using SkiShop.Core.Contracts.Email;
    using SkiShop.Core.Services.Common;
    using SkiShop.Core.Contracts.Admin;
    using SkiShop.Core.Contracts.Common;
    using SkiShop.Core.Services.ShoppingCart;
    using SkiShop.Core.Contracts.ShoppingCart;
    using SkiShop.Core.Services.ProductServices;
    using SkiShop.Core.Contracts.ProductContracts;

    public static class SkiShopServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IProductServiceAdmin, ProductServiceAdmin>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<IUserServiceAdmin, UserServiceAdmin>();
            services.AddScoped<IRoleServiceAdmin, RoleServiceAdmin>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}