using BlackSound.Entity;
using BlackSound.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound.Services
{
	class AuthenticationService
	{
		public static User LoggedUser { get; private set; }
		public static void AuthenticateUser(string email, string password)
		{
			UserRepository userRepository = new UserRepository();
            LoggedUser = userRepository.GetAll(u => u.Email == email && u.Password == password).FirstOrDefault();
		}
	}
}
