## 文件url验证, 可设置验证文件类型,文件大小,
### 使用方法如下:
`
var fileUrlValidator = new CommonFileUrlValidator()
.SetMaxFileSize(10 * 1024 * 1024)
.SetAllowTextFileExtensions(".txt")
.SetAllowImageFileExtensions(".jpg", ".png")
var validateResult = await fileUrlValidator.ValidateAsync("https://www.test.com/1.jpg");
`
