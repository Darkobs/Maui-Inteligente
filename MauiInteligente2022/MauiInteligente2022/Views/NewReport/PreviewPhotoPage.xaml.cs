namespace MauiInteligente2022.Views;

public partial class PreviewPhotoPage : BindedPage
{
	public PreviewPhotoPage(PreviewPhotoViewModel previewPhotoViewModel)
	{
		InitializeComponent();
		BindingContext = previewPhotoViewModel;
	}
}