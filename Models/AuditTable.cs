using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TWP_API_Log.Models
{
    [Table("AuditTable")]
    public class AuditTable
    {
        [Key]
        public Guid Id { get; set; }
        public Guid MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuAlias { get; set; }
        public string NewValue { get; set; }
        public string OldValue { get; set; }
        public string Action { get; set; }
        public string ActionName { get; set; }
        public string UserNameInsert { get; set; }
        public DateTime Date { get; set; } = DateTime.Now.Date;
        public DateTime InsertDate { get; set; } = DateTime.Now;

    }
}