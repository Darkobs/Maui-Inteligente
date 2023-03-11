using MauiInteligente2022.AppBase.Validations;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace MauiInteligente2022.AppBase.Controls;

public partial class ErrorValidationEntry : VerticalStackLayout
{
	public ErrorValidationEntry()
	{
		InitializeComponent();
        //BindingContext = this;
        entry.BindingContext = this;
        lbError.BindingContext = this;
	}

    public static readonly BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(ValidationResult), typeof(ErrorValidationEntry)
                , ValidationResult.None, propertyChanged: IsValidChanged);
    public ValidationResult IsValid
    {
        get => (ValidationResult)GetValue(IsValidProperty);
        set => SetValue(IsValidProperty, value);
    }
    public static readonly BindableProperty IsObligatoryProperty = BindableProperty.Create(nameof(IsObligatory), typeof(bool), typeof(ErrorValidationEntry), true, propertyChanged: IsObligatotyPropertyChanged);
    public bool IsObligatory
    {
        get => (bool)GetValue(IsObligatoryProperty);
        set => SetValue(IsObligatoryProperty, value);
    }
    static void IsObligatotyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var errorEntry = bindable as ErrorValidationEntry;
        errorEntry.lbIndicator.IsVisible = (bool)newValue;
    }
    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(ErrorValidationEntry), string.Empty, propertyChanged: PlaceHolderPropertyChanged);
    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }
    static void PlaceHolderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var errorEntry = bindable as ErrorValidationEntry;
        errorEntry.entry.Placeholder = (string)newValue;
        errorEntry.lbTitle.Text = (string)newValue;
    }
    public static readonly BindableProperty ErrorDescriptionProperty = BindableProperty.Create(nameof(ErrorDescription), typeof(string), typeof(ErrorValidationEntry), string.Empty, defaultBindingMode: BindingMode.TwoWay);
    public string ErrorDescription
    {
        get => (string)GetValue(ErrorDescriptionProperty);
        set
        {
            SetValue(ErrorDescriptionProperty, value);
        }
    }
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(ErrorValidationEntry), string.Empty, defaultBindingMode: BindingMode.TwoWay);
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set
        {
            SetValue(TextProperty, value);
        }
    }
    public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(ErrorValidationEntry), Keyboard.Default, propertyChanged: KeyboardPropertyChanged);
    public Keyboard Keyboard
    {
        get => (Keyboard)GetValue(KeyboardProperty);
        set => SetValue(KeyboardProperty, value);
    }
    static void KeyboardPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var errorEntry = bindable as ErrorValidationEntry;
        errorEntry.entry.Keyboard = (Keyboard)newValue;
    }
    public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(ErrorValidationEntry), 0, propertyChanged: MaxLengthPropertyChanged);
    public int MaxLength
    {
        get => (int)GetValue(MaxLengthProperty);
        set => SetValue(MaxLengthProperty, value);
    }
    static void MaxLengthPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var errorEntry = bindable as ErrorValidationEntry;
        errorEntry.entry.MaxLength = (int)newValue;
    }

    private static void IsValidChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is ErrorValidationEntry errorValidationEntry)
        {
            var handler = errorValidationEntry.entry.Handler as EntryHandler;
            var nativeControl = handler.PlatformView;
            var color = (ValidationResult)newValue != ValidationResult.Invalid ? Colors.Black : Colors.Red;

#if ANDROID
            nativeControl.Background.SetColorFilter(
            AndroidX.Core.Graphics.BlendModeColorFilterCompat.CreateBlendModeColorFilterCompat(color.ToPlatform(), 
            AndroidX.Core.Graphics.BlendModeCompat.SrcAtop));
#elif IOS
            nativeControl.Layer.BorderColor = color.ToPlatform().CGColor;
#endif
            errorValidationEntry.lbError.IsVisible = (ValidationResult)newValue == ValidationResult.Invalid;
        }
    }
}