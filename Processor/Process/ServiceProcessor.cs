using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TWP_API_Log.App_Data;
using TWP_API_Log.Generic;
using TWP_API_Log.Helpers;
using TWP_API_Log.Models;
using TWP_API_Log.ViewModels;
using Microsoft.AspNetCore.Http;

namespace TWP_API_Log.Processor
{
    public class ServiceProcessor : IProcessor<ServiceBaseModel>
    {
        private DataContext _context;
        private AbsBusiness _AbsBusiness;
        SecurityHelper _SecurityHelper = new SecurityHelper();

        public ServiceProcessor(App_Data.DataContext context)
        {
            _context = context;
            _AbsBusiness = Builder.MakeBusinessClass(Enums.ClassName.ServiceTable, _context);
        }

        public async Task<ApiResponse> ProcessGet(string _TokenString)
        {
            if (_AbsBusiness != null)
            {
                ApiResponse apiResponse = _SecurityHelper.UserInfo(_TokenString).GetAwaiter().GetResult();
                if (apiResponse.statusCode != StatusCodes.Status200OK.ToString())
                {
                    return apiResponse;
                }
                LoginInfoViewModel _LoginInfoViewModel = (LoginInfoViewModel)apiResponse.data;

                var response = await _AbsBusiness.GetDataAsync(_LoginInfoViewModel);

                if (Convert.ToInt32(response.statusCode) == 200)
                {
                    var _Table = (IEnumerable<ServiceTable>)response.data;
                    var result = (from ViewTable in _Table
                                  select new ServiceViewModel
                                  {
                                      Id = ViewTable.Id,
                                      ServiceName = ViewTable.ServiceName,
                                      Active = ViewTable.Active,
                                  }).ToList();
                    response.data = result;
                }


                return response;
            }
            return null;
        }

        public async Task<ApiResponse> ProcessGetById(Guid _Id, string _TokenString)
        {
            if (_AbsBusiness != null)
            {
                ApiResponse apiResponse = _SecurityHelper.UserInfo(_TokenString).GetAwaiter().GetResult();
                if (apiResponse.statusCode != StatusCodes.Status200OK.ToString())
                {
                    return apiResponse;
                }
                LoginInfoViewModel _LoginInfoViewModel = (LoginInfoViewModel)apiResponse.data;


                var response = await _AbsBusiness.GetDataByIdAsync(_Id, _LoginInfoViewModel);
                if (Convert.ToInt32(response.statusCode) == 200)
                {
                    var _Table = (ServiceTable)response.data;
                    var _ViewModel = new ServiceViewByIdModel
                    {
                        Id = _Table.Id,
                        ServiceName = _Table.ServiceName,
                        Active = _Table.Active,
                    };
                    response.data = _ViewModel;
                }

                return response;
            }
            return null;
        }

        public async Task<ApiResponse> ProcessPost(object request)
        {
            if (_AbsBusiness != null)
            {

                var _request = (ServiceViewAddModel)request;
                var _Table = new ServiceTable
                {
                    ServiceName = _request.ServiceName,
                    Active = _request.Active,
                };
                return await _AbsBusiness.AddAsync(_Table);


            }
            return null;
        }

        public async Task<ApiResponse> ProcessPut(object request)
        {
            ApiResponse apiResponse = new ApiResponse();

            if (_AbsBusiness != null)
            {
                var _request = (ServiceViewUpdateModel)request;



                var _Table = new ServiceTable
                {
                    Id = _request.Id,
                    ServiceName = _request.ServiceName,
                    Active = _request.Active
                };
                return await _AbsBusiness.UpdateAsync(_Table);
            }
            apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
            apiResponse.message = "Invalid Class";
            return apiResponse;

        }


    }
}