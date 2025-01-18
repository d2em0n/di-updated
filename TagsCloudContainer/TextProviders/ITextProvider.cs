namespace TagsCloudContainer.TextProviders;

public interface ITextProvider
{
    public Result<string> ReadFile();
}