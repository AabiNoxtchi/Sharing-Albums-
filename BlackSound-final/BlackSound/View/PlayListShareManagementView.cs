using BlackSound.Entity;
using BlackSound.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound.View
{
	//public class PlayListShareManagementView
	//{
	//	private readonly PlayList playList;
	//	public PlayListShareManagementView(PlayList playList)
	//	{
	//		this.playList = playList;

	//	}

	//	public void Show()
	//	{

	//		while (true)
	//		{
	//			string choice = Choice();

	//			switch (choice)
	//			{
	//				case "A":
	//					{
	//						Add();
	//						break;
	//					}
	//				case "L":
	//					{
	//						GetAll();
	//						break;
	//					}
	//				case "D":
	//					{
	//						Delete();
	//						break;
	//					}

	//				case "X":
	//					{
	//						return;
	//					}
	//				default:
	//					{
	//						Console.WriteLine("Invalid Choice");
	//						Console.ReadKey(true);
	//						break;
	//					}


	//			}
	//		}

	//	}

	//	private string Choice()
	//	{
	//		Console.Clear();

	//		Console.WriteLine("{0} Share Management",playList.Name);
	//		Console.WriteLine();			
	//		Console.WriteLine("[A]dd");			
	//		Console.WriteLine("[D]elete Shares");
	//		Console.WriteLine("[L]ist All Viewers");
	//		Console.WriteLine("E[x]it");

	//		return (Console.ReadLine().ToUpper());
	//	}


	//	private void Add()
	//	{			
	//			Console.Clear();
	//		playList.Shares.ForEach(x => Console.WriteLine("{0}({1})", x.FullName, x.Id));

	//		Console.WriteLine("Add User To Share List  :");
				
	//				Console.Write("User Id : ");
	//				int userId = Convert.ToInt32(Console.ReadLine());
	//				User user = new User();

	//				UserRepository userRepo = new UserRepository();
	//				user = userRepo.GetById(userId);

	//				if (user != null)
	//				{
	//					playList.Shares.Add(user);
	//					PlayListRepository playListRepo = new PlayListRepository();
	//					playListRepo.Save(playList);
	//					Console.WriteLine("User Added Successfully");
	//				}
				
	//		Console.ReadKey(true);	

	//	}

	//	private void GetAll()
	//	{
	//		Console.WriteLine("Users Who Can See {0}", playList.Name);
	//		playList.Shares.ForEach(x => Console.WriteLine("{0}({1})\n", x.FullName, x.Id));
	//		Console.ReadKey(true);			
	//	}

	//	private void Delete()
	//	{
	//		Console.Clear();

	//		playList.Shares.ForEach(x => Console.WriteLine("{0}({1})", x.FullName, x.Id));
	//		Console.WriteLine("UnShare {0} with : ", playList.Name);
	//		int userId = Convert.ToInt32(Console.ReadLine());

	//		User user = new User();
	//		user = playList.Shares.Find(x => x.Id == userId);
	//		if ( user!= null)
	//		{
	//			PlayListRepository playListRepo = new PlayListRepository();
	//			playListRepo.Delete(playList);
	//			playList.Shares.Remove(user);
	//			PlayList playList2 = new PlayList();
	//			playList2.Name = playList.Name;
	//			playList2.ParentId = playList.ParentId;
	//			playList2.Description = playList.Description;
	//			playList2.IsPublic = playList.IsPublic;
	//			playList2.Shares = playList.Shares;
	//			playListRepo.Save(playList2);
	//			Console.WriteLine("User Removed Successfully");
	//		}
	//		Console.ReadKey(true);

	//	}
	//}
}
