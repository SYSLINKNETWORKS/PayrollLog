
using JsonDiffer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TWP_API_Log.App_Data;
using TWP_API_Log.Generic;
using TWP_API_Log.Helpers;
using TWP_API_Log.Models;
using TWP_API_Log.ViewModels;

namespace TWP_API_Log.Services
{
    public interface IMicroservices
    {
        Task<ApiResponse> MSServiceByIdAsync(Guid _Key);
        Task<ApiResponse> MSServiceAuditLogAsync(AuditViewAddModel _model);


    }

    public class Microservices : IMicroservices
    {
        private readonly DataContext _context;
        SecurityHelper _SecurityHelper = new SecurityHelper();

        public Microservices(DataContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse> MSServiceByIdAsync(Guid _Key)
        {
            ApiResponse apiResponse = new ApiResponse();
            var _ServiceTable = await _context.ServiceTables.Where(x => x.Id == _Key).FirstOrDefaultAsync();//.FirstOrDefaultAsync();
            if (_ServiceTable == null)
            {
                apiResponse.statusCode = StatusCodes.Status401Unauthorized.ToString();
                apiResponse.message = "Invalid Key";
                return apiResponse;
            }
            LovServicesViewModel _LovServicesViewModel = new LovServicesViewModel();
            _LovServicesViewModel.Id = _ServiceTable.Id;
            _LovServicesViewModel.Name = _ServiceTable.ServiceName;


            apiResponse.statusCode = StatusCodes.Status200OK.ToString();
            apiResponse.data = _LovServicesViewModel;

            return apiResponse;
        }
        public async Task<ApiResponse> MSServiceAuditLogAsync(AuditViewAddModel _model)
        {
            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", false)
                                    .Build();
            // var _PassPhase = configuration["ConnectionStrings:PassPhase"];
            ApiResponse apiResponse = new ApiResponse();
            var _ServiceTable = await _context.ServiceTables.Where(x => x.Id == _model.Key).FirstOrDefaultAsync();
            if (_ServiceTable == null)
            {
                apiResponse.statusCode = StatusCodes.Status401Unauthorized.ToString();
                apiResponse.message = "Invalid Key";
                return apiResponse;
            }
            // if (_model.NewValue != "" && _model.OldValue != "")
            // {
            //     var JTokenOld = JToken.Parse(_model.OldValue);
            //     var JTokenNew = JToken.Parse(_model.NewValue);
            //     _model.OldValue = JsonDifferentiator.Differentiate(JTokenOld, JTokenNew).ToString();
            //     _model.NewValue = JsonDifferentiator.Differentiate(JTokenNew, JTokenOld).ToString();
            // }
            var audit = new AuditTable
            {
                MenuId = _model.MenuId,
                MenuName = _model.MenuName,
                MenuAlias = _model.MenuAlias,
                NewValue = _model.NewValue,
                OldValue = _model.OldValue,
                Action = _model.Action,
                ActionName = _model.Action == Enums.Operations.A.ToString() ? "Insert Record" : _model.Action == Enums.Operations.E.ToString() ? "Edit Record" : _model.Action == Enums.Operations.D.ToString() ? "Delete Record" : _model.Action == Enums.Operations.G.ToString() ? "Get All Record" : _model.Action == Enums.Operations.I.ToString() ? "Get By Id Record" : _model.Action,
                UserNameInsert = _model.UserName,
            };

            await _context.AuditTables.AddAsync(audit);
            await _context.SaveChangesAsync();

            apiResponse.statusCode = StatusCodes.Status200OK.ToString();
            apiResponse.message = "Added";

            return apiResponse;
        }

    }

}
