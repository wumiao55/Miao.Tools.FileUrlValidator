using System.Collections.Generic;

namespace Miao.Tools.FileUrlValidator
{
    public class CommonFileUrlValidator : FileUrlValidator
    {
        #region 属性

        /// <summary>
        /// 最大文件大小
        /// </summary>
        public override long MaxFileSize { get; protected set; } = 100 * 1024 * 1024;

        /// <summary>
        /// 允许的文本文件后缀名
        /// </summary>
        public override List<string> AllowTextFileExtensions { get; protected set; } = new List<string>
        {
            ".txt"
        };

        /// <summary>
        /// 允许的办公文件后缀名
        /// </summary>
        public override List<string> AllowOfficeFileExtensions { get; protected set; } = new List<string>
        {
            ".ppt",
            ".pptx",
            ".xls",
            ".xlsx",
            ".doc",
            ".docx",
            ".pdf"
        };

        /// <summary>
        /// 允许的压缩文件后缀名
        /// </summary>
        public override List<string> AllowArchiveFileExtensions { get; protected set; } = new List<string>
        {
            ".rar",
            ".zip",
            ".7z"
        };

        /// <summary>
        /// 允许的图片文件后缀名
        /// </summary>
        public override List<string> AllowImageFileExtensions { get; protected set; } = new List<string>
        {
            ".ico",
            ".jpeg",
            ".jpg",
            ".png",
            ".bmp",
            ".webp"
        };

        /// <summary>
        /// 允许的视频文件后缀名
        /// </summary>
        public override List<string> AllowVideoFileExtensions { get; protected set; } = new List<string>
        {
            ".avi",
            ".flv",
            ".mp4"
        };

        /// <summary>
        /// 允许的音频文件后缀名
        /// </summary>
        public override List<string> AllowAudioFileExtensions { get; protected set; } = new List<string>
        {
            ".aac",
            ".mp3"
        };

        #endregion
    }
}
