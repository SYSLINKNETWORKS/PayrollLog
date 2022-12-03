using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TWP_API_Log.ViewModels
{
    public class ServiceFoundationModel
    {

    }
    public class ServiceBaseModel : ServiceFoundationModel
    {
        [Required]
        public string ServiceName { get; set; }

        [Required]
        public bool Active { get; set; }
    }


    public class ServiceViewAddModel : ServiceBaseModel
    {

    }

    public class ServiceViewUpdateModel : ServiceBaseModel
    {
        [Required]
        public Guid Id { get; set; }
    }

    public class ServiceViewModel : ServiceBaseModel
    {

        [Required]
        public Guid Id { get; set; }

    }
    public class ServiceViewByIdModel : ServiceBaseModel
    {
        [Required]
        public Guid Id { get; set; }

    }
}