using Avalonia.Controls;
using Avalonia.Input;
using BatchProcess3.ViewModels;

namespace BatchProcess3.Views;

public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();
    }

    private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.ClickCount != 2)
            return;
        
        (DataContext as MainViewModel)?.SideMenuResizeCommand?.Execute(null);
    }
}