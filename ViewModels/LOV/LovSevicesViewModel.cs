using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TWP_API_Log.ViewModels
{


    public class LovServicesViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class LovServicesStringViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }


}