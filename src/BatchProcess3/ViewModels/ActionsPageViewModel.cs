using System;
using System.Collections.ObjectModel;
using System.Data.Common;
using BatchProcess3.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Linq;
using BatchProcess3.Views;

namespace BatchProcess3.ViewModels;

public partial class ActionsPageViewModel() : PageViewModel(ApplicationPageNames.Actions)
{
    private PrintProfileViewModel? _defaultPrinterProfile = new PrintProfileViewModel()
    {
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
    private ActionsPrintViewModel? _selectedPrintListItem;

    
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
            new ActionsPrintViewModel {Id = "1", JobName = "Print Only Drawings", Description = "Print Only Drawings Files", PrintDrawingRange = "0, 5, 7-8", PrintDrawings = true,  PrinterProfile = _defaultPrinterProfile},
            new ActionsPrintViewModel {Id = "2", JobName = "Print All Drawings Scale To Fit", Description = "Prints drawing scaled to fit the paper", PrintDrawings = true,  PrinterProfile = _defaultPrinterProfile},
            new ActionsPrintViewModel {Id = "3", JobName = "Print 3D Models A3", Description = "Print models as 3D Visual", PrintModels = true, PrinterProfile = _defaultPrinterProfile},
        ];

        PrinterProfiles =
        [
            _defaultPrinterProfile,
            new PrintProfileViewModel {Name = "Print Landscape", Description = "Print all files in landscape mode", Copies = 3}, // TODO: Populate Printer Seetings
            new PrintProfileViewModel {Name = "Print Portrait", Description = "Print all files in portrait mode", Copies = 1},
            new PrintProfileViewModel {Name = "Print B&W A3", Description = "Print all A3 print B&W", Copies = 3},
        ];
        
        // Update PrintLIstHast Items when collections changes
        PrintList.CollectionChanged += (_, _) => OnPropertyChanged(nameof(PrintListHasItems));

        if (PrintList.Count > 0)
        {
            // Select First Item
            PrintList.First().IsSelected = true;
            
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
    public void DeletePrintItem(string id)
    {
        // TODO: Pass this logic to a service that handles the database/storage/fetching

        if (PrintList.Count(x => x.Id == id) != 1)
        // TODO: Throw/Warn?
        return;


        DeletePrintItemFromUI(id);
        
        
        
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
            IsSelected = true,
            JobName = "New Print Item"
        };

        // Remove Item
        PrintList.Add(newItem);
    }

    [RelayCommand]
    public void CancelPrintItem()
    {
        // Ignore if nothing is selected
        if (SelectedPrintListItem == null)
            return;
        
        if (SelectedPrintListItem.IsNewItem)
        DeletePrintItemFromUI(SelectedPrintListItem.Id);
        
    }

    public void DeletePrintItemFromUI(string id)
    {
        int index = PrintList.IndexOf(PrintList.First(x => x.Id == id));
        
        
        PrintList.RemoveAt(index);
        
        if (index > 1) index--;

        if (PrintList.Count > 0)
            PrintList[index].IsSelected = true;
    }
}