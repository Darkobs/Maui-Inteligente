namespace MauiInteligente2022.AppBase.Behaviors;

public class ViewUnfocusBehavior : Behavior<VisualElement>
{
    protected override void OnAttachedTo(VisualElement bindable)
    {
        bindable.Unfocused += Bindable_Unfocused;
    }

    protected override void OnDetachingFrom(VisualElement bindable)
    {
        bindable.Unfocused -= Bindable_Unfocused;
    }

    private async void Bindable_Unfocused(object sender, FocusEventArgs e)
    {
        if(sender is VisualElement visualElement && visualElement.GetParentPage() is BindedPage bindedPage)
        {
            await bindedPage.UnfocusSaveAsync();
        }
    }
}

public static class ViewExtensions
{
    public static Page GetParentPage(this VisualElement visualElement)
    {
        var parent = visualElement.Parent;

        while(parent is not null)
        {
            if(parent is Page page)
            {
                return page;
            }
            parent = parent.Parent;
        }

        return null;
    }
}
