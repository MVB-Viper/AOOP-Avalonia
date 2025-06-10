using System;
using System.Collections.ObjectModel;
using System.Data.Common;
using BatchProcess3.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Linq;
using System.Threading.Tasks;
using BatchProcess3.Services;
using BatchProcess3.Views;

namespace BatchProcess3.ViewModels;

public partial class ActionsPageViewModel(MainViewModel mainViewModel, DialogService dialogService) : PageViewModel(ApplicationPageNames.Actions)
{
    private PrintProfileViewModel? _defaultPrinterProfile = new PrintProfileViewModel()
    {
        Id = "0",
        Name = "(Default)",
        Description = "Print with default settings",
        Copies = 1,
    };
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PrintListHasItems))]
    private ObservableCollection<ActionsPrintViewModel> _printList = [];
    
    public bool PrintListHasItems => PrintList.Any();
    
    [ObservableProperty]
    private ObservableCollection<PrintProfileViewModel> _printerProfiles = [];
    
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SelectedPrintListItem))]
    private string _selectedPrintListItemId = "";

    public ActionsPrintViewModel? SelectedPrintListItem =>
        PrintList.FirstOrDefault(f => f.Id == SelectedPrintListItemId);
    
    [RelayCommand]
    public void RefreshActionsPage(ActionsPageName actionsPageName)
    {
        switch (actionsPageName)
        {
            case ActionsPageName.Print: FetchPrintActionsData(); break;
        }
    }
    
    [RelayCommand]
    private void FetchPrintActionsData()
    {
        // TODO: Fetch form a database/service Provider
        PrintList =
        [
            new ActionsPrintViewModel {Id = "1", JobName = "Print Only Drawings", Description = "Print Only Drawings Files", PrintDrawingRange = "0, 5, 7-8", PrintDrawings = true,  PrinterProfileId = "1"},
            new ActionsPrintViewModel {Id = "2", JobName = "Print All Drawings Scale To Fit", Description = "Prints drawing scaled to fit the paper", PrintDrawings = true,  PrinterProfileId = "2"},
            new ActionsPrintViewModel {Id = "3", JobName = "Print 3D Models A3", Description = "Print models as 3D Visual", PrintModels = true, PrinterProfileId = "3"},
        ];

        PrinterProfiles =
        [
            _defaultPrinterProfile,
            new PrintProfileViewModel {Id = "1", Name = "Print Landscape", Description = "Print all files in landscape mode", Copies = 3}, // TODO: Populate Printer Seetings
            new PrintProfileViewModel {Id = "2", Name = "Print Portrait", Description = "Print all files in portrait mode", Copies = 1},
            new PrintProfileViewModel {Id = "3", Name = "Print B&W A3", Description = "Print all A3 print B&W", Copies = 3},
        ];
        
        // Update PrintLIstHast Items when collections changes
        PrintList.CollectionChanged += (_, _) => OnPropertyChanged(nameof(PrintListHasItems));

        if (PrintList.Count > 0)
        {
            // Select First Item
            SelectedPrintListItemId =  PrintList.First().Id;
            
            // Load last fetched items saved state
            foreach (var printItem in PrintList)
            {
                printItem.SetSavedState();
            }
        }
            
    }

    protected override void OnDesignTimeConstructor()
    {
        FetchPrintActionsData();
    }
    
    [RelayCommand]
    public async Task DeletePrintItemAsync(string id)
    {
        // TODO: Pass this logic to a service that handles the database/storage/fetching

        if (PrintList.Count(x => x.Id == id) != 1)
        // TODO: Throw/Warn?
        return;


        await DeletePrintItemFromUIAsync(id);
        
        
        
        // Remove Item
        //PrintList.Remove(PrintList.First(x => x.Id == id));
    }
    
    
    [RelayCommand]
    public void AddNewPrintItem()
    {
        // Create a new item
        var newItem = new ActionsPrintViewModel
        {
            Id = Guid.NewGuid().ToString("N"),
            IsNewItem = true,
            JobName = "New Print Item",
            PrinterProfileId = "0"
        };

        
        // Add Item to print list
        PrintList.Add(newItem);
        
        // Select Item
        SelectedPrintListItemId = newItem.Id;
    }

    [RelayCommand]
    public async Task CancelPrintItemAsync()
    {
        // Ignore if nothing is selected
        if (SelectedPrintListItem == null)
            return;
        
        if (SelectedPrintListItem.IsNewItem)
            await DeletePrintItemFromUIAsync(SelectedPrintListItem.Id, warn: false);
        else
        {
            SelectedPrintListItem.RestoreSavedState();
        }
            
        
    }

    private async Task DeletePrintItemFromUIAsync(string id, bool warn = true)
    {

        int index = PrintList.IndexOf(PrintList.First(x => x.Id == id));
        
        if (index == -1)
            return;
        
        if (warn) {
            var confirmViewModel = new ConfirmDialogViewModel
            {
                Title = $"Delete {PrintList[index].JobName}?",
                Message = $"Are you sure you want to delete this print?",
                /*OnConfirm = async (vm) =>
                {
                    await Task.Delay(2000);
                    
                    vm.ProgressText = "Deleting...";
                    
                    await Task.Delay(2000);
                    
                    
                    return true;

                }*/
            };
            
            await dialogService.ShowDialog(mainViewModel, confirmViewModel);

            // Return if cancel is pressed
            if (!confirmViewModel.Confirmed)
                return;
        }
        PrintList.RemoveAt(index);
        
        if (index > 1) index--;

        if (PrintList.Count > 0)
            SelectedPrintListItemId = PrintList[index].Id;
    }
}