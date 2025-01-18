﻿using Xceed.Words.NET;

namespace TagsCloudContainer.TextProviders;

[Label(".docx")]
public class DocXTextProvider : ITextProvider
{
    private readonly string _filePath;

    public DocXTextProvider(string filePath)
    {
        _filePath = filePath;
    }

    public Result<string> ReadFile()
    {
        if (!File.Exists(_filePath)) 
            return Result.Fail<string>($"File {_filePath} does not exist");
        using var document = DocX.Load(_filePath);
        return document.Text;
    }
}