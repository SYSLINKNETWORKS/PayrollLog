
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TWP_API_Log.Helpers;
using TWP_API_Log.ViewModels;

namespace TWP_API_Log.Generic
{
    public interface IFacade<T>
    {
        Task<ApiResponse> GetDataAsync(LoginInfoViewModel _LoginInfoViewModel);
        Task<ApiResponse> GetDataByIdAsync(Guid _Id, LoginInfoViewModel _LoginInfoViewModel);
        Task<ApiResponse> AddAsync(T model);
    }
}