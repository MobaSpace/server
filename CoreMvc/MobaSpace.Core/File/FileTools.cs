using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MobaSpace.Core.File
{
    public class FileTools
    {
        public static void CopyTo(IFormFile file, string destFile)
        {
            Stream destFileStream;
            try
            {
                destFileStream = System.IO.File.Create(destFile);
            }
            catch (IOException ex)
            {
                throw new Exception($"Fail to create destination file", ex);
            }

            try
            {
                file.CopyTo(destFileStream);
            }
            catch (IOException ex)
            {
                throw new Exception($"Fail to copy file to destination file", ex);
            }

            destFileStream?.Dispose();
        }
    }
}
