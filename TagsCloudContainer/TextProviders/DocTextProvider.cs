using NPOI.HWPF;

namespace TagsCloudContainer.TextProviders;

[Label(".doc")]
public class DocTextProvider : ITextProvider
{
    private readonly string _filePath;

    public DocTextProvider(string filePath)
    {
        _filePath = filePath;
    }

    public Result<string> ReadFile()
    {
        if (!File.Exists(_filePath))
            return new Result<string>($"File not found: {_filePath}");
        using var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read);
        var document = new HWPFDocument(stream);
        var range = document.GetRange();
        return range.Text;
    }
}