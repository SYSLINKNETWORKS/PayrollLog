using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TWP_API_Log.ViewModels
{
    public class AuditBaseModel
    {
        public Guid MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuAlias { get; set; }
        public string NewValue { get; set; }
        public string OldValue { get; set; }
        public string Action { get; set; }
        public string UserName { get; set; }

    }

    public class AuditViewAddModel : AuditBaseModel
    {
        public Guid Key { get; set; }

    }
}