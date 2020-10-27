using Microsoft.AspNetCore.Http;
using NewFeatureDevelopmentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewFeatureDevelopmentAPI.Services
{
    public interface IAttachment
    {
        Task<string> UploadAttachment(IFormFile Files);
        int SaveAttachment(Attachment attachmentToCreate);
    }
}
