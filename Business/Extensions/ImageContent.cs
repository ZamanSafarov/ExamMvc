using Business.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Extensions
{
    public static partial class Exntension 
    {

        public static string ImageAdd(this IWebHostEnvironment env,string folder,IFormFile file,string content)
        {
            if (!file.ContentType.Contains("image/"))
            {
                throw new FileExtensionsException("File Must Be Image");
            }
            string extension = Path.GetExtension(file.FileName);

            string fileName = content + "-" + Guid.NewGuid().ToString().ToLower() + extension;

            var path = Path.Combine(env.WebRootPath,folder, fileName);

            using (FileStream fs = new FileStream(path,FileMode.Create))
            {
                file.CopyTo(fs);
            }

            return fileName;
        }
        public static void ArchiveImage(this IWebHostEnvironment env, string folder,string fileName)
        {
            var actualPath = Path.Combine(env.WebRootPath, folder, fileName);

            if (File.Exists(actualPath))
            {
                var archivePath = Path.Combine(env.WebRootPath, folder,$"archive-{DateTime.Now:yyyyMMddHHmmss}-{fileName}");

                using (FileStream stream = new FileStream(actualPath,FileMode.Open))
                {
                    using (FileStream archiveStream = new FileStream(archivePath,FileMode.Create))
                    {
                        stream.CopyTo(archiveStream);
                    }
                }
                File.Delete(actualPath);
            }
            else
            {
                throw new FileDoesNotExsistException($"File Does Not Exsist in {actualPath}");
            }
        }
    }
}
