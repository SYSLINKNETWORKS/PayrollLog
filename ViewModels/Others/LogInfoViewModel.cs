using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TWP_API_Log.ViewModels
{
    public class LoginInfoViewModel
    {
        [Required]
        public string CompanyId { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string BranchId { get; set; }
        [Required]
        public string BranchName { get; set; }
 
        [Required]
        public string UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string YearId { get; set; }
        [Required]
        public String YearName { get; set; }

    }

}