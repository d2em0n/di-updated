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

    public string ReadFile()
    {
        if (!File.Exists(_filePath))
            throw new FileNotFoundException();
        using var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read);
        var document = new HWPFDocument(stream);
        var range = document.GetRange();
        return range.Text;
    }
}