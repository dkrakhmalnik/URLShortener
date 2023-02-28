namespace URLShortener;

using URLShortener.ViewModels;

public partial class MainPage : ContentPage
{
	private MainPageViewModel _viewModel;

	public MainPage(MainPageViewModel viewModel)
	{		
		 BindingContext = _viewModel = viewModel;
		InitializeComponent();
	}

	protected override async void OnAppearing()
	{
		await _viewModel.Init();
	}
}

