using System;

namespace BlackSound.View
{
    public class AdminView
    {
        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Administration View:");
                Console.WriteLine("[U]ser Management");
                Console.WriteLine("[P]layList Management");
                Console.WriteLine("E[x]it");

                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "U":
                        {
                            UserManagementView userView = new UserManagementView();
                            userView.Show();
                            break;
                        }
                    case "P":
                        {
                            PlayListManagementView playListView = new PlayListManagementView();
                            playListView.Show();
                            break;
                        }
                    case "X":
                        {
                            return;
                        }

                    default:
                        {
                            Console.WriteLine("Invalid choice.");
                            Console.ReadKey(true);
                            break;
                        }
                }
            }
        }
    }
}