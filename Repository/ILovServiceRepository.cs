using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using TWP_API_Log.App_Data;
using TWP_API_Log.Controllers;
using TWP_API_Log.Generic;
using TWP_API_Log.Helpers;
using TWP_API_Log.ViewModels;

namespace TWP_API_Log.Repository
{
    public interface ILovServiceRepository
    {

        Task<ApiResponse> GetServicesLovAsync(ClaimsPrincipal _User, string _Search);
        Task<ApiResponse> GetAuditUserAsync(ClaimsPrincipal _User, string _Search);
        Task<ApiResponse> GetAuditActionAsync(ClaimsPrincipal _User, string _Search);
        Task<ApiResponse> GetAuditMenuAsync(ClaimsPrincipal _User, string _Search);
    }

    public class LovServiceRepository : ILovServiceRepository
    {
        private readonly DataContext _context = null;
        SecurityHelper _SecurityHelper = new SecurityHelper();

        public LovServiceRepository(DataContext context)
        {
            _context = context;
        }




        public async Task<ApiResponse> GetServicesLovAsync(ClaimsPrincipal _User, string _Search)
        {
            ApiResponse ApiResponse = new ApiResponse();

            try
            {
                var _Table = await (from Table in _context.ServiceTables.Where(a => a.Active == true && (string.IsNullOrEmpty(_Search) ? true : a.ServiceName.Contains(_Search)))
                                    select new LovServicesViewModel
                                    {
                                        Id = Table.Id,
                                        Name = Table.ServiceName
                                    }).OrderBy(o => o.Name).ToListAsync();

                if (_Table == null)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    ApiResponse.message = "Record not found";
                    return ApiResponse;
                }

                if (_Table.Count == 0)
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

        public async Task<ApiResponse> GetAuditUserAsync(ClaimsPrincipal _User, string _Search)
        {
            ApiResponse ApiResponse = new ApiResponse();

            try
            {
                var _Table = await (from Table in _context.AuditTables.Where(a => (string.IsNullOrEmpty(_Search) ? true : a.UserNameInsert.Contains(_Search)))
                                    select new LovServicesStringViewModel
                                    {
                                        Id = Table.UserNameInsert,
                                        Name = Table.UserNameInsert
                                    }).Distinct().OrderBy(o => o.Name).ToListAsync();

                if (_Table == null)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    ApiResponse.message = "Record not found";
                    return ApiResponse;
                }

                if (_Table.Count == 0)
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
        public async Task<ApiResponse> GetAuditActionAsync(ClaimsPrincipal _User, string _Search)
        {
            ApiResponse ApiResponse = new ApiResponse();

            try
            {
                var _Table = await (from Table in _context.AuditTables.Where(a => (string.IsNullOrEmpty(_Search) ? true : a.Action.Contains(_Search)))
                                    select new LovServicesStringViewModel
                                    {
                                        Id = Table.ActionName,
                                        Name = Table.ActionName
                                    }).Distinct().OrderBy(o => o.Name).ToListAsync();

                if (_Table == null)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    ApiResponse.message = "Record not found";
                    return ApiResponse;
                }

                if (_Table.Count == 0)
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

        public async Task<ApiResponse> GetAuditMenuAsync(ClaimsPrincipal _User, string _Search)
        {
            ApiResponse ApiResponse = new ApiResponse();

            try
            {
                var _Table = await (from Table in _context.AuditTables.Where(a => (string.IsNullOrEmpty(_Search) ? true : a.MenuAlias.Contains(_Search)))
                                    select new LovServicesStringViewModel
                                    {
                                        Id = Table.MenuAlias,
                                        Name = Table.MenuAlias
                                    }).Distinct().OrderBy(o => o.Name).ToListAsync();

                if (_Table == null)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    ApiResponse.message = "Record not found";
                    return ApiResponse;
                }

                if (_Table.Count == 0)
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

    }
}