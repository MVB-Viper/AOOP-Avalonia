using System;
using Avalonia.Svg.Skia;
using BatchProcess3.Data;
using BatchProcess3.Factories;
using BatchProcess3.Interface;
using BatchProcess3.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BatchProcess3.ViewModels;

public partial class MainViewModel : ViewModelBase, IDialogProvider
{
    private PageFactory _pageFactory;
    
    [ObservableProperty]
    //[NotifyPropertyChangedFor(nameof(SideMenuImage))]
    private bool _sideMenuExpanded = true;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HomePageIsActive))]
    [NotifyPropertyChangedFor(nameof(ProcessPageIsActive))]
    [NotifyPropertyChangedFor(nameof(ActionsPageIsActive))]
    [NotifyPropertyChangedFor(nameof(MacrosPageIsActive))]
    [NotifyPropertyChangedFor(nameof(ReporterPageIsActive))]
    [NotifyPropertyChangedFor(nameof(HistoryPageIsActive))]
    [NotifyPropertyChangedFor(nameof(SettingsPageIsActive))]
    private PageViewModel _currentPage;
    
    [ObservableProperty]
    private DialogViewModel _dialog;
    
    // ButtonClasses
    public bool HomePageIsActive => CurrentPage.PageName == ApplicationPageNames.Home;
    public bool ProcessPageIsActive => CurrentPage.PageName == ApplicationPageNames.Process;
    public bool ActionsPageIsActive => CurrentPage.PageName == ApplicationPageNames.Actions;
    public bool MacrosPageIsActive => CurrentPage.PageName == ApplicationPageNames.Macros;
    public bool ReporterPageIsActive => CurrentPage.PageName == ApplicationPageNames.Reporter;
    public bool HistoryPageIsActive => CurrentPage.PageName == ApplicationPageNames.History;
    public bool SettingsPageIsActive => CurrentPage.PageName == ApplicationPageNames.Settings;
    
    
    // public SvgImage SideMenuImage => new SvgImage
    // {
    //     Source = SvgSource.Load($"avares://{nameof(BatchProcess3)}/Assets/Images/{(SideMenuExpanded ? "_logo" : "icon")}.svg")
    // };

    /// <summary>
    /// Design-time only Constructor
    /// </summary>
    public MainViewModel()
    {
        CurrentPage = new SettingsPageViewModel();
    }

    public MainViewModel(PageFactory pageFactory)
    {
        _pageFactory = pageFactory ?? throw new ArgumentNullException(nameof(pageFactory));
        CurrentPage = _pageFactory.GetPageViewModel<SettingsPageViewModel>();

        GoToHome();
    }
    
    [RelayCommand]
    private void SideMenuResize()
    {
        SideMenuExpanded = !SideMenuExpanded;
    }

    [RelayCommand]
    private void GoToHome() => CurrentPage = _pageFactory.GetPageViewModel<HomePageViewModel>();

    [RelayCommand]
    private void GoToProcess() => CurrentPage = _pageFactory.GetPageViewModel<ProcessPageViewModel>();
    
    [RelayCommand]
    private void GoToActions() => CurrentPage = _pageFactory.GetPageViewModel<ActionsPageViewModel>();
    
    [RelayCommand]
    private void GoToMacros() => CurrentPage = _pageFactory.GetPageViewModel<MacrosPageViewModel>();
    
    [RelayCommand]
    private void GoToReporter() => CurrentPage = _pageFactory.GetPageViewModel<ReporterPageViewModel>();
    
    [RelayCommand]
    private void GoToHistory() => CurrentPage = _pageFactory.GetPageViewModel<HistoryPageViewModel>();

    [RelayCommand]
    private void GoToSettings() => CurrentPage = _pageFactory.GetPageViewModel<SettingsPageViewModel>();

    
}

