using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
namespace TWP_API_Log.Controllers
{

    public class AuditReportCriteriaViewModel
    {

        public Guid Id { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string UserName { get; set; }
        public string ActionName { get; set; }
        public string MenuAlias { get; set; }
        public string NewValue { get; set; }
        public string OldValue { get; set; }
        public string MenuId { get; set; }
    }

    public class AuditReportViewModel
    {

        public string HeadingName { get; set; }
        public string CompanyName { get; set; }
        public string UserName { get; set; }
        public List<AuditReportServicesViewModel> auditReportServicesViewModels { get; set; }
    }


    public class AuditReportServicesViewModel
    {
        public Guid Id { get; set; }
        public string MenuName { get; set; }
        public string MenuAlias { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public string Action { get; set; }

        public string CompanyName { get; set; }
    }
     public class AuditReportDetailViewModel
    {

        public string HeadingName { get; set; }
        public string CompanyName { get; set; }
        public string UserName { get; set; }
        public List<AuditReportServicesDetailViewModel> auditReportServicesDetailViewModels { get; set; }
    }

    public class AuditReportServicesDetailViewModel
    {
        
        public Guid Id { get; set; }
        public string MenuName { get; set; }
        public string MenuAlias { get; set; }
        public string NewValue { get; set; }
        public string OldValue { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public string Action { get; set; }
 
        public string CompanyName { get; set; }
    }


}