using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }


        private ApiResponse() { }
        public static ApiResponse<T> SuccessReponse(int statusCode, string? message, T? data)
        {
            return new ApiResponse<T>()
            {
                Success = true,
                StatusCode = statusCode,
                Message = message,
                Data = data

            };
        }
        public static ApiResponse<T> FailReponse(int statusCode, string? message, List<string>? errors)
        {
            return new ApiResponse<T>()
            {
                Success = false,
                StatusCode = statusCode,
                Message = message,
                Errors = errors

            };
        }

    };
 
    
}
