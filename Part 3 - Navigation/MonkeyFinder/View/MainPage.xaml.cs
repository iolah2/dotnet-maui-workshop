namespace MonkeyFinder.View;

public partial class MainPage : ContentPage
{
    private RakjegyzeksViewModel _viewModel;

    public MainPage(RakjegyzeksViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = _viewModel = viewModel;
        IsBusy = false;
        //await GetRakjegyzeksAsync();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.OnAppearing();
    }
}

