using Xceed.Words.NET;

namespace TagsCloudContainer.TextProviders;

[Label(".docx")]
public class DocXTextProvider : ITextProvider
{
    private readonly string _filePath;

    public DocXTextProvider(string filePath)
    {
        _filePath = filePath;
    }

    public string ReadFile()
    {
        if (!File.Exists(_filePath)) throw new FileNotFoundException();
        else
            using (var document = DocX.Load(_filePath))
                return document.Text;
    }
}