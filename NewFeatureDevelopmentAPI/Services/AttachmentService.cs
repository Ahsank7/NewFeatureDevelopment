
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NewFeatureDevelopmentAPI.Models;
using NewFeatureDevelopmentAPI.Infrastructure;

namespace NewFeatureDevelopmentAPI.Services
{
    public class AttachmentService : IAttachment
    {
        private readonly IAmazonS3 _client;
        private AttachmentContext _attachmentContext;
        private string bucketName="newfeatureattachments";

        public AttachmentService(IAmazonS3 client, AttachmentContext attachmentContext)
        {
            _client = client;
            _attachmentContext = attachmentContext;
        }

        public int SaveAttachment(Attachment attachmentToCreate)
        {
            

            _attachmentContext.Add(attachmentToCreate);
           int response= _attachmentContext.SaveChanges();

            return response;
        }

        public async Task<string> UploadAttachment(IFormFile File)
        {
            try
            {
                if (await AmazonS3Util.DoesS3BucketExistV2Async(_client, bucketName))
                {
                    var transfacility = new TransferUtility(_client);

                    var folderName = Path.Combine("StaticFiles", "Images");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    var fileName = File.FileName;

                    if (File.Length > 0)
                    {

                        var fullPath = Path.Combine(pathToSave, fileName);
                        var dbPath = Path.Combine(folderName, fileName);



                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await File.CopyToAsync(stream);

                            await transfacility.UploadAsync(stream,
                                                   bucketName, fileName);
                        }



                        //  using (var fileToUpload =
                        //new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                        //  {
                        //      await transfacility.UploadAsync(fileToUpload,
                        //                                 "newfeatureimages", fileName);
                        //  }

                        //GetObjectRequest request = new GetObjectRequest();
                        //request.BucketName = "newfeatureattachments";
                        //request.Key = fileName;
                        //var getresponse = await _client.GetObjectAsync(request);

                        string urlString = "";

                        GetPreSignedUrlRequest request1 = new GetPreSignedUrlRequest
                        {
                            BucketName = bucketName,
                            Key = fileName,
                            Expires = DateTime.UtcNow.AddHours(168)
                        };
                        urlString = _client.GetPreSignedURL(request1);



                        return urlString;
                        // return Ok(new { dbPath });
                    }
                    else
                    {
                        return "";
                    }



                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }

            
        }
    }
}
