using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using System;
using Avalonia.Media;

namespace BatchProcess3.Controls;

public class IconButtonDuplicate : Button
{
    public static readonly StyledProperty<string> IconTextProperty = AvaloniaProperty.Register<IconButtonDuplicate, string>(
        nameof(IconText));

    public static readonly StyledProperty<Brush> IconColorProperty = AvaloniaProperty.Register<IconButtonDuplicate, Brush>(
        nameof(IconColorProperty));
    
    public string IconText
    {
        get => GetValue(IconTextProperty);
        set => SetValue(IconTextProperty, value);
    }

    public Brush IconColor
    {
        get => GetValue(IconColorProperty);
        set => SetValue(IconColorProperty, value);
    }
}