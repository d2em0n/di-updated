using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace TagsCloudContainer.Configuration;

public class DependencyInjection
{
    public IContainer BuildContainer(Config config)
    {
        var container = new ContainerBuilder();

        container.RegisterType(config.PointGenerator)
            .AsImplementedInterfaces();


        return container.Build();
    }
}