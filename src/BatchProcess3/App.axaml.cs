using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Metadata;
using BatchProcess3.Data;
using BatchProcess3.Factories;
using BatchProcess3.Services;
using BatchProcess3.ViewModels;
using Microsoft.Extensions.DependencyInjection;

[assembly: XmlnsDefinition("https://github.com/avaloniaui", "BatchProcess3.Controls")]

namespace BatchProcess3;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var collection = new ServiceCollection();
        collection.AddSingleton<MainViewModel>();
        collection.AddTransient<ActionsPageViewModel>();
        collection.AddTransient<HistoryPageViewModel>();
        collection.AddTransient<HomePageViewModel>();
        collection.AddTransient<MacrosPageViewModel>();
        collection.AddTransient<ProcessPageViewModel>();
        collection.AddTransient<ReporterPageViewModel>();
        collection.AddTransient<SettingsPageViewModel>();

        collection.AddSingleton<Func<Type, PageViewModel>>(x => type => type switch
        {
            _ when type == typeof(HomePageViewModel) => x.GetRequiredService<HomePageViewModel>(),
            _ when type == typeof(ProcessPageViewModel) => x.GetRequiredService<ProcessPageViewModel>(),
            _ when type == typeof(ActionsPageViewModel) => x.GetRequiredService<ActionsPageViewModel>(),
            _ when type == typeof(MacrosPageViewModel)  => x.GetRequiredService<MacrosPageViewModel>(),
            _ when type == typeof(ReporterPageViewModel)  => x.GetRequiredService<ReporterPageViewModel>(),
            _ when type == typeof(HistoryPageViewModel)  => x.GetRequiredService<HistoryPageViewModel>(),
            _ when type == typeof(SettingsPageViewModel)  => x.GetRequiredService<SettingsPageViewModel>(),
            _ => throw new InvalidOperationException(),
        });
        
        collection.AddSingleton<PageFactory>();
        collection.AddSingleton<DialogService>();
        
        var serviceProvider = collection.BuildServiceProvider();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new Views.MainView
            {
                DataContext = serviceProvider.GetRequiredService<MainViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}