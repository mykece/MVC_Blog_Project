using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS14MVC.UI.Extentions
{
    public static class FormFileExtention
    {
        /// <summary>
        /// bir uzatma (extension) metodudur.
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public static async Task<byte[]> StringToByteArrayAsync(this IFormFile formFile)
        {
            using MemoryStream memory = new MemoryStream();
            await formFile.CopyToAsync(memory);
            return memory.ToArray();
        }
        public static async Task<string> ByteArrayToStringAsync(this byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                return string.Empty;

            return await Task.Run(() => Convert.ToBase64String(byteArray));
        }
        public static async Task<IFormFile> ByteArrayToFormFileAsync(this byte[] byteArray, string fileName, string contentType)
        {
            if (byteArray == null || byteArray.Length == 0)
                return null;

            var stream = new MemoryStream(byteArray);
            IFormFile formFile = new FormFile(stream, 0, byteArray.Length, "name", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType
            };

            return await Task.FromResult(formFile);
        }
        public static async Task<TimeSpan> CalculeTimeForString(this string value)
        {
            var lenght = value.Length;
            int x = lenght / 5;
            return TimeSpan.FromMinutes(x+1);

        }
    }
}
