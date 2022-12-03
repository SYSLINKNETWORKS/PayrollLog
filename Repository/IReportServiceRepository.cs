using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TWP_API_Log.App_Data;
using TWP_API_Log.Controllers;
using TWP_API_Log.Generic;
using TWP_API_Log.Helpers;
using TWP_API_Log.ViewModels;

namespace TWP_API_Log.Repository
{
    public interface IReportServiceRepository
    {
        Task<ApiResponse> GetAuditReportAsync(ClaimsPrincipal _User, AuditReportCriteriaViewModel _AuditReportCriteriaViewModels);
        Task<ApiResponse> GetAuditReportDetailAsync(ClaimsPrincipal _User, AuditReportCriteriaViewModel AuditReportCriteriaViewModels);
    }

    public class ReportServiceRepository : IReportServiceRepository
    {
        private readonly DataContext _context = null;
        SecurityHelper _SecurityHelper = new SecurityHelper();

        public ReportServiceRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse> GetAuditReportAsync(ClaimsPrincipal _User, AuditReportCriteriaViewModel _AuditReportCriteriaViewModels)
        {
            ApiResponse ApiResponse = new ApiResponse();

            try
            {
                var _Key = _User.Claims.FirstOrDefault(x => x.Type == Enums.Misc.Key.ToString())?.Value.ToString();


                Guid _MenuId = new Guid(_AuditReportCriteriaViewModels.MenuId);

                ApiResponse apiResponseUser = await _SecurityHelper.UserMenuPermissionAsync(_MenuId, _User);
                if (apiResponseUser.statusCode.ToString() != StatusCodes.Status200OK.ToString()) { return apiResponseUser; }
                var _UserMenuPermissionAsync = (GetUserPermissionViewModel)apiResponseUser.data;

                if (!_UserMenuPermissionAsync.View_Permission)
                {
                    ApiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
                    ApiResponse.message = "Invalid Permission";
                    return ApiResponse;
                }



                DateTime _DtFrom = new DateTime();
                DateTime _DtTo = new DateTime();
                if (!string.IsNullOrEmpty(_AuditReportCriteriaViewModels.DateFrom))
                {

                    _DtFrom = Convert.ToDateTime(_AuditReportCriteriaViewModels.DateFrom);
                    _DtTo = Convert.ToDateTime(_AuditReportCriteriaViewModels.DateTo);

                }


                AuditReportViewModel _AuditReportViewModel = new AuditReportViewModel();
                _AuditReportViewModel.auditReportServicesViewModels = new List<AuditReportServicesViewModel>();

                _AuditReportViewModel.CompanyName = _UserMenuPermissionAsync.CompanyName;
                _AuditReportViewModel.HeadingName = "Audit Report";
                _AuditReportViewModel.UserName = _UserMenuPermissionAsync.UserName;

                var _Table = await (from Table in _context.AuditTables
                                    where
                                     (string.IsNullOrEmpty(_AuditReportCriteriaViewModels.DateFrom) ? true : (Table.Date >= _DtFrom && Table.Date <= _DtTo))
                                    && (string.IsNullOrEmpty(_AuditReportCriteriaViewModels.NewValue) ? true : Table.NewValue.Contains(_AuditReportCriteriaViewModels.NewValue))
                                       && (string.IsNullOrEmpty(_AuditReportCriteriaViewModels.OldValue) ? true : Table.OldValue.Contains(_AuditReportCriteriaViewModels.OldValue))
                                      && (string.IsNullOrEmpty(_AuditReportCriteriaViewModels.UserName) ? true : Table.UserNameInsert.Contains(_AuditReportCriteriaViewModels.UserName))
                                      && (string.IsNullOrEmpty(_AuditReportCriteriaViewModels.MenuAlias) ? true : Table.MenuAlias == _AuditReportCriteriaViewModels.MenuAlias)
                                      && (string.IsNullOrEmpty(_AuditReportCriteriaViewModels.ActionName) ? true : Table.ActionName.Contains(_AuditReportCriteriaViewModels.ActionName))
                                    select new AuditReportServicesViewModel
                                    {
                                        Id = Table.Id,
                                        MenuName = Table.MenuName,
                                        MenuAlias = Table.MenuAlias,
                                        Action = Table.ActionName,// == Enums.Operations.A.ToString() ? "Insert Record" : Table.Action == Enums.Operations.E.ToString() ? "Edit Record" : Table.Action == Enums.Operations.D.ToString() ? "Delete Record" : Table.Action == Enums.Operations.G.ToString() ? "Get All Record" : Table.Action == Enums.Operations.I.ToString() ? "Get By Id Record" : Table.Action,
                                        UserName = Table.UserNameInsert,
                                        Date = Table.InsertDate,
                                        CompanyName = _UserMenuPermissionAsync.CompanyName,
                                    }).Distinct().OrderByDescending(o => o.Date).ToListAsync();



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


                _AuditReportViewModel.auditReportServicesViewModels = _Table;

                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.data = _AuditReportViewModel;
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

        public async Task<ApiResponse> GetAuditReportDetailAsync(ClaimsPrincipal _User, AuditReportCriteriaViewModel AuditReportCriteriaViewModels)
        {
            ApiResponse ApiResponse = new ApiResponse();

            try
            {
                var _Key = _User.Claims.FirstOrDefault(x => x.Type == Enums.Misc.Key.ToString())?.Value.ToString();


                Guid _MenuId = new Guid(AuditReportCriteriaViewModels.MenuId);

                ApiResponse apiResponseUser = await _SecurityHelper.UserMenuPermissionAsync(_MenuId, _User);
                if (apiResponseUser.statusCode.ToString() != StatusCodes.Status200OK.ToString()) { return apiResponseUser; }
                var _UserMenuPermissionAsync = (GetUserPermissionViewModel)apiResponseUser.data;

                if (!_UserMenuPermissionAsync.View_Permission)
                {
                    ApiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
                    ApiResponse.message = "Invalid Permission";
                    return ApiResponse;
                }





                AuditReportDetailViewModel _AuditReportViewModel = new AuditReportDetailViewModel();
                _AuditReportViewModel.auditReportServicesDetailViewModels = new List<AuditReportServicesDetailViewModel>();

                _AuditReportViewModel.CompanyName = _UserMenuPermissionAsync.CompanyName;
                _AuditReportViewModel.HeadingName = "Audit History Report";
                _AuditReportViewModel.UserName = _UserMenuPermissionAsync.UserName;

                var _Table = await (from Table in _context.AuditTables
                                    where Table.Id == AuditReportCriteriaViewModels.Id
                                    select new AuditReportServicesDetailViewModel
                                    {
                                        Id = Table.Id,
                                        MenuName = Table.MenuName,
                                        MenuAlias = Table.MenuAlias,
                                        Action = Table.ActionName,
                                        UserName = Table.UserNameInsert,
                                        Date = Table.InsertDate,
                                        NewValue = Table.NewValue,
                                        OldValue = Table.OldValue,
                                        CompanyName = _UserMenuPermissionAsync.CompanyName,

                                    }).OrderByDescending(o => o.Date).ToListAsync();



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


                _AuditReportViewModel.auditReportServicesDetailViewModels = _Table;

                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.data = _AuditReportViewModel;
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