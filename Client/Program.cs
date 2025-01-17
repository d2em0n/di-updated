using System.Drawing;
using System.Drawing.Text;
using System.Reflection;
using TagsCloudContainer.Configuration;
using TagsCloudContainer.PointGenerators;
using TagsCloudContainer.TextProviders;
using TagsCloudContainer;
using Autofac;
using NPOI.SS.Util.CellWalk;

namespace Client
{
    internal class Program
    {
        static void Main()
        {
            var config = new Config();

            ConfigureSupportedReadingFormats(config);
            ConfigureFileSource(config);
            ConfigureCloudView(config);
            ConfigureColor(config);
            ConfigurePathToSave(config);
            ConfigureStartPoint(config);
            ConfigureFont(config);

            var container = DependencyInjection.BuildContainer(config);
            using var scope = container.BeginLifetimeScope();
            scope.Resolve<PictureMaker>().DrawPicture();
            Console.WriteLine($"результат сохранен в {config.PicturePath}");
        }

        private static void ConfigureSupportedReadingFormats(Config config)
        {
            Console.WriteLine("Поддерживаются следующие форматы файлов для чтения:");
            var textProviders = FindImplemetations<ITextProvider>();
            foreach (var point in textProviders)
                Console.WriteLine("\t" + point.Key);
            config.SupportedReadingFormats = textProviders;
        }

        private static void ConfigureFont(Config config)
        {
            Console.WriteLine("Введите размер шрифта");
            if (!int.TryParse(Console.ReadLine(), out var fontSize))
                throw new Exception("invalid fontSize");

                Console.WriteLine("Введите название шрифта");
            var fontName = Console.ReadLine();
            if (!CheckFont(fontName))
                throw new Exception("invalid fontName");
            config.Font = new Font(fontName, fontSize);
        }

        private static bool CheckFont(string fontName)
        {
            var fontCollection = new InstalledFontCollection();
            return fontCollection.Families.Any(f => f.Name.Equals(fontName, StringComparison.InvariantCultureIgnoreCase));
        }

        private static void ConfigurePathToSave(Config config)
        {
            Console.WriteLine("Введите полный путь и название файла для сохранения");
            var inp = Console.ReadLine();
            config.PicturePath = inp.Length == 0 ? "1.bmp" : inp;
        }

        private static void ConfigureStartPoint(Config config)
        {
            Console.WriteLine("Введите координаты центра поля для рисования" +
                              "\n При некорректном вводе координаты центра составят ( 1000, 1000)");
            var xLine = ReadValue("Координата Х");
            var yLine = ReadValue("Координата Y");
            if (int.TryParse(xLine, out var xResult) &&
                int.TryParse(yLine, out var yResult))
                config.StartPoint = new Point(xResult, yResult);
            config.StartPoint = new Point(1000, 1000);
        }

        private static void ConfigureFileSource(Config config)
        {
            Console.WriteLine("Введите имя файла источника тэгов");
            var inp = Console.ReadLine();
            if ((inp.Length != 0) && !config.SupportedReadingFormats.TryGetValue(Path.GetExtension(inp), out var textProvider))
                throw new Exception("wrong file format for text source");
            config.FilePath = inp.Length == 0 ? @"TestFile.txt" : inp;
        }

        private static string GetLabel(RainbowColors color)
        {
            var fieldInfo = color.GetType().GetField(color.ToString());
            var attribute = (LabelAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(LabelAttribute));

            return attribute.LabelText;
        }

        private static void ConfigureColor(Config config)
        {
            Console.WriteLine("Выборите цвет из возможных:");
            var colors = Enum.GetValues(typeof(RainbowColors))
                .Cast<RainbowColors>()
                .ToDictionary(color => GetLabel(color).ToLower(), color => color);

            foreach (var color in colors)
                Console.WriteLine("\t" + color.Key);

            Console.WriteLine("В случае неправильного ввода - цвет будет выбираться случайным образом");
            var inp = Console.ReadLine().ToLower();
            if (colors.TryGetValue(inp, out var colorName))
            {
                config.Color = Color.FromName(colorName.ToString());
                Console.WriteLine($"Выбран {inp} цвет");
            }
            else
                Console.WriteLine("Цвет будет выбираться случайно");
        }

        private static void ConfigureCloudView(Config config)
        {
            Console.WriteLine("Выберите внешний вид облака из возможных:");
            var pointGenerators = FindImplemetations<IPointGenerator>();
            foreach (var point in pointGenerators)
                Console.WriteLine("\t" + point.Key);
            Console.WriteLine("Введите, соблюдая орфографию");
            var pointGenerator = Console.ReadLine().ToLower();
            if (pointGenerators.TryGetValue(pointGenerator, out var pointGeneratorName))
                config.PointGenerator = pointGeneratorName;
            else
            {
                Console.WriteLine("Такой формы не предусмотрено");
                ConfigureCloudView(config);
            }
        }

        private static Dictionary<string, Type> FindImplemetations<T>()
        {
            var assembly = Assembly.LoadFrom("TagsCloudContainer.dll");
            var type = typeof(T);
            return assembly.GetTypes()
                .Where(t => type.IsAssignableFrom(t) && !t.IsInterface)
                .ToDictionary(x => x.GetCustomAttribute<LabelAttribute>().LabelText.ToLower(), x => x);
        }

        private static string? ReadValue(string? argName = null)
        {
            Console.Write($"{argName ?? ""}: ");
            return Console.ReadLine();
        }
    }
}