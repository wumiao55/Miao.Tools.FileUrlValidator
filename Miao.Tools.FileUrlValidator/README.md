## �ļ�url��֤, ��������֤�ļ�����,�ļ���С,
### ʹ�÷�������:
`
var fileUrlValidator = new CommonFileUrlValidator()
	.SetMaxFileSize(10 * 1024 * 1024)
    .SetAllowTextFileExtensions(".txt")
    .SetAllowImageFileExtensions(".jpg", ".png")
var validateResult = await fileUrlValidator.ValidateAsync("https://www.test.com/1.jpg");
`