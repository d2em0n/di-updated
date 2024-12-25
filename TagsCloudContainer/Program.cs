using TagsCloudContainer.TextProviders;
using TagsCloudContainer.WordFilters;

namespace TagsCloudContainer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var provider = new TxtTextProvider();
            var filter = new SimpleFilter();
            var processor = new TextProcessor.TextProcessor(@"C:\test\test.txt", provider, filter);
            foreach (var word in processor.Words)
            {
                Console.WriteLine(word.Key.Value + " : " + word.Value);
            }
        }
    }
}
