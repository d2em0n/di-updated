namespace TagsCloudContainer.TextProviders;

[Label(".txt")]
public class TxtTextProvider : ITextProvider
{
    private readonly string _filePath;

    public TxtTextProvider(string filePath)
    {
        _filePath = filePath;
    }

    public Result<string> ReadFile()
    {
        return !File.Exists(_filePath) 
            ? Result.Fail<string>($"File {_filePath} does not exist") 
            : Result.Of(()=> File.ReadAllText(_filePath));
    }
}