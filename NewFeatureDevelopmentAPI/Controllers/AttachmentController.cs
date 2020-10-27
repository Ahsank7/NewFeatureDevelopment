using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewFeatureDevelopmentAPI.Infrastructure;
using NewFeatureDevelopmentAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewFeatureDevelopmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private AttachmentContext _attachmentContext;
        public AttachmentController(AttachmentContext attachmentContext)
        {
            _attachmentContext = attachmentContext;
        }

        // POST api/<AttachmentController>
        //[HttpPost]
        //public IActionResult Post([FromBody] AttachmentToCreate attachmentToCreate)
        //{
        //    if (attachmentToCreate == null)
        //    {
        //        return BadRequest("Attachment object is null");
        //    }

        //    Attachment attachment = new Attachment();

        //    attachment.Description= attachmentToCreate.Description;
        //    attachment.FileSize = attachmentToCreate.FileSize;
        //    attachment.FileType = attachmentToCreate.FileType;
        //    attachment.ImagePath = attachmentToCreate.ImagePath;

        //    _attachmentContext.Add(attachment);
        //    _attachmentContext.SaveChanges();

        //    return StatusCode((int)HttpStatusCode.Created);
        //}


        // GET: api/<AttachmentController>
        [HttpPost]
        [Route("/api/[controller]/Search")]
        public IActionResult GetAttachments(SearchParameters searchParameters)
        {

            try
            {
                List<Attachment> attachments = new List<Attachment>();

                attachments = _attachmentContext.attachments.Where(x => x.Description == searchParameters.Description ||
                                                                   x.FileSize == searchParameters.FileSize ||
                                                                   x.FileType == searchParameters.FileType
                                                                  ).ToList();



                if (attachments!=null && attachments.Count>0)
                {
                    return Ok(attachments);
                }
                else
                {
                    return  StatusCode((int)HttpStatusCode.NoContent);
                }

            
            }
            catch {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            
        }

        

   
      
    }
}
