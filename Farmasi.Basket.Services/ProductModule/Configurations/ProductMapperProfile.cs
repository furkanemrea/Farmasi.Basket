using AutoMapper;
using Farmasi.Basket.Data.Models;
using Farmasi.Basket.Services.ProductModule.Models.Response;
using Farmasi.Basket.Services.Service.Product.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Services.ProductModule.Configurations
{
    public class ProductMapperProfile : Profile
    {
        public ProductMapperProfile()
        {
            CreateMap<CreateProductRequestModel, Product>();
            CreateMap<Product, ProductDetailModel>();
        }
    }
}
