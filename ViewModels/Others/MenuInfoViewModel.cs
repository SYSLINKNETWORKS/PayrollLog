using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TWP_API_Log.ViewModels
{
    public class MenuInfoViewModel
    {
        public Guid MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuAlias { get; set; }
        public Guid ModuleId { get; set; }
        public string ModuleName { get; set; }
        public Guid SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public Guid SubCategoryMasterId { get; set; }
        public string SubCategoryMasterName { get; set; }

    }

}