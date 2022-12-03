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
    public class ServiceController : ControllerBase
    {
        string _TokenString = "";
        private readonly IProcessor<ServiceBaseModel> _IProcessor = null;
        public ServiceController(IProcessor<ServiceBaseModel> IProcessor)
        {
            _IProcessor = IProcessor;
        }

       ///<summary>
        ///Get All Services
        ///</summary>
        [HttpGet]
        public async Task<IActionResult> GetService()
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
        ///Get Service by id
        ///</summary>
        [HttpGet]
        [Route("GetServiceById")]
        public async Task<IActionResult> GetServiceById([FromHeader] Guid _Id)
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
        ///Service Add
        ///</summary>
        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> AddService([FromBody] ServiceViewAddModel model)
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


         ///<summary>
        ///Service Update
        ///</summary>
        [HttpPut]
        [AllowAnonymous]

        public async Task<IActionResult> UpdateService([FromBody] ServiceViewUpdateModel model)
        {
            try
            {
                return Ok(await _IProcessor.ProcessPut(model));
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



    }

}