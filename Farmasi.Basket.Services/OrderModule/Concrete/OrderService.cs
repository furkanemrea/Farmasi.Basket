using AutoMapper;
using Farmasi.Basket.Common.Concrete;
using Farmasi.Basket.Data.Abstraction;
using Farmasi.Basket.Data.Models;
using Farmasi.Basket.Services.CartModule.Abstraction;
using Farmasi.Basket.Services.OrderModule.Abstraction;
using Farmasi.Basket.Services.OrderModule.Concrete.Models.RequestModels;
using Farmasi.Basket.Services.OrderModule.Concrete.Models.ResponseModels;
using Farmasi.Basket.Services.OrderModule.Concrete.Validators;
using Farmasi.Basket.Services.Publisher.Abstraction;
using Farmasi.Basket.Services.Publisher.Concrete.Models;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Services.OrderModule.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IPublisherService _publisherService;
        private readonly IOrderRepository _orderRepository;
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IConfiguration configuration, IPublisherService publisherService, IMapper mapper, ICartService cartService)
        {
            _orderRepository = orderRepository;
            _publisherService = publisherService;
            _mapper = mapper;
            _cartService = cartService;
        }

        public async Task<BaseResponse<CreatePaymentResponseModel>> CreatePayment(CreatePaymentRequestModel requestModel)
        {
            requestModel.Validate();

            CreatePaymentValidator createPaymentValidator = new CreatePaymentValidator();

            string hashKey = Util.GetCartHashKey(requestModel.UserId.ToString());

            var serializedObjectResponse = await _cartService.GetProductOfCartFromRedis(requestModel.UserId.ToString());

            createPaymentValidator.ThrowIfProductsIsNull(serializedObjectResponse.Data);

            var productOfCardModel = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductOfCartModel>>(serializedObjectResponse.Data);

            createPaymentValidator.ThrowIfProductsCountLessThanOne(productOfCardModel);

            EmailMessageModel emailMessageModel = new()
            {
                Email = "dummy@gmail.com",
                UserId = requestModel.UserId.ToString()
            };

            var products = _mapper.Map<List<Product>>(productOfCardModel);

            await _orderRepository.Add(new Data.Models.Order()
            {
                Products = products,
                UserId = requestModel.UserId.ToString()
            });

            await _publisherService.Send(emailMessageModel.ToString());

            await _cartService.ClearBasket(requestModel.UserId.ToString());

            return BaseResponse<CreatePaymentResponseModel>.Builder().SetSuccessCode().Build();
        }
    }
}
