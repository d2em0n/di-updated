namespace TagsCloudContainer.TextProviders;

public class TxtTextProvider : ITextProvider
{
    public string ReadFile(string filePath)
    {
        if (!File.Exists(filePath)) throw new FileNotFoundException();
        return File.ReadAllText(filePath).ToLower();
    }
}