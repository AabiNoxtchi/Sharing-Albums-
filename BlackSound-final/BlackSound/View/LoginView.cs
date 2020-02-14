using BlackSound.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound.View
{
	class LoginView
	{
		public void Show()
		{

			while (true)
			{
				Console.Clear();

				Console.Write("Email: ");
				string email = Console.ReadLine();

				Console.Write("Password: ");
				string password = Console.ReadLine();

				AuthenticationService.AuthenticateUser(email, password);

				if (AuthenticationService.LoggedUser != null)
				{
					Console.WriteLine("Welcome " + AuthenticationService.LoggedUser.FullName);
					Console.ReadKey(true);
					break;
				}
				else
				{
					Console.WriteLine("Invalid email or password");
					Console.ReadKey(true);
				}

			}
		}
	}
}
