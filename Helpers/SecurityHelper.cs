using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TWP_API_Log.Generic;
using TWP_API_Log.ViewModels;

namespace TWP_API_Log.Helpers
{
    public class SecurityHelper
    {
        //Encrypt Password Start
        string _connectionString = "";
        public SecurityHelper()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();

            _connectionString = configuration.GetConnectionString("Connection").ToString();

        }

        //Menu Permission
        public async Task<ApiResponse> UserMenuPermissionAsync(Guid _MenuId, ClaimsPrincipal _User)
        {
            ApiResponse apiResponse = new ApiResponse();
            string _Key = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.Key.ToString())?.Value.ToString();

            var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false)
        .Build();

            HttpClient client = new HttpClient();
            var _BaseUri = configuration["ConnectionStrings:GWAPI"] + "/Auth/v1/";
            client.BaseAddress = new Uri(_BaseUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("MenuId", _MenuId.ToString());
            client.DefaultRequestHeaders.Add("Key", _Key);// _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.Key.ToString())?.Value.ToString());
            var url = "Microservices/MenuPermission";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var _JsonData = response.Content.ReadAsStringAsync().Result;
                if (_JsonData != null)
                {
                    GetUserPermissionViewModel _GetUserPermissionViewModel = new GetUserPermissionViewModel();

                    ApiResponse _ApiResponse = JsonConvert.DeserializeObject<ApiResponse>(_JsonData.ToString());
                    var entities = JObject.Parse(_JsonData).SelectToken("data");
                    _GetUserPermissionViewModel = JsonConvert.DeserializeObject<GetUserPermissionViewModel>(entities.ToString());
                    _ApiResponse.data = _GetUserPermissionViewModel;
                    apiResponse = _ApiResponse;
                }
                return apiResponse;
            }
            apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
            apiResponse.message = "Invalid Permission";
            return apiResponse;
        }
        //Menu Permission



        public async Task<bool> KeyValidation(string _TokenString)
        {
            var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", false)
           .Build();



            HttpClient client = new HttpClient();
            var _BaseUri = configuration["ConnectionStrings:GWAPI"] + "/Auth/v1/";
            client.BaseAddress = new Uri(_BaseUri);
            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Add("Authorization", _TokenString);
            var url = "Microservices/UserKey";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        public async Task<ApiResponse> UserInfo(string _TokenString)
        {
            ApiResponse apiResponse = new ApiResponse();

            var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", false)
           .Build();



            HttpClient client = new HttpClient();
            var _BaseUri = configuration["ConnectionStrings:GWAPI"] + "/Auth/v1/";
            client.BaseAddress = new Uri(_BaseUri);
            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Add("Authorization", _TokenString);
            var url = "Auth/UserLoginInfo";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var _JsonData = response.Content.ReadAsStringAsync().Result;
                if (_JsonData != null)
                {
                    LoginInfoViewModel _LoginInfoViewModel = new LoginInfoViewModel();

                    ApiResponse _ApiResponse = JsonConvert.DeserializeObject<ApiResponse>(_JsonData.ToString());
                    var entities = JObject.Parse(_JsonData).SelectToken("data");
                    _LoginInfoViewModel = JsonConvert.DeserializeObject<LoginInfoViewModel>(entities.ToString());

                    apiResponse.statusCode = StatusCodes.Status200OK.ToString();
                    apiResponse.data = _LoginInfoViewModel;
                    return apiResponse;
                }
            }
            apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
            return apiResponse;
        }
        public async Task<ApiResponse> MenuInfo(string _Key)
        {
            ApiResponse apiResponse = new ApiResponse();

            var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", false)
                        .Build();

            HttpClient client = new HttpClient();
            var _BaseUri = configuration["ConnectionStrings:GWAPI"] + "/Auth/v1/";
            client.BaseAddress = new Uri(_BaseUri);
            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Add("Key", _Key);
            var url = "Microservices/MenuInfo";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var _JsonData = response.Content.ReadAsStringAsync().Result;
                if (_JsonData != null)
                {

                    ApiResponse _ApiResponse = JsonConvert.DeserializeObject<ApiResponse>(_JsonData.ToString());
                    _ApiResponse.data = JsonConvert.DeserializeObject<List<MenuInfoViewModel>>(_ApiResponse.data.ToString());
                    apiResponse = _ApiResponse;
                }
                return apiResponse;
            }
            apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
            apiResponse.message = "Branch not found";
            return apiResponse;
        }
    }

}