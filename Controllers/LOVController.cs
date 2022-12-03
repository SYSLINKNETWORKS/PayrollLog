using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TWP_API_Log.Repository;

namespace TWP_API_Log.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]

    public class LOVController : ControllerBase
    {
        private readonly ILovServiceRepository ILogServiceRepository = null;
        public LOVController(ILovServiceRepository _ILogServiceRepository)
        {
            ILogServiceRepository = _ILogServiceRepository;
        }



        //GET Start
        ///<summary>
        ///Fetch Service 
        ///</summary>
        [HttpGet]
        [Route("GetService")]
        public async Task<IActionResult> GetService([FromHeader] string _Search)
        {
            var result = await ILogServiceRepository.GetServicesLovAsync(User, _Search);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        //Fetch Service End


        //GET Start
        ///<summary>
        ///Fetch Audit User 
        ///</summary>
        [HttpGet]
        [Route("GetAuditUser")]
        public async Task<IActionResult> GetAuditUser([FromHeader] string _Search)
        {
            var result = await ILogServiceRepository.GetAuditUserAsync(User, _Search);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        //GET Start
        ///<summary>
        ///Fetch Audit Action 
        ///</summary>
        [HttpGet]
        [Route("GetAuditAction")]
        public async Task<IActionResult> GetAuditAction([FromHeader] string _Search)
        {
            var result = await ILogServiceRepository.GetAuditActionAsync(User, _Search);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        //GET Start
        ///<summary>
        ///Fetch Audit Menu 
        ///</summary>
        [HttpGet]
        [Route("GetAuditMenu")]
        public async Task<IActionResult> GetAuditMenu([FromHeader] string _Search)
        {
            var result = await ILogServiceRepository.GetAuditMenuAsync(User, _Search);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


    }

}