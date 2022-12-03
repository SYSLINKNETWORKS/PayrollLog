using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TWP_API_Log.Helpers;
using TWP_API_Log.ViewModels;

namespace TWP_API_Log.Generic
{
    public class AbsBusiness : IFacade<object>
    {
        public virtual Task<ApiResponse> GetDataAsync(LoginInfoViewModel _LoginInfoViewModel)
        {
            throw new NotImplementedException();
        }
        public virtual Task<ApiResponse> GetDataByIdAsync(Guid _Id, LoginInfoViewModel _LoginInfoViewModel)
        {
            throw new NotImplementedException();
        }

        public virtual Task<ApiResponse> AddAsync(object model)
        {
            throw new NotImplementedException();
        }

        public virtual Task<ApiResponse> UpdateAsync(object model)
        {
            throw new NotImplementedException();
        }


    }
}