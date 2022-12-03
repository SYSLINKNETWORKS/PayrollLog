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
    public class AuditProcessor : IProcessor<AuditBaseModel>
    {
        private DataContext _context;
        private AbsBusiness _AbsBusiness;
        SecurityHelper _SecurityHelper = new SecurityHelper();

        public AuditProcessor(App_Data.DataContext context)
        {
            _context = context;
            _AbsBusiness = Builder.MakeBusinessClass(Enums.ClassName.AuditLog, _context);
        }
        public async Task<ApiResponse> ProcessPost(object request)
        {
            ApiResponse apiResponse = new ApiResponse();
            if (_AbsBusiness != null)
            {
                var _request = (AuditViewAddModel)request;


                var _ServiceTable = await _context.ServiceTables.Where(x => x.Id == _request.Key).FirstOrDefaultAsync();//.FirstOrDefaultAsync();
                if (_ServiceTable == null)
                {
                    apiResponse.statusCode = StatusCodes.Status401Unauthorized.ToString();
                    apiResponse.message = "Invalid Key";
                    return apiResponse;
                }


                var _Table = new AuditTable
                {
                    MenuId = _request.MenuId,
                    MenuAlias = _request.MenuAlias,
                    MenuName = _request.MenuName,
                    Action = _request.Action,
                    ActionName = _request.Action == Enums.Operations.A.ToString() ? "Insert Record" : _request.Action == Enums.Operations.E.ToString() ? "Edit Record" : _request.Action == Enums.Operations.D.ToString() ? "Delete Record" : _request.Action == Enums.Operations.G.ToString() ? "Get All Record" : _request.Action == Enums.Operations.I.ToString() ? "Get By Id Record" : _request.Action,
                    UserNameInsert = _request.UserName,
                    Date = DateTime.UtcNow.Date,
                    InsertDate = DateTime.UtcNow,
                    NewValue = _request.NewValue,
                    OldValue = _request.OldValue,


                };
                return await _AbsBusiness.AddAsync(_Table);


            }
            return null;
        }
        public Task<ApiResponse> ProcessGet(string _TokenString)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> ProcessGetById(Guid _Id, string _TokenString)
        {
            throw new NotImplementedException();
        }



        public Task<ApiResponse> ProcessPut(object request)
        {
            throw new NotImplementedException();
        }
    }
}