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

            ConfigureApp(config)
                .OnFail(error => Console.WriteLine($"Ошибка конфигурирования: {error}"))
                .Then(Run);
        }

        private static void Run(Config config)
        {
            var container = DependencyInjection.BuildContainer(config);
            using var scope = container.BeginLifetimeScope();
            scope.Resolve<PictureMaker>().DrawPicture().OnFail(error => Console.WriteLine($"Ошибка обработки: {error}"))
                .Then(a => Console.WriteLine($"результат сохранен в {config.PicturePath}"));
        }

        private static Result<Config> ConfigureApp(Config config)
        {
            return ConfigureSupportedReadingFormats(config).AsResult()
                .Then(ConfigureFileSource)
                .Then(ConfigureCloudView)
                .Then(ConfigureColor)
                .Then(ConfigurePathToSave)
                .Then(ConfigureStartPoint)
                .Then(ConfigureFont);
        }

        private static Config ConfigureSupportedReadingFormats(Config config)
        {
            Console.WriteLine("Поддерживаются следующие форматы файлов для чтения:");
            var textProviders = FindImplemetations<ITextProvider>();
            foreach (var point in textProviders)
                Console.WriteLine("\t" + point.Key);
            config.SupportedReadingFormats = textProviders;
            return config;
        }

        private static Result<Config> ConfigureFont(Config config)
        {
            Console.WriteLine("Введите размер шрифта");
            if (!int.TryParse(Console.ReadLine(), out var fontSize))
                return Result.Fail<Config>("invalid fontSize");

            Console.WriteLine("Введите название шрифта");
            var fontName = Console.ReadLine();
            if (!CheckFont(fontName))
                return Result.Fail<Config>("invalid fontName");
            config.Font = new Font(fontName, fontSize);
            return Result.Ok(config);
        }

        private static bool CheckFont(string fontName)
        {
            var fontCollection = new InstalledFontCollection();
            return fontCollection.Families.Any(
                f => f.Name.Equals(fontName, StringComparison.InvariantCultureIgnoreCase));
        }

        private static Config ConfigurePathToSave(Config config)
        {
            var inp = ReadValue("Введите полный путь и название файла для сохранения");
            config.PicturePath = inp.Length == 0 ? "1.bmp" : inp;
            return config;
        }

        private static Result<Config> ConfigureStartPoint(Config config)
        {
            Console.WriteLine("Введите координаты центра поля для рисования" +
                              "\n При некорректном вводе координаты центра составят ( 1000, 1000)");
            var xLine = ReadValue("Координата Х");
            var yLine = ReadValue("Координата Y");
            if (int.TryParse(xLine, out var xResult) &&
                int.TryParse(yLine, out var yResult))
                config.StartPoint = new Point(xResult, yResult);
            else
                config.StartPoint = new Point(1000, 1000);
            return Result.Ok(config);
        }

        private static Result<Config> ConfigureFileSource(Config config)
        {
            var inp = ReadValue("Введите имя файла источника тэгов");
            if ((inp.Length != 0) &&
                !config.SupportedReadingFormats.TryGetValue(Path.GetExtension(inp), out var textProvider))
                return Result.Fail<Config>("wrong file format for text source");
            config.FilePath = inp.Length == 0 ? @"TestFile.txt" : inp;
            return Result.Ok(config);
        }

        private static string GetLabel(RainbowColors color)
        {
            var fieldInfo = color.GetType().GetField(color.ToString());
            var attribute = (LabelAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(LabelAttribute));

            return attribute.LabelText;
        }

        private static Config ConfigureColor(Config config)
        {
            Console.WriteLine("Выборите цвет из возможных:");
            var colors = Enum.GetValues(typeof(RainbowColors))
                .Cast<RainbowColors>()
                .ToDictionary(color => GetLabel(color).ToLower(), color => color);

            foreach (var color in colors)
                Console.WriteLine("\t" + color.Key);

            var inp = ReadValue("В случае неправильного ввода - цвет будет выбираться случайным образом")
                .ToLower();
            if (colors.TryGetValue(inp, out var colorName))
            {
                config.Color = Color.FromName(colorName.ToString());
                Console.WriteLine($"Выбран {inp} цвет");
            }
            else
                Console.WriteLine("Цвет будет выбираться случайно");

            return config;
        }

        private static Result<Config> ConfigureCloudView(Config config)
        {
            Console.WriteLine("Выберите внешний вид облака из возможных:");
            var pointGenerators = FindImplemetations<IPointGenerator>();
            foreach (var point in pointGenerators)
                Console.WriteLine("\t" + point.Key);
            Console.WriteLine("Введите, соблюдая орфографию");
            var pointGenerator = Console.ReadLine().ToLower();
            if (pointGenerators.TryGetValue(pointGenerator, out var pointGeneratorName))
            {
                config.PointGenerator = pointGeneratorName;
                return Result.Ok(config);
            }
            return Result.Fail<Config>("Такой формы не предусмотрено");
        }
        
        private static Dictionary<string, Type> FindImplemetations<T>()
        {
            var assembly = Assembly.LoadFrom("TagsCloudContainer.dll");
            var type = typeof(T);
            return assembly.GetTypes()
                .Where(t => type.IsAssignableFrom(t) && !t.IsInterface)
                .ToDictionary(x => x.GetCustomAttribute<LabelAttribute>().LabelText.ToLower(), x => x);
        }

        private static string ReadValue(string? argName = null)
        {
            Console.Write($"{argName ?? ""}: ");
            return Console.ReadLine();
        }
    }
}