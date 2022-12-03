using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TWP_API_Log.Models
{
    [Table("TableLog")]
    public class TableLog
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        [Required]
        public string Description { get; set; }
        [Required]
        public string StackTrace { get; set; }
        [Required]
        public string Type { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
        [Required]
        public string CompanyId { get; set; }

        [Required]
        public string UserId { get; set; }
        [Required]
        public Guid Key { get; set; }

    }
}