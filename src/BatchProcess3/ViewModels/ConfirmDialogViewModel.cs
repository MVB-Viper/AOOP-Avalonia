using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BatchProcess3.ViewModels;

public partial class ConfirmDialogViewModel : DialogViewModel
{

    [ObservableProperty] private string _title = "Confirm";
    [ObservableProperty] private string _message = "Are you sure?";
    [ObservableProperty] private string _confirmText = "Yes";
    [ObservableProperty] private string _cancelText = "No";
    [ObservableProperty] private string _iconText = "\xe4e0";
    [ObservableProperty] private string _statusText = "";
    [ObservableProperty] private string _progressText = "";
    
    [ObservableProperty] private double _dialogWidth = double.NaN;
    [ObservableProperty] private bool _isBusy = false;
    [ObservableProperty] private bool _showingStatus = false;
    
    [ObservableProperty]
    private bool _confirmed;

    public Func<ConfirmDialogViewModel, Task<bool>> OnConfirm { get; set; } = (_) => Task.FromResult(true);
    
    [RelayCommand]
    public async Task ConfirmAsync()
    {
        if (IsBusy)
            return;
        IsBusy = true;

        // Clear status text
        StatusText = "";
        ShowingStatus = false;
        
        // Set initial progress text
        ProgressText = "Processing...";

        var result = await OnConfirm(this);
        
        IsBusy = false;
        
        if (!result)
            return;
        
        Confirmed = true;
        
        Close();
    }

    [RelayCommand]
    public void Cancel()
    {
        Confirmed = false;
        Close();
    }
}