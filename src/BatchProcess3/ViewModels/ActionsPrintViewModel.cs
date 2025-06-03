using System.Text.Json;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BatchProcess3.ViewModels;

public partial class ActionsPrintViewModel : ViewModelBase
{
    [property: JsonIgnore]
    private string _savedState = "";
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasChanged))]
    private string? _id  = "";

    [ObservableProperty][NotifyPropertyChangedFor(nameof(HasChanged))] private string _jobName = "";

    [ObservableProperty][NotifyPropertyChangedFor(nameof(HasChanged))] private string _description = "";

    [ObservableProperty][NotifyPropertyChangedFor(nameof(HasChanged))] private string _printDrawingRange = "";
    [ObservableProperty][NotifyPropertyChangedFor(nameof(HasChanged))] private string _drawingExclusionList = "";
    [ObservableProperty][NotifyPropertyChangedFor(nameof(HasChanged))] private string _drawingExclusionWhiteList = "";
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasChanged))]
    [NotifyPropertyChangedFor(nameof(DrawingExclusionListTitle))]
    private bool _drawingExclusionIsWhiteList;
    
    [ObservableProperty][NotifyPropertyChangedFor(nameof(HasChanged))] private bool _printModels;
    [ObservableProperty][NotifyPropertyChangedFor(nameof(HasChanged))] private bool _printDrawings;
    
    [ObservableProperty]
    [property: JsonIgnore]
    private bool _isSelected;

    [ObservableProperty] private bool _isNewItem;

    public string DrawingExclusionListTitle => (DrawingExclusionIsWhiteList ? "White List" : "Black List");
    
    [ObservableProperty][NotifyPropertyChangedFor(nameof(HasChanged))]
    private PrintProfileViewModel? _printerProfile;

    [JsonIgnore]
    public bool HasChanged => IsNewItem || (_savedState != "" && _savedState != JsonSerializer.Serialize(this));


    public void SetSavedState()
    {
        _savedState = JsonSerializer.Serialize(this);
        
        OnPropertyChanged(nameof(HasChanged));
    }
}