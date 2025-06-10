using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BatchProcess3.Data;
using BatchProcess3.ViewModels;

namespace BatchProcess3.Views;

public partial class ActionsPageView : UserControl
{
    public ActionsPageView()
    {
        InitializeComponent();
    }

    private void ActionsTab_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (Equals(e.Source, ActionsTabControl)) OnTabChanged();   
    }  
    

    private void OnTabChanged()
    {
        // Get Active tab Control (Pages inside of each tab)
        var selectedPage = (ActionsTabControl?.SelectedItem as TabItem)?.Content as Control;
        
        if (selectedPage == null)
            return;

        // Convert to ActiosnPage Name
        var actionsPage = selectedPage switch
        {
            ActionsPrintView => ActionsPageName.Print,
            _ => ActionsPageName.Unknown,
        };
        
        // Get View Model
        var viewModel = selectedPage.DataContext as ActionsPageViewModel;
        
        // Type Check
        viewModel?.RefreshActionsPage(actionsPage);
    }

    protected override void OnInitialized()
    {
        // Fire off initial refresh
        OnTabChanged();
        
        base.OnInitialized();
    }
}