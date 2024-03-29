﻿
using MauiInteligente2022.AppBase.Helpers;
using MauiInteligente2022.AppBase.Validations;

namespace MauiInteligente2022.AppBase.Behaviors;

public class EntryValidationBehavior : Behavior<Entry>
{
    static readonly BindablePropertyKey IsValidPropertyKey =
        BindableProperty.CreateReadOnly(nameof(IsValid), typeof(ValidationResult), typeof(EntryValidationBehavior),
            ValidationResult.None);

    public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

    public ValidationResult IsValid
    {
        get => (ValidationResult)GetValue(IsValidProperty);
        private set => SetValue(IsValidPropertyKey, value);
    }   

    static readonly BindableProperty ValidationTypeProperty =
        BindableProperty.Create(nameof(ValidationType), typeof(ValidationType), typeof(EntryValidationBehavior),
            ValidationType.None, defaultBindingMode: BindingMode.OneWay);

    public ValidationType ValidationType
    {
        get => (ValidationType)GetValue(ValidationTypeProperty);
        set => SetValue(ValidationTypeProperty, value);
    }

    protected override void OnAttachedTo(Entry bindable)
    {
        bindable.TextChanged += Bindable_TextChanged;
        bindable.BindingContextChanged += Bindable_BindingContextChanged;
    }

    private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = sender as Entry;

        if(entry.Text is not null)
        { 
            IsValid = ValidationsHelper.ValidateString(ValidationType, entry.Text);
            entry.TextColor = IsValid == ValidationResult.Valid ? Colors.Black : Colors.Red;
        }
        else
        {
            IsValid = ValidationResult.None;
        } 
    }

    private void Bindable_BindingContextChanged(object sender, EventArgs e)
    {
        var entry = sender as Entry;
        BindingContext = entry.BindingContext;
    }

    protected override void OnDetachingFrom(Entry bindable)
    {
        bindable.TextChanged -= Bindable_TextChanged;
        bindable.BindingContextChanged -= Bindable_BindingContextChanged;
    }
}
