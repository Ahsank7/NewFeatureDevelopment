using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewFeatureDevelopmentAPI.Models
{
    public class AttachmentToCreate
    {
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public string FileSize { get; set; }
        public string FileType { get; set; }
    }
}
