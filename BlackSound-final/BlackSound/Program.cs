using BlackSound.Services;
using BlackSound.View;

namespace BlackSound
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                LoginView loginView = new LoginView();
                loginView.Show();

                if (AuthenticationService.LoggedUser.IsAdmin)
                {
                    AdminView adminView = new AdminView();
                    adminView.Show();
                }
                else
                {
                    PlayListManagementView playListView = new PlayListManagementView();
                    playListView.Show();
                }
            }
        }
    }
}