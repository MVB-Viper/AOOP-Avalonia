using System.Collections.Generic;
using BatchProcess3.Data;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BatchProcess3.ViewModels;

public partial class SettingsPageViewModel : PageViewModel
{
    [ObservableProperty]
    private List<string> _locationsPaths;
    
    public string Test { get; set; } = "Settings";

    public SettingsPageViewModel() : base(ApplicationPageNames.Settings)
    {
        
        // TEMP: Remove
        LocationsPaths =
        [
            @"C:\Users\Mvb007\Downloads\TestActions",
            @"C:\Users\Mvb007\Documents\BatchProcess",
            @"C:\Users\Mvb007\BatchProcess\Templates",
        ];
    }
}