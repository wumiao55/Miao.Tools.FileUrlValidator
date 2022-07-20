using System.Collections.Generic;
using System.Globalization;
using Miao.Tools.FileUrlValidator;
#nullable enable

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// 文件url校验特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false)]
    public class FileUrlValidationAttribute : ValidationAttribute
    {
        public FileUrlValidator FileUrlValidator { get; private set; }

        public List<string> ErrorMessages { get; private set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="fileUrlValidatorType">文件url校验器类型</param>
        /// <param name="isValidateFileExtension">是否校验文件扩展名,默认true</param>
        /// <param name="isValidateFileSize">是否校验文件大小,默认true</param>
        /// <exception cref="ArgumentException"></exception>
        public FileUrlValidationAttribute(Type fileUrlValidatorType, bool isValidateFileExtension = true, bool isValidateFileSize = true)
            : base(() => MySR.FileUrlValidationAttribute_ValidationError)
        {
            if (fileUrlValidatorType.IsAbstract || !typeof(FileUrlValidator).IsAssignableFrom(fileUrlValidatorType))
            {
                throw new ArgumentException($"the {fileUrlValidatorType.FullName} must be assigned to '{typeof(FileUrlValidator).FullName}'", nameof(fileUrlValidatorType));
            }
            FileUrlValidator = (Activator.CreateInstance(fileUrlValidatorType) as FileUrlValidator)!;
            FileUrlValidator
                .ValidateFileExtension(isValidateFileExtension)
                .ValidateFileSize(isValidateFileSize);
            ErrorMessages = new List<string>();
        }

        /// <summary>
        /// IsValid
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object? value)
        {
            if (value == null || (value as string)?.Length == 0)
            {
                return true;
            }
            if (!(value is string))
            {
                return true;
            }

            var validateResult = FileUrlValidator.ValidateAsync(value.ToString()!).Result;
            ErrorMessages = validateResult.ErrorMessages;
            return validateResult.IsSuccess;
        }

        /// <summary>
        /// FormatErrorMessage
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override string FormatErrorMessage(string name)
        {
            string errorMsgs = string.Join(";", ErrorMessages);
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, errorMsgs);
        }

    }
}
