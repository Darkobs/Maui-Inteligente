using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace MauiInteligente2022.AppBase.Controls;

[ContentProperty(nameof(LayoutContent))]
public abstract class BindedPage : ContentPage
{
	public BindedPage()
	{
		On<iOS>().SetUseSafeArea(true);
	}

	public static readonly BindableProperty LayoutContentProperty =
		BindableProperty.Create(nameof(LayoutContent), typeof(View), typeof(BindedPage)
			, propertyChanged: OnContentChanged);

	public View LayoutContent
	{
		get => (View)GetValue(LayoutContentProperty);

		set => SetValue(LayoutContentProperty, value);
	}

	private static void OnContentChanged(BindableObject bindableObject, object oldValue, object newValue)
	{
		if(bindableObject is BindedPage bindedPage)
		{
			Resources.ControlsResources controlResources= new();

			var template = controlResources["MasterPageTemplate"] as ControlTemplate;

			var contentView = new ContentView
			{
				ControlTemplate = template, 
				Content = newValue as View
			};

			bindedPage.Content = contentView;
		}
	}

	public static readonly BindableProperty SubTitleProperty
		= BindableProperty.Create(nameof(SubTitle), typeof(string), typeof(BindedPage));

	public string SubTitle
	{
		get => GetValue(SubTitleProperty)?.ToString();
		set => SetValue(SubTitleProperty, value);
	}

    public static readonly BindableProperty PageIdProperty
    = BindableProperty.Create(nameof(PageId), typeof(string), typeof(BindedPage));

    public string PageId
    {
        get => GetValue(PageIdProperty)?.ToString();
        set => SetValue(PageIdProperty, value);
    }

    protected override void OnBindingContextChanged()
    {
		base.OnBindingContextChanged();

		Binding titleBinding = new("Title");
		SetBinding(TitleProperty, titleBinding);

		Binding subTitleBinding = new("SubTitle");
		SetBinding(SubTitleProperty, subTitleBinding);

		Binding pageIdBinding = new("PageId");
		SetBinding(PageIdProperty, pageIdBinding);

		Binding isBusyBinding = new("IsBusy");
		SetBinding(IsBusyProperty, isBusyBinding);
    }

    protected override void OnAppearing()
    {
        if(BindingContext is BaseViewModel baseViewModel)
            baseViewModel.OnAppearing();
    }

    protected override void OnDisappearing()
    {
		if(BindingContext is BaseViewModel baseViewModel)
            baseViewModel.OnDisappearing();
    }
}
