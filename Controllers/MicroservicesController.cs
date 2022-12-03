using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TWP_API_Log.Helpers;
using TWP_API_Log.Services;
using TWP_API_Log.ViewModels;

namespace TWP_API_Log.Controllers
{
    ///<summary>
    ///Micro Service
    ///</summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class MicroservicesController : ControllerBase
    {
        private readonly IMicroservices _IMicroservices = null;
        public MicroservicesController(IMicroservices IMicroservices)
        {
            _IMicroservices = IMicroservices;
        }

        ///<summary>
        ///Get Deparment All 
        ///</summary>
        [HttpGet]
        [Route("MSServiceById")]
        public async Task<IActionResult> MSServiceById([FromHeader] Guid Key)
        {
            try
            {
                ApiResponse result = await _IMicroservices.MSServiceByIdAsync(Key);
                if (result.statusCode == StatusCodes.Status200OK.ToString())
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception e)
            {
                string innerexp = "";
                if (e.InnerException != null)
                {
                    innerexp = " Inner Error : " + e.InnerException.ToString();
                }
                return BadRequest(e.Message.ToString() + innerexp);
            }
        }
        //Get End

        ///<summary>
        ///Audit Log
        ///</summary>
        [HttpPost]
        [Route("MSServiceAuditLog")]
        public async Task<IActionResult> MSServiceAuditLog([FromBody] AuditViewAddModel _model)
        {
            try
            {
                ApiResponse result = await _IMicroservices.MSServiceAuditLogAsync(_model);
                if (result.statusCode == StatusCodes.Status200OK.ToString())
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception e)
            {
                string innerexp = "";
                if (e.InnerException != null)
                {
                    innerexp = " Inner Error : " + e.InnerException.ToString();
                }
                return BadRequest(e.Message.ToString() + innerexp);
            }
        }
        //Post End


    }

}