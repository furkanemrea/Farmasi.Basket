using Farmasi.Basket.Data.Abstraction;
using Farmasi.Basket.Data.Context;
using Farmasi.Basket.Data.Repositories;
using Farmasi.Basket.Services.CartModule.Abstraction;
using Farmasi.Basket.Services.CartModule.Concrete;
using Farmasi.Basket.Services.ProductModule.Abstraction;
using Farmasi.Basket.Services.ProductModule.Concrete;
using Farmasi.Basket.Services.Publisher.Abstraction;
using Farmasi.Basket.Services.Publisher.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Services.Configuration
{
    public static class ServiceRegistiration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMongoContext, MongoContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IPublisherService, PublisherService>();
        }
    }
}
