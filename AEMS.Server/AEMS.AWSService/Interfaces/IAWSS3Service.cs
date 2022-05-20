using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AEMS.AWSService
{
    public interface IAWSS3Service
    {
        /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        Task<string> UploadFile(IFormFile file, string path);

        /// <summary>
        /// Gets the presigned URL.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        string GetPresignedUrl(string path);

        /// <summary>
        /// Gets the muti presigned URL.
        /// </summary>
        /// <param name="paths">The paths.</param>
        /// <returns></returns>
        IEnumerable<string> GetMutiPresignedUrl(List<string> paths);
    }
}
