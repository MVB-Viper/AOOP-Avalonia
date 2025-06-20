﻿using System.Diagnostics.Contracts;
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

    [ObservableProperty] private bool _isNewItem;

    
    public string DrawingExclusionListTitle => (DrawingExclusionIsWhiteList ? "White List" : "Black List");
    
    [ObservableProperty][NotifyPropertyChangedFor(nameof(HasChanged))]
    private string _printerProfileId = "";

    [JsonIgnore]
    public bool HasChanged => IsNewItem || (_savedState != "" && _savedState != JsonSerializer.Serialize(this));


    public void SetSavedState()
    {
        _savedState = JsonSerializer.Serialize(this);
        
        OnPropertyChanged(nameof(HasChanged));
    }

    public void RestoreSavedState()
    {
        var savedState = JsonSerializer.Deserialize<ActionsPrintViewModel>(_savedState);

        foreach (var propertyInfo in GetType().GetProperties())
        {
            // Only setters, not get only properties
            if (!propertyInfo.CanWrite)
                continue;
            
            // Ignore any properties that have a JsonIgnore attribute
            if (propertyInfo.GetCustomAttributes(typeof(JsonIgnoreAttribute), false).GetLength(0) > 0)
                continue;
            
            // Pull the saved value
            var originalValue = propertyInfo.GetValue(savedState);
            
            // Restore it to this value
            propertyInfo.SetValue(this, originalValue);
        }
    }
}