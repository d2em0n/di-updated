using Autofac;
using TagsCloudContainer;
using TagsCloudContainer.ColorProviders;
using TagsCloudContainer.Configuration;
using TagsCloudContainer.PointGenerators;
using TagsCloudContainer.StringParsers;
using TagsCloudContainer.TagGenerator;
using TagsCloudContainer.TextProcessor;
using TagsCloudContainer.TextProviders;
using TagsCloudContainer.WordFilters;

namespace Client;

public class DependencyInjection
{
    public static IContainer BuildContainer(Config config)
    {
        var container = new ContainerBuilder();
        container.RegisterInstance(config).AsSelf();

        if (config.FilePath.EndsWith(".txt"))
            container.RegisterType<TxtTextProvider>()
                .As<ITextProvider>()
                .WithParameter("filePath", config.FilePath)
                .SingleInstance();

        container.RegisterType<TextProcessor>()
            .As<ITextProcessor>()
            .SingleInstance();

        container.RegisterType(config.PointGenerator)
            .As<IPointGenerator>()
            .SingleInstance();

        if (config.Color != null)
            container.RegisterType<ColorProvider>()
                .As<IColorProvider>()
                .WithParameter("color", config.Color)
                .SingleInstance();
        else
            container.RegisterType<RandomColorProvider>()
                .As<IColorProvider>()
                .SingleInstance();

        container.RegisterType<RegexParser>()
            .As<IStringParser>()
            .SingleInstance();

        container.RegisterType<TagGenerator>()
            .As<ITagsGenerator>()
            .WithParameter("defaultFont", config.Font)
            .SingleInstance();
        
      

        container.RegisterType<ToLowerFilter>().As<IWordFilter>()
            .SingleInstance();
        container.RegisterType<ShortWordFilter>().As<IWordFilter>()
            .SingleInstance();
        container.RegisterType<BoringWordFilter>().As<IWordFilter>()
            .SingleInstance();

        container.RegisterType<PictureMaker>()
            .AsSelf()
            .WithParameter("fileName", config.PicturePath)
            .WithParameter("startPoint", config.StartPoint)
            .SingleInstance();

        return container.Build();
    }
}