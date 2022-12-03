using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TWP_API_Log.Helpers;

namespace TWP_API_Log.Processor
{
    public interface IProcessor<T>
    {
        Task<ApiResponse> ProcessGet(string _TokenString);
        Task<ApiResponse> ProcessGetById(Guid _Id, string _TokenString);
        Task<ApiResponse> ProcessPost(object request);

        Task<ApiResponse> ProcessPut(object request);
    }
}