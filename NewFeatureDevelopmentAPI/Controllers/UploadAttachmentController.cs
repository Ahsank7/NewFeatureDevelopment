using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewFeatureDevelopmentAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewFeatureDevelopmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadAttachmentController : ControllerBase
    {
        static readonly string[] suffixes = { " Bytes", " KB", " MB", " GB", " TB", " PB" };
        private Services.IAttachment _attachment;
        public UploadAttachmentController(Services.IAttachment attachment)
        {
            _attachment = attachment;
        }


        // POST api/<AttachmentController>
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Post()
        {
            try
            {
                var file = Request.Form.Files[0];

               
                System.IO.FileInfo fi = new System.IO.FileInfo(file.FileName);

              

                string imagePath= await _attachment.UploadAttachment(file);

                if(!string.IsNullOrEmpty(imagePath))
                {


                

                    Attachment attachmentToCreate = new Attachment();

                    attachmentToCreate.ImagePath = imagePath;
                    attachmentToCreate.FileSize = FormatSize(file.Length);
                    attachmentToCreate.FileType = fi.Extension.ToUpper().Replace(".", "");

                    _attachment.SaveAttachment(attachmentToCreate);

                    return StatusCode((int)HttpStatusCode.Created);
                 
                    
                }
                else
                {

                    return StatusCode((int)HttpStatusCode.InternalServerError, "Unale to save file on AWS bucket");
                }


                
               
                
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        public static string FormatSize(Int64 bytes)
        {
            int counter = 0;
            decimal number = (decimal)bytes;
            while (Math.Round(number / 1024) >= 1)
            {
                number = number / 1024;
                counter++;
            }
            return string.Format("{0:n2}{1}", number, suffixes[counter]);
        }
    }
}
