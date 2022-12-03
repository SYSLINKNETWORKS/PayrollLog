using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TWP_API_Log.Processor;
using TWP_API_Log.ViewModels;

namespace TWP_API_Log.Controllers
{
    ///<summary>
    ///Log
    ///</summary>

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class LogController : ControllerBase
    {
        string _TokenString = "";
        private readonly IProcessor<LogBaseModel> _IProcessor = null;
        public LogController(IProcessor<LogBaseModel> IProcessor)
        {
            _IProcessor = IProcessor;
        }

        ///<summary>
        ///Get All Log
        ///</summary>
        [HttpGet]
        public async Task<IActionResult> GetLog()
        {
            try
            {
                _TokenString = HttpContext.Request.Headers["Authorization"].ToString();

                var result = await _IProcessor.ProcessGet(_TokenString);
                return Ok(result);
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
        ///Get Log by id
        ///</summary>
        [HttpGet]
        [Route("GetLogById")]
        public async Task<IActionResult> GetLogById([FromHeader] Guid _Id)
        {
            try
            {
                _TokenString = HttpContext.Request.Headers["Authorization"].ToString();
                var result = await _IProcessor.ProcessGetById(_Id, _TokenString);
                return Ok(result);
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
        //Get by Id End

        ///<summary>
        ///Log Add
        ///</summary>
        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> AddLog([FromBody] LogViewAddModel model)
        {
            try
            {
                return Ok(await _IProcessor.ProcessPost(model));
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
        //Create End



    }

}