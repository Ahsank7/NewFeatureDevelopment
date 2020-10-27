using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NewFeatureDevelopmentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewFeatureDevelopmentAPI.Infrastructure
{
    public class AttachmentContext:DbContext
    {

        public AttachmentContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Attachment> attachments { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        //{ 
        //    if(dbContextOptionsBuilder.IsConfigured)
        //    {
        //        return;
        //    }
        //    string connectionstring = _configuration["ConnectionStrings:NotificationDBConnectionString"];
        //    dbContextOptionsBuilder.UseSqlServer(connectionstring);

        //    base.OnConfiguring(dbContextOptionsBuilder);


        //}
    }
}
