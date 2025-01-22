using FluentAssertions;
using TagsCloudContainer.StringParsers;

namespace TagsCloudContainer.Tests;

[TestFixture]
public class RegexParserShould
{
    private RegexParser _parser;

    [SetUp]
    public void Setup()
    {
        _parser = new RegexParser();
    }

    [Test]
    public void ReturnsOnlyWords()
    {
        var input = "This parser should -+- parse all #words, except any *symbols";
        
        var result = _parser.GetWordsFromString(input);
        var expected = new [] {"This", "parser", "should", "parse", "all", "words", "except", "any", "symbols"};
        
        result.IsSuccess.Should().BeTrue();
        result.GetValueOrThrow().ToList().Select(w => w.Value).Should().BeEquivalentTo(expected);
    }

    [Test]
    public void ReturnsNoWordsIfEmptyInput()
    {
        var input = "";
       
        var result = _parser.GetWordsFromString(input);

        result.Error.Should().Be("Input cannot be empty");
    }
}

