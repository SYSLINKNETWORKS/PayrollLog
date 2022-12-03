using System;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TWP_API_Log.Models;


namespace TWP_API_Log.App_Data
{
    public partial class DataContext : DbContext
    {
        //DbContext {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public virtual DbSet<TableLog> TableLogs { get; set; }
        public virtual DbSet<ServiceTable> ServiceTables {get;set;}
        public virtual DbSet<AuditTable> AuditTables {get;set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

        }
    }
}