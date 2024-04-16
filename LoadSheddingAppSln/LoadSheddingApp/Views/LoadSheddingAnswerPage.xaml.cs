using LoadSheddingApp.ViewModels;

namespace LoadSheddingApp.Views;

public partial class LoadsheddingAnswerPage : ContentPage
{
    LoadsheddingAnswerViewModel _viewModel;

    public LoadsheddingAnswerPage(LoadsheddingAnswerViewModel vm)
	{
        _viewModel = vm;

        InitializeComponent();

        BindingContext = _viewModel;
	}
}