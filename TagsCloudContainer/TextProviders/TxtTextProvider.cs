namespace TagsCloudContainer.TextProviders;

[Label(".txt")]
public class TxtTextProvider : ITextProvider
{
    private readonly string _filePath;

    public TxtTextProvider(string filePath)
    {
        _filePath = filePath;
    }

    public string ReadFile()
    {
        if (!File.Exists(_filePath)) throw new FileNotFoundException();
        return File.ReadAllText(_filePath).ToLower();
    }
}