using LoadSheddingApp.Views;

namespace LoadSheddingApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute("loadsheddingquestion", typeof(LoadsheddingQuestionPage));
            Routing.RegisterRoute("loadsheddinganswer", typeof(LoadsheddingAnswerPage));
        }
    }
}
