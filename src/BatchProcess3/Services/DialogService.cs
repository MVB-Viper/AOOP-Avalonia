using System.Threading.Tasks;
using Avalonia.Controls;
using BatchProcess3.Interface;
using BatchProcess3.ViewModels;

namespace BatchProcess3.Services;

public class DialogService
{
    public async Task ShowDialog<THost, TDialogViewModel>(THost host, TDialogViewModel dialogViewModel)
        where TDialogViewModel : DialogViewModel
        where THost : IDialogProvider
    {
        // Set Host Dialog to provided on
        host.Dialog = dialogViewModel;
        dialogViewModel.Show();
        
        // Wait for dialog to close
        await dialogViewModel.WaitAsync();
    }
}