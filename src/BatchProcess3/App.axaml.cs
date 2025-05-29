using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Metadata;
using BatchProcess3.Data;
using BatchProcess3.Factories;
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

        collection.AddSingleton<Func<ApplicationPageNames, PageViewModel>>(x => name => name switch
        {
            ApplicationPageNames.Home => x.GetRequiredService<HomePageViewModel>(),
            ApplicationPageNames.Process => x.GetRequiredService<ProcessPageViewModel>(),
            ApplicationPageNames.Actions => x.GetRequiredService<ActionsPageViewModel>(),
            ApplicationPageNames.Macros => x.GetRequiredService<MacrosPageViewModel>(),
            ApplicationPageNames.Reporter => x.GetRequiredService<ReporterPageViewModel>(),
            ApplicationPageNames.History => x.GetRequiredService<HistoryPageViewModel>(),
            ApplicationPageNames.Settings => x.GetRequiredService<SettingsPageViewModel>(),
            _ => throw new InvalidOperationException(),
        });
        
        collection.AddSingleton<PageFactory>();
        
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