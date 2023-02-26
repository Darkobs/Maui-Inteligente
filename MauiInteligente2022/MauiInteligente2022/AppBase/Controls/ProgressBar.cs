using Microsoft.Maui.Graphics.Text;
using System.Runtime.CompilerServices;

namespace MauiInteligente2022.AppBase.Controls;

public class ProgressBar : VerticalStackLayout
{
    public static BindableProperty CurrentStepProperty = BindableProperty.Create(nameof(CurrentStep), typeof(int), typeof(ProgressBar)
               , 0);
    public int CurrentStep
    {
        get => (int)GetValue(CurrentStepProperty);
        set => SetValue(CurrentStepProperty, value);
    }
    public static BindableProperty TotalStepsProperty = BindableProperty.Create(nameof(TotalSteps), typeof(int), typeof(ProgressBar)
            , 1);
    public int TotalSteps
    {
        get => (int)GetValue(TotalStepsProperty);
        set => SetValue(TotalStepsProperty, value);
    }

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        if(propertyName.Equals(nameof(CurrentStep)) || propertyName.Equals(nameof(TotalSteps))) 
        {
            Initialize();
        }
        base.OnPropertyChanged(propertyName);
    }

    private void Initialize()
    {
        Children.Clear();

        Label messageLabel = new()
        {
            Text = Localization.Resources.ProgressBarHeader,
            Margin = new(0, 0, 0, 10),
            TextColor = Application.Current.Resources.MergedDictionaries.First()["Primary"] as Color
        };

        Grid stepsGrid = new();

        RowDefinition row = new() { Height = 30 };
        stepsGrid.RowDefinitions.Add(row);

        stepsGrid.ColumnSpacing = 5;

        double stepWidth = (double)1 / TotalSteps;

        for (int i = 0; i < TotalSteps; i++)
        {
            ColumnDefinition columnDefinition = new(new(stepWidth, GridUnitType.Star));
            stepsGrid.ColumnDefinitions.Add(columnDefinition);

            Image image = new()
            {
                Source = ImageSource.FromFile
                (
                    i == CurrentStep - 1 ? "progresscurrent"
                    : i < CurrentStep - 1 ? "progresscompleted"
                    : "progresspending"
                )
            };
            stepsGrid.Children.Add(image);
            Grid.SetRow(image, 0);
            Grid.SetColumn(image, i);
        }

        Children.Add(messageLabel);
        Children.Add(stepsGrid);
    }
}
