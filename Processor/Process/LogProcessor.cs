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
using Microsoft.EntityFrameworkCore;

namespace TWP_API_Log.Processor
{
    public class LogProcessor : IProcessor<LogBaseModel>
    {
        private DataContext _context;
        private AbsBusiness _AbsBusiness;
        SecurityHelper _SecurityHelper = new SecurityHelper();

        public LogProcessor(App_Data.DataContext context)
        {
            _context = context;
            _AbsBusiness = Builder.MakeBusinessClass(Enums.ClassName.ErrorLog, _context);
        }
        public async Task<ApiResponse> ProcessGet(string _TokenString)
        {
            if (_AbsBusiness != null)
            {
                ApiResponse apiResponse = (ApiResponse)await _SecurityHelper.UserInfo(_TokenString);
                if (apiResponse.statusCode != StatusCodes.Status200OK.ToString())
                {
                    return apiResponse;
                }
                LoginInfoViewModel _LoginInfoViewModel = (LoginInfoViewModel)apiResponse.data;

                var response = await _AbsBusiness.GetDataAsync(_LoginInfoViewModel);

                if (Convert.ToInt32(response.statusCode) == 200)
                {
                    var _Table = (IEnumerable<TableLog>)response.data;
                    var result = (from ViewTable in _Table
                                  select new LogViewModel
                                  {
                                      Id = ViewTable.Id,
                                      Date = ViewTable.Date,
                                      Description = ViewTable.Description,
                                      StackTrace = ViewTable.StackTrace,
                                      Type = ViewTable.Type,
                                      UserName = _LoginInfoViewModel.UserName,
                                      CompanyName = _LoginInfoViewModel.CompanyName,
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
                    var _Table = (TableLog)response.data;
                    var _ViewModel = new LogViewByIdModel
                    {
                        Id = _Table.Id,
                        Date = _Table.Date,
                        Description = _Table.Description,
                        StackTrace = _Table.StackTrace,
                        Type = _Table.Type,
                        UserName = _LoginInfoViewModel.UserName,
                        CompanyName = _LoginInfoViewModel.CompanyName,
                    };
                    response.data = _ViewModel;
                }

                return response;
            }
            return null;
        }
        public async Task<ApiResponse> ProcessPost(object request)
        {
            ApiResponse apiResponse = new ApiResponse();
            if (_AbsBusiness != null)
            {
                // var _ServiceTable = await _context.ServiceTables.ToListAsync();
                var _request = (LogViewAddModel)request;


                var _ServiceTable = await _context.ServiceTables.Where(x => x.Id == _request.Key).FirstOrDefaultAsync();//.FirstOrDefaultAsync();
                if (_ServiceTable == null)
                {
                    apiResponse.statusCode = StatusCodes.Status401Unauthorized.ToString();
                    apiResponse.message = "Invalid Key";
                    return apiResponse;
                }


                var _Table = new TableLog
                {
                    Date = DateTime.UtcNow,
                    Description = _request.Description,
                    StackTrace = _request.StackTrace,
                    Type = _request.Type,
                    CompanyId = _request.CompanyId,
                    UserId = _request.UserId,
                    Key = _request.Key,

                };
                return await _AbsBusiness.AddAsync(_Table);


            }
            return null;
        }

        public Task<ApiResponse> ProcessPut(object request)
        {
            throw new NotImplementedException();
        }
    }
}