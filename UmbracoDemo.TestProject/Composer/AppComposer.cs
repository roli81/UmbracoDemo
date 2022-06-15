using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using UmbracoDemo.TestProject.Components;
using UmbracoDemo.TestProject.Services;

namespace UmbracoDemo.TestProject.Composer
{
    public class AppComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            RegisterServices(builder);
            AddComponents(builder);
        }


        private void AddComponents(IUmbracoBuilder builder)
        {
            builder.Components().Append<TestComponent>();
        }

        private void RegisterServices(IUmbracoBuilder builder)
        {
            builder.Services.AddSingleton<IImageService, ImageService>();
            builder.Services.AddScoped<IWidgetService, WidgetService>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddScoped<IMachineService, MachineService>();
        }
    }
}
