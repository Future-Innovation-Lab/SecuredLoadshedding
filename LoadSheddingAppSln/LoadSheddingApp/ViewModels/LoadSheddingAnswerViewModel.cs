using LoadSheddingApp.Models;

namespace LoadSheddingApp.ViewModels
{
	[QueryProperty(nameof(Response), "Response")]
    public class LoadsheddingAnswerViewModel : BaseViewModel
    {
		private ChatMessage _questionResponseModel;

		public ChatMessage Response
		{
			get { return _questionResponseModel; }
			set { _questionResponseModel = value;

				OnPropertyChanged();
			}
		}

		public LoadsheddingAnswerViewModel()
		{
        }
    }
}
