using AEMS.Utilities;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AEMS.AWSService
{
    public class AWSS3Service : IAWSS3Service
    {
        #region Services

        /// <summary>
        /// The amazon s3
        /// </summary>
        private readonly IAmazonS3 AmazonS3;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AWSS3Service"/> class.
        /// </summary>
        /// <param name="amazonS3">The amazon s3.</param>
        public AWSS3Service()
        {
            AmazonS3 = new AmazonS3Client(AppSettingValues.AWSAccessKey, AppSettingValues.AWSSecretKey, RegionEndpoint.APSoutheast1);
        }

        #endregion

        #region Get Presigned Url

        /// <summary>
        /// Gets the presigned URL.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public string GetPresignedUrl(string path)
        {
            try
            {
                var request = new GetPreSignedUrlRequest
                {
                    BucketName = AppSettingValues.AWSS3BucketName,
                    Key = path,
                    Expires = DateTimeCountry.DateTimeNow.AddMinutes(AppSettingValues.AWSDurationPreSignedUrl)
                };

                return AmazonS3.GetPreSignedURL(request);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        #endregion

        #region Get Mutiple Presigned Url

        /// <summary>
        /// Gets the muti presigned URL.
        /// </summary>
        /// <param name="paths">The paths.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<string> GetMutiPresignedUrl(List<string> paths)
        {
            return paths.Select(x => GetPresignedUrl(x));
        }

        #endregion

        #region Upload File

        /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public async Task<string> UploadFile(IFormFile file, string path)
        {
            try
            {
                var putRequest = new PutObjectRequest()
                {
                    BucketName = AppSettingValues.AWSS3BucketName,
                    Key = $"{path}/{DateTimeCountry.DateTimeNow:MM-dd-yyyy-hh:mm:tt}-{file.FileName}",
                    InputStream = file.OpenReadStream(),
                    ContentType = file.ContentType,
                };
                await AmazonS3.PutObjectAsync(putRequest).ConfigureAwait(false);
                return putRequest.Key;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        #endregion

        public IEnumerable<string> GetMutiApiGatewayS3InvokeURL(List<string> paths)
        {
            return paths.Select(x => $"{AppSettingValues.AWSApiGatewayS3InvokeURL}/{x}");
        }
    }
}
