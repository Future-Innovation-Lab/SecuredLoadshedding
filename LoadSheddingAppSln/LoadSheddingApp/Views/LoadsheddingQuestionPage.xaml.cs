using LoadSheddingApp.ViewModels;

namespace LoadSheddingApp.Views;

public partial class LoadsheddingQuestionPage : ContentPage
{
    LoadsheddingQuestionViewModel _viewModel;

	public LoadsheddingQuestionPage(LoadsheddingQuestionViewModel vm)
	{
		InitializeComponent();

		_viewModel = vm;

        BindingContext = _viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _viewModel.OnAppearing();
    }
}