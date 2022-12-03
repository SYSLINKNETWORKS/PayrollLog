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
    public class BService : AbsBusiness
    {
        private readonly DataContext _context;


        public BService(DataContext context)
        {
            _context = context;
        }

        public override async Task<ApiResponse> GetDataAsync(LoginInfoViewModel _LoginInfoViewModel)
        {
            var ApiResponse = new ApiResponse();
            try
            {

                var _Table = await _context.ServiceTables.Where(a => a.Active == true).OrderBy(x=>x.ServiceName).ToListAsync();

                if (_Table == null)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString(); ;
                    ApiResponse.message = "Record Not Found";
                    return ApiResponse;
                }

                if (_Table.Count == 0)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString(); // "401";
                    ApiResponse.message = "Record Not Found";
                    return ApiResponse;
                }
                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.data = _Table;
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
        public override async Task<ApiResponse> GetDataByIdAsync(Guid _Id, LoginInfoViewModel _LoginInfoViewModel)
        {
            var ApiResponse = new ApiResponse();
            try
            {

                var _Table = await _context.ServiceTables.Where(a => a.Id == _Id && a.Active == true).FirstOrDefaultAsync();

                if (_Table == null)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    ApiResponse.message = "Record not found";
                    return ApiResponse;
                }
                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.data = _Table;
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
        public override async Task<ApiResponse> AddAsync(object model)
        {
            var ApiResponse = new ApiResponse();
            try
            {


                var _model = (ServiceTable)model;
                _context.ServiceTables.Add(_model);
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


        public override async Task<ApiResponse> UpdateAsync(object model)
        {
            var ApiResponse = new ApiResponse();
            try
            {


                var _model = (ServiceTable)model;
                _context.ServiceTables.Update(_model);
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