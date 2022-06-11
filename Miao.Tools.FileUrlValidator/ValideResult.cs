using System.Collections.Generic;

namespace Miao.Tools.FileUrlValidator
{
    /// <summary>
    /// 验证结果
    /// </summary>
    public class ValideResult
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="errorMessages"></param>
        public ValideResult(bool isSuccess, List<string> errorMessages)
        {
            IsSuccess = isSuccess;
            ErrorMessages = errorMessages;
        }

        /// <summary>
        /// 是否验证成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public List<string> ErrorMessages { get; set; }
    }
}
