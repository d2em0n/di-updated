using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using TagsCloudContainer.TagGenerator;

namespace TagsCloudContainer.Configuration;

public class DependencyInjection
{
    public IContainer BuildContainer(Config config)
    {
        var container = new ContainerBuilder();

        container.RegisterType(config.PointGenerator)
            .AsImplementedInterfaces()
            .SingleInstance();

        //if (config.RandomColor)
        //    container.RegisterType<TagGenerator.TagGenerator>()
        //        .AsImplementedInterfaces()
        //        .SingleInstance();
        //else
        //{
        //    container.RegisterType<SingleColorTagGenerator>()
        //    .AsImplementedInterfaces()
        //        .SingleInstance();
        //}


        return container.Build();
    }
}