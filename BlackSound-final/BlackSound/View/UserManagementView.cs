using BlackSound.Entity;
using BlackSound.Repository;
using System;

namespace BlackSound.View
{
	public class UserManagementView : BaseManagementView<UserRepository, User> 
	{
        protected override void DisplayShort(User item)
        {
            Console.Write("{0}({1})", item.Email, item.Id);
        }

        protected override void ReadFromConsole(User item)
		{
			Console.WriteLine("Add New User");

			Console.Write("Email: ");
            item.Email = Console.ReadLine();

			Console.Write("Password: ");
            item.Password = Console.ReadLine();

			Console.Write("Full Name: ");
            item.FullName = Console.ReadLine();

			Console.Write("Is Admin :  (True / False)");
			item.IsAdmin = Convert.ToBoolean(Console.ReadLine());
		}

        protected override void WriteToConsole(User item)
        {
            Console.WriteLine("Email: " + item.Email);
            Console.WriteLine("Password: " + item.Password);
            Console.WriteLine("Full Name: " + item.FullName);
            Console.WriteLine("Is Admin: " + item.IsAdmin.ToString());
        }
    }
}
