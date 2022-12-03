using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TWP_API_Log.App_Data;
using TWP_API_Log.Generic;
using TWP_API_Log.Helpers;
using TWP_API_Log.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TWP_API_Log.ViewModels;

namespace TWP_API_Log.Bussiness
{
    public class BAudit : AbsBusiness
    {
        private readonly DataContext _context;


        public BAudit(DataContext context)
        {
            _context = context;
        }

        
        
        public override async Task<ApiResponse> AddAsync(object model)
        {
            var ApiResponse = new ApiResponse();
            try
            {
                var _model = (AuditTable)model;
                _context.AuditTables.Add(_model);
                await _context.SaveChangesAsync();


                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.message = _model.Id;
                return ApiResponse;

            }
            catch (Exception e)
            {

                string innerexp = "";
                if (e.InnerException != null)
                {
                    innerexp = " Inner Error : " + e.InnerException.ToString();
                }
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                ApiResponse.message = e.Message.ToString() + innerexp;
                return ApiResponse;
            }
        }


    }
}