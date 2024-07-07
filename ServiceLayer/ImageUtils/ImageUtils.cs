using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ImageUtils
{
    internal static class ImageUtils
    {
        public static byte[]? GetFileAsMemoryStream(IFormFile file)
        {
            if (file != null)
            {
                if (file.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);

                        if (memoryStream.Length < 50 * 1024 * 1024)
                        {
                            return memoryStream.ToArray();
                        }
                    }
                }
            }

            return null;
        }
    }
}
