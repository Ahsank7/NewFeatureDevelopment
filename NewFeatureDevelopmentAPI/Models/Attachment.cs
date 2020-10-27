using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewFeatureDevelopmentAPI.Models
{
    [Table("tblAttachment")]
    public class Attachment
    {
        [Key]
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public string FileSize { get; set; }
        public string FileType { get; set; }

    }
}
