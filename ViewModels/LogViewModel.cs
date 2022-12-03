using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TWP_API_Log.ViewModels
{
    public class LogBaseModel
    {

    }

    public class LogFoundationModel : LogBaseModel
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public string StackTrace { get; set; }
        [Required]
        public string Type { get; set; }

    }
    public class LogViewModel : LogFoundationModel
    {


        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string BranchName { get; set; }

        [Required]
        public string UserName { get; set; }
    }
    public class LogViewByIdModel : LogFoundationModel
    {
        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string BranchName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime Date { get; set; }



    }
    public class LogViewAddModel : LogFoundationModel
    {
        [Required]
        public Guid Key { get; set; }
        [Required]
        public string CompanyId { get; set; }

        [Required]
        public string UserId { get; set; }

    }
}