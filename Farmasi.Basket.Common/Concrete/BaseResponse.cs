using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Common.Concrete
{
    public class BaseResponse<T>
    {
        public int Code { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public BaseResponse()
        {

        }

        public BaseResponse(int code, string message, T data)
        {
            this.Code = code;
            this.Message = message;
            this.Data = data;
        }

        public bool IsSuccess()
        {
            return this.Code == StatusCodes.Status200OK;
        }
        public static BaseResponseBuilder<T> Builder()
        {
            return new BaseResponseBuilder<T>();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public class BaseResponseBuilder<T>
        {
            public int Code { get; set; }
            public string? Message { get; set; }
            public T? Data { get; set; }

            public BaseResponseBuilder<T> SetData(T data)
            {
                this.Data = data;
                return this;
            }
            public BaseResponseBuilder<T> SetMessage(string message)
            {
                this.Message = message;
                return this;
            }
            public BaseResponseBuilder<T> SetCode(int code)
            {
                this.Code = code;
                return this;
            }
            public BaseResponseBuilder<T> SetSuccessCode()
            {
                this.Code = StatusCodes.Status200OK;
                return this;
            }
            public BaseResponseBuilder<T> SetErrorCode()
            {
                this.Code = StatusCodes.Status400BadRequest;
                return this;
            }

            public BaseResponse<T> Build()
            {
                return new BaseResponse<T>()
                {
                    Data = this.Data,
                    Message = this.Message,
                    Code = this.Code
                };
            }
        }
    }
}
