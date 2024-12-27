using TagsCloudContainer.StringParsers;
using TagsCloudContainer.TextProviders;
using TagsCloudContainer.WordFilters;

namespace TagsCloudContainer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var provider = new TxtTextProvider();
            var filter = new BoringWordFilter();
            var processor = new TextProcessor.TextProcessor();
            var parser = new RegexParser();
            foreach (var word in processor.Words(@"C:\test\test.txt", provider, parser, filter))
            {
                Console.WriteLine(word.Key.Value + " : " + word.Value);
            }
        }
    }
}
