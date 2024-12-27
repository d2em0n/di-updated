namespace TagsCloudContainer.TextProviders;

public interface ITextProvider
{
    public string ReadFile(string filePath);
}