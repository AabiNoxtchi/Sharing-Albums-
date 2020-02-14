using BlackSound.Entity;
using BlackSound.Repository;
using BlackSound.Services;
using System;
using System.Collections.Generic;

namespace BlackSound.View
{
    public class PlayListManagementView : BaseManagementView<PlayListRepository, PlayList>
    {
        protected override Func<PlayList, bool> GetFilter()
        {
            return p => p.ParentId == AuthenticationService.LoggedUser.Id || p.IsPublic == true || p.Shares.FindAll(u => u.Id == AuthenticationService.LoggedUser.Id).Count > 0;
        }

        protected override void BeforeView(PlayList item, ref bool cancel)
        {
            cancel = true;

            if (item.ParentId != AuthenticationService.LoggedUser.Id && (item.IsPublic == true || item.Shares.FindAll(u => u.Id == AuthenticationService.LoggedUser.Id).Count > 0))
            {
                SongRepository repo = new SongRepository();
                foreach (Song song in repo.GetAll(x => x.ParentID == item.Id))
                {
                    Console.WriteLine("Title: " + song.Title);
                    Console.WriteLine("Artist: " + song.Artist);
                    Console.WriteLine("Year: " + song.Year);
                    Console.WriteLine("###############################");
                }
            }

            if (item.ParentId == AuthenticationService.LoggedUser.Id)
            {
                SongManagementView songManagementView = new SongManagementView(item);
                songManagementView.Show();

                return;
            }

            Console.ReadKey(true);
        }

        protected override void DisplayShort(PlayList item)
        {
            Console.Write("{0}({1})", item.Name, item.Id);
        }

        protected override void ReadFromConsole(PlayList item)
        {
            Console.WriteLine("Add New PlayList");

            item.ParentId = AuthenticationService.LoggedUser.Id;

            Console.Write("Name: ");
            item.Name = Console.ReadLine();

            Console.Write("Description: ");
            item.Description = Console.ReadLine();

            Console.Write("Is Public (True / False):  ");
            item.IsPublic = Convert.ToBoolean(Console.ReadLine());
        }

        protected override void WriteToConsole(PlayList item)
        {
            Console.WriteLine("Name: " + item.Name);
            Console.WriteLine("Description: " + item.Description);
            Console.WriteLine("Is Public: " + item.IsPublic);
        }

        protected override void DisplayGetAllDetails(PlayList item)
        {
            if (item.ParentId != AuthenticationService.LoggedUser.Id && (item.IsPublic == true || item.Shares.FindAll(u => u.Id == AuthenticationService.LoggedUser.Id).Count > 0))
            {
                SongRepository repo = new SongRepository();
                foreach (Song song in repo.GetAll(x => x.ParentID == item.Id))
                {
                    Console.Write("\tTitle: " + song.Title);
                }                
            }
            if (item.ParentId == AuthenticationService.LoggedUser.Id)
            {
                SongRepository songRepo = new SongRepository();
                Console.WriteLine("All Songs In " + item.Name);
                songRepo.GetAll(x => x.ParentID == item.Id).ForEach(x => Console.Write("\t{0}((1}", x.Title, x.Id));
                Console.WriteLine("All Shares for " + item.Name);
                item.Shares.ForEach(x => Console.WriteLine("{0}({1})", x.FullName, x.Id));

                return;
            }

            //1
            //protected override void RenderCustomMenus()
            //{
            //	Console.WriteLine("[S]hare View");

            //}

            //2
            //protected override void HandleCustomMenus(string choice, ref bool cancel)
            //{
            //	if (choice == "S" )
            //	{

            //		cancel = true;

            //		PlayListRepository repo = new PlayListRepository();
            //		List<PlayList> items = repo.GetAll(x => x.ParentId == AuthenticationService.LoggedUser.Id);
            //		foreach (PlayList i in items)
            //		{
            //			DisplayShort(i);
            //			Console.Write(" ");
            //		}
            //		Console.WriteLine();

            //		Console.Write("Choose Which PlayList To Share/Unshare\n Id:");
            //		int id = Convert.ToInt32(Console.ReadLine());

            //		PlayList item = items.Find(x => x.Id == id);

            //		if (item == null)
            //		{
            //			Console.WriteLine("Record not found");
            //			Console.ReadKey(true);
            //		}
            //		else
            //		{
            //			PlayListShareManagementView playListShareView = new PlayListShareManagementView(item);
            //			playListShareView.Show();
            //		}

        }

    }
}

		

		

	
