using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TWP_API_Log.Processor;
using TWP_API_Log.ViewModels;

namespace TWP_API_Log.Controllers
{
    ///<summary>
    ///AuditTable
    ///</summary>

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class AuditController : ControllerBase
    {
        private readonly IProcessor<AuditBaseModel> _IProcessor = null;
        public AuditController(IProcessor<AuditBaseModel> IProcessor)
        {
            _IProcessor = IProcessor;
        }


      

        ///<summary>
        ///Audit Add
        ///</summary>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddAudit([FromBody] AuditViewAddModel model)
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