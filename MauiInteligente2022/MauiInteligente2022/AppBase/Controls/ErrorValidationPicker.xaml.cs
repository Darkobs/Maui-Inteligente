using MauiInteligente2022.AppBase.Validations;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using System.Collections;

namespace MauiInteligente2022.AppBase.Controls;

public partial class ErrorValidationPicker : VerticalStackLayout
{
    public static readonly BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(ValidationResult), typeof(ErrorValidationPicker), ValidationResult.None, defaultBindingMode: BindingMode.TwoWay, propertyChanged: IsValidChanged);
    public ValidationResult IsValid
    {
        get => (ValidationResult)GetValue(IsValidProperty);
        set => SetValue(IsValidProperty, value);
    }
    public static readonly BindableProperty LineColorProperty =
        BindableProperty.CreateAttached("LineColor", typeof(Color), typeof(ErrorValidationPicker), Colors.Black);
    public static Color GetLineColor(BindableObject view)
    {
        return (Color)view.GetValue(LineColorProperty);
    }
    public static void SetLineColor(BindableObject view, Color value)
    {
        view.SetValue(LineColorProperty, value);
    }
    public static readonly BindableProperty IsObligatoryProperty = BindableProperty.Create(nameof(IsObligatory), typeof(bool), typeof(ErrorValidationPicker), true, propertyChanged: IsObligatotyPropertyChanged);
    public bool IsObligatory
    {
        get => (bool)GetValue(IsObligatoryProperty);
        set => SetValue(IsObligatoryProperty, value);
    }
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(ErrorValidationPicker), string.Empty, propertyChanged: TitlePropertyChanged);
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(ErrorValidationPicker), null, defaultBindingMode: BindingMode.TwoWay);
    public IList ItemsSource
    {
        get => (IList)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }
    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(ErrorValidationPicker), null, defaultBindingMode: BindingMode.TwoWay);
    public object SelectedItem
    {
        get => (object)GetValue(SelectedItemProperty);
        set
        {
            SetValue(SelectedItemProperty, value);
            IsValid = ValidationResult.None;
        }
    }
    public static readonly BindableProperty ErrorDescriptionProperty = BindableProperty.Create(nameof(ErrorDescription), typeof(string), typeof(ErrorValidationPicker), string.Empty, defaultBindingMode: BindingMode.TwoWay);
    public string ErrorDescription
    {
        get => (string)GetValue(ErrorDescriptionProperty);
        set
        {
            SetValue(ErrorDescriptionProperty, value?.ToUpperInvariant());
        }
    }
    static void IsValidChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ErrorValidationPicker errorPicker)
        {
            var view = errorPicker.Children[1] as Picker;
            var handler = view.Handler as PickerHandler;
            var color = (ValidationResult)newValue != ValidationResult.Invalid ? Colors.Black : Colors.Red;
            errorPicker.lbError.IsVisible = (ValidationResult)newValue == ValidationResult.Invalid;
#if __ANDROID__
            handler.PlatformView.Background.SetColorFilter(AndroidX.Core.Graphics.BlendModeColorFilterCompat.CreateBlendModeColorFilterCompat(color.ToPlatform(), AndroidX.Core.Graphics.BlendModeCompat.SrcAtop));
#elif IOS
            handler.PlatformView.Layer.CornerRadius = 8.0f;
            handler.PlatformView.Layer.MasksToBounds = true;
            handler.PlatformView.Layer.BorderColor = color.ToPlatform().CGColor;
            handler.PlatformView.Layer.BorderWidth = 1.0f;
#endif
        }
    }
    static void IsObligatotyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var errorEntry = bindable as ErrorValidationPicker;
        errorEntry.lbIndicator.IsVisible = (bool)newValue;
    }
    static void TitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var errorPicker = bindable as ErrorValidationPicker;
        errorPicker.picker.Title = (string)newValue;
        errorPicker.lbTitle.Text = (string)newValue;
    }
    public ErrorValidationPicker()
    {
        InitializeComponent();
        IsObligatory = false;
        picker.BindingContext = this;
        lbError.BindingContext = this;
    }
}