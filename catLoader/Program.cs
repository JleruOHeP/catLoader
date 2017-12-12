using System;
using Autofac;
using CatLoader.Configuration;
using CatLoader.Models;
using CatLoader.Services;

namespace CatLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = ContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<ICatLoaderService>();
                app.Run().GetAwaiter().GetResult();
            }
        }
    }
}
