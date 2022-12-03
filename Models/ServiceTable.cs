using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TWP_API_Log.Models
{
    [Table("ServiceTable")]
    public class ServiceTable
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string ServiceName { get; set; }

        [Required]

        public bool Active {get;set;}
        

    }
}