using Microsoft.Extensions.DependencyInjection;
using MVVM_Antipub.Models;
using MVVM_Antipub.ViewModels;
using System;

namespace MVVM_Antipub
{
    public static class AppHost
    {
        private static ServiceProvider _provider;

        public static void ConfigureServices()
        {
            var services = new ServiceCollection();

            // Сервисы
            services.AddSingleton<IFileService, JsonFileService>();

            // ViewModels
            services.AddSingleton<MainWindowViewModel>();
            services.AddTransient<DBShowWindowViewModel>();
            services.AddTransient<NewNoteViewModel>();
            services.AddSingleton<NewTariffViewModel>();
            services.AddSingleton<TariffChangeViewModel>();

            _provider = services.BuildServiceProvider();
        }

        public static T GetService<T>() => _provider.GetRequiredService<T>();
    }
}
