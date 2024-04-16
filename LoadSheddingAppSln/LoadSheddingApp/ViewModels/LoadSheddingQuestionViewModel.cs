using LoadSheddingApp.Models;
using LoadSheddingApp.Services.Interfaces;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Platform;

namespace LoadSheddingApp.ViewModels
{
    public partial class LoadsheddingQuestionViewModel : BaseViewModel
    {
		private IAiAssistant _assistant;
        private ILoadSheddingDataService _dataservice;

        private ObservableCollection<ChatMessage> _chatHistory;

        public ObservableCollection<ChatMessage> ChatHistory
        {
            get { return _chatHistory; }
            set { _chatHistory = value;

                OnPropertyChanged();
            }
        }

        private string _currentQuestion;
        public string CurrentQuestion
        {
            get { return _currentQuestion; }
            set
            {
                _currentQuestion = value;

                OnPropertyChanged();
            }
        }


        //      public ICommand AskQuestionCommand { get; set; }

        public LoadsheddingQuestionViewModel(IAiAssistant assistant, ILoadSheddingDataService dataService)
		{
            _assistant = assistant;
            _dataservice = dataService;
            ChatHistory = new ObservableCollection<ChatMessage>();
            ChatHistory.Add(new ChatMessage { MessageType = Enums.ChatMessageTypeEnum.Inbound, MessageBody = "Hello, how can I help you today?" });
        }

        [RelayCommand]
        public async Task ChatSelected(ChatMessage message)
        {
             var navigationParameter = new Dictionary<string, object>
     {
         { "Response", message }
     };
             await Shell.Current.GoToAsync($"loadsheddinganswer", navigationParameter);
           
        }

        [RelayCommand]
        public async Task AskQuestion(ITextInput view, CancellationToken token)
        {
            var inboundMessages = ChatHistory.Where(x => x.MessageType == Enums.ChatMessageTypeEnum.Inbound).ToList();

            var currentChatMessage = new ChatMessage { MessageType = Enums.ChatMessageTypeEnum.Outbound, MessageBody = CurrentQuestion };

            try
            {
                var response = _assistant.GetCompletion(inboundMessages, currentChatMessage);
                ChatHistory.Add(currentChatMessage);

                var responseChatMessage = new ChatMessage { MessageType = Enums.ChatMessageTypeEnum.Inbound, MessageBody = response.Content };
                ChatHistory.Add(responseChatMessage);


                CurrentQuestion = string.Empty;
            }
            catch (Exception ex)
            {

            }
            bool isSuccessful = await view.HideKeyboardAsync(token);

        }

        public async override void OnAppearing()
        {
            base.OnAppearing();

            await _dataservice.SyncSettings();

        }
    }
}
