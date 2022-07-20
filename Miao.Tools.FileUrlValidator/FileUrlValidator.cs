using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Miao.Tools.FileUrlValidator
{
    public abstract class FileUrlValidator
    {
        /// <summary>
        /// 获取允许的文件后缀名
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllowFileExtensions()
        {
            var result = new List<string>();
            result.AddRange(this.AllowTextFileExtensions);
            result.AddRange(this.AllowOfficeFileExtensions);
            result.AddRange(this.AllowArchiveFileExtensions);
            result.AddRange(this.AllowImageFileExtensions);
            result.AddRange(this.AllowVideoFileExtensions);
            result.AddRange(this.AllowAudioFileExtensions);
            result.AddRange(this.AllowOtherFileExtensions);
            return result.Distinct().ToList();
        }

        /// <summary>
        /// 设置最大文件大小
        /// </summary>
        /// <param name="maxFileSize"></param>
        /// <returns></returns>
        public FileUrlValidator SetMaxFileSize(long maxFileSize)
        {
            this.MaxFileSize = maxFileSize;
            return this;
        }

        /// <summary>
        /// 设置允许的文本文件后缀名
        /// </summary>
        /// <param name="fileExtensions"></param>
        /// <returns></returns>
        public FileUrlValidator SetAllowTextFileExtensions(params string[] fileExtensions)
        {
            this.AllowTextFileExtensions = fileExtensions.ToList();
            return this;
        }

        /// <summary>
        /// 设置允许的办公文件后缀名
        /// </summary>
        /// <param name="fileExtensions"></param>
        /// <returns></returns>
        public FileUrlValidator SetAllowOfficeFileExtensions(params string[] fileExtensions)
        {
            this.AllowOfficeFileExtensions = fileExtensions.ToList();
            return this;
        }

        /// <summary>
        /// 设置允许的压缩文件后缀名
        /// </summary>
        /// <param name="fileExtensions"></param>
        /// <returns></returns>
        public FileUrlValidator SetAllowArchiveFileExtensions(params string[] fileExtensions)
        {
            this.AllowArchiveFileExtensions = fileExtensions.ToList();
            return this;
        }

        /// <summary>
        /// 设置允许的图片文件后缀名
        /// </summary>
        /// <param name="fileExtensions"></param>
        /// <returns></returns>
        public FileUrlValidator SetAllowImageFileExtensions(params string[] fileExtensions)
        {
            this.AllowImageFileExtensions = fileExtensions.ToList();
            return this;
        }

        /// <summary>
        /// 设置允许的视频文件后缀名
        /// </summary>
        /// <param name="fileExtensions"></param>
        /// <returns></returns>
        public FileUrlValidator SetAllowVideoFileExtensions(params string[] fileExtensions)
        {
            this.AllowVideoFileExtensions = fileExtensions.ToList();
            return this;
        }

        /// <summary>
        /// 设置允许的音频文件后缀名
        /// </summary>
        /// <param name="fileExtensions"></param>
        /// <returns></returns>
        public FileUrlValidator SetAllowAudioFileExtensions(params string[] fileExtensions)
        {
            this.AllowAudioFileExtensions = fileExtensions.ToList();
            return this;
        }

        /// <summary>
        /// 设置允许的其他文件后缀名
        /// </summary>
        /// <param name="fileExtensions"></param>
        /// <returns></returns>
        public FileUrlValidator SetAllowOtherFileExtensions(params string[] fileExtensions)
        {
            this.AllowOtherFileExtensions = fileExtensions.ToList();
            return this;
        }

        /// <summary>
        /// 是否验证文件大小
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public FileUrlValidator ValidateFileSize(bool value)
        {
            this.IsValidateFileSize = value;
            return this;
        }

        /// <summary>
        /// 是否验证文件扩展名
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public FileUrlValidator ValidateFileExtension(bool value)
        {
            this.IsValidateFileExtension = value;
            return this;
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public virtual async Task<ValideResult> ValidateAsync(string fileUrl)
        {
            if (string.IsNullOrEmpty(fileUrl))
            {
                throw new ArgumentException("the file url is null or empty", nameof(fileUrl));
            }
            List<string> errorMessages = new List<string>();
            //是否验证文件扩展名
            if (this.IsValidateFileExtension)
            {
                var errorMsgs = await DoValidateFileExtension(fileUrl);
                errorMessages.AddRange(errorMsgs);
            }
            //是否验证文件大小
            if (this.IsValidateFileSize)
            {
                var errorMsgs = await DoValidateFileSize(fileUrl);
                errorMessages.AddRange(errorMsgs);
            }
            return new ValideResult(!errorMessages.Any(), errorMessages);
        }

        #region 属性

        /// <summary>
        /// 是否验证文件大小,默认为true
        /// </summary>
        public bool IsValidateFileSize { get; protected set; } = true;

        /// <summary>
        /// 是否验证文件扩展名,默认为true
        /// </summary>
        public bool IsValidateFileExtension { get; protected set; } = true;

        /// <summary>
        /// 最大文件大小
        /// </summary>
        public virtual long MaxFileSize { get; protected set; }

        /// <summary>
        /// 允许的文本文件后缀名
        /// </summary>
        public virtual List<string> AllowTextFileExtensions { get; protected set; } = new List<string>();

        /// <summary>
        /// 允许的办公文件后缀名
        /// </summary>
        public virtual List<string> AllowOfficeFileExtensions { get; protected set; } = new List<string>();

        /// <summary>
        /// 允许的压缩文件后缀名
        /// </summary>
        public virtual List<string> AllowArchiveFileExtensions { get; protected set; } = new List<string>();

        /// <summary>
        /// 允许的图片文件后缀名
        /// </summary>
        public virtual List<string> AllowImageFileExtensions { get; protected set; } = new List<string>();

        /// <summary>
        /// 允许的视频文件后缀名
        /// </summary>
        public virtual List<string> AllowVideoFileExtensions { get; protected set; } = new List<string>();

        /// <summary>
        /// 允许的音频文件后缀名
        /// </summary>
        public virtual List<string> AllowAudioFileExtensions { get; protected set; } = new List<string>();

        /// <summary>
        /// 允许的其他文件后缀名
        /// </summary>
        public virtual List<string> AllowOtherFileExtensions { get; protected set; } = new List<string>();

        #endregion

        #region private methods

        /// <summary>
        /// 验证文件大小
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        private async Task<List<string>> DoValidateFileSize(string fileUrl)
        {
            var errorMessages = new List<string>();
            try
            {
                var httpClient = new HttpClient()
                {
                    Timeout = TimeSpan.FromSeconds(3)
                };
                var httpResponse = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, fileUrl));
                //判断文件是否存在
                if (httpResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    errorMessages.Add("the file url is not found (404)");
                }
                //判断文件大小
                else if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    var contentLength = httpResponse.Content.Headers.ContentLength ?? 0;
                    if (contentLength <= 0)
                    {
                        errorMessages.Add("unable to get file size");
                    }
                    else if (contentLength > MaxFileSize)
                    {
                        errorMessages.Add($"the file({contentLength} bytes) is too large");
                    }
                }
                else
                {
                    errorMessages.Add($"the file status code({httpResponse.StatusCode}) is abnormal");
                }
            }
            catch (Exception ex)
            {
                errorMessages.Add($"the file url is error: {ex.Message}");
            }
            return errorMessages;
        }

        /// <summary>
        /// 验证文件类型
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        private async Task<List<string>> DoValidateFileExtension(string fileUrl)
        {
            var errorMessages = new List<string>();
            var allowFileExtensions = GetAllowFileExtensions();
            //fileExtensions: test.pptx?version=1
            string fileExtensions = Path.GetExtension(fileUrl) ?? string.Empty;
            fileExtensions = fileExtensions.Split('?')[0];
            if (string.IsNullOrEmpty(fileExtensions))
            {
                errorMessages.Add("the file extension is unknown");
            }
            else if (!allowFileExtensions.Contains(fileExtensions))
            {
                errorMessages.Add($"the file extension '{fileExtensions}' is not allowed");
            }
            return await Task.FromResult(errorMessages);
        }

        #endregion
    }
}
