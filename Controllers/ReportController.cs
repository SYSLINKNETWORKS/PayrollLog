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

    public class ReportController : ControllerBase
    {
        private readonly IReportServiceRepository IReportServiceRepository = null;
        public ReportController(IReportServiceRepository _IReportServiceRepository)
        {
            IReportServiceRepository = _IReportServiceRepository;
        }

        //GET Start
        ///<summary>
        ///Fetch Report 
        ///</summary>
        [HttpPost]
        [Route("GetAuditReport")]
        public async Task<IActionResult> GetAuditReport([FromBody] AuditReportCriteriaViewModel AuditReportCriteriaViewModels)
        {
            var result = await IReportServiceRepository.GetAuditReportAsync(User, AuditReportCriteriaViewModels);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        //GET Start
        ///<summary>
        ///Fetch Report 
        ///</summary>
        [HttpPost]
        [Route("GetAuditHistoryReport")]
        public async Task<IActionResult> GetAuditHistoryReport([FromBody] AuditReportCriteriaViewModel AuditReportCriteriaViewModels)
        {
            var result = await IReportServiceRepository.GetAuditReportDetailAsync(User, AuditReportCriteriaViewModels);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }




    }

}