using BlackSound.Entity;
using BlackSound.Repository;
using BlackSound.Services;
using System;
using System.Collections.Generic;

namespace BlackSound.View
{
	public class SongManagementView : BaseManagementView<SongRepository, Song> 
	{
        private readonly PlayList playList;

        public SongManagementView(PlayList playList)
        {
            this.playList = playList;
        }

        protected override Func<Song, bool> GetFilter()
        {
            return s => s.ParentID == playList.Id;
        }

        protected override void RenderCustomMenus()
        {
            Console.WriteLine("List All Vie[w]ers " );
            Console.WriteLine("[S]hare Playlist");
            Console.WriteLine("UnS[h]are");
        }

        protected override void HandleCustomMenus(string choice, ref bool cancel)
        {
            if (choice == "S")
            {

                UserRepository repo = new UserRepository();
                List<User> items = repo.GetAll(u => true);
                foreach (User i in items)
                {
                    Console.Write("{0}({1})", i.Email, i.Id);
                    Console.Write(" ");
                }
                Console.WriteLine();

                Console.WriteLine("Share {0} playlist with", playList.Name);
                Console.WriteLine("User Id:");
                int id = Convert.ToInt32(Console.ReadLine());

                User item = repo.GetById(id);

                if (item == null)
                {
                    Console.WriteLine("Record not found");
                    Console.ReadKey(true);
                }

                playList.Shares.Add(item);
                PlayListRepository playListRepo = new PlayListRepository();
                playListRepo.Save(playList);

                Console.WriteLine("PlayList shared successfully!");
                Console.ReadKey(true);
                cancel = true;
            }

            else if (choice == "H")
            {
                Console.WriteLine("All Users who can see " + playList.Name);
                playList.Shares.ForEach(x => Console.WriteLine("{0}({1})", x.FullName, x.Id));
                Console.Write("UnShare {0} with : ", playList.Name);
                int userId = Convert.ToInt32(Console.ReadLine());

                UserRepository userRepo = new UserRepository();
                User user = playList.Shares.Find(x => x.Id == userId); //userRepo.GetById(userId);

                if (user!=null)
                {
                    playList.Shares.Remove(user);
                    PlayListRepository playListRepo = new PlayListRepository();
                    playListRepo.Save(playList);
                    Console.WriteLine(playList.Name + " Was Successfully been Unshared with  " + user.FullName);
                }
                
                Console.ReadKey(true);
                cancel = true;
            }

            else if (choice == "W")

            {
                   Console.WriteLine("Users Who Can See {0}", playList.Name);
                if (playList.Shares.Count == 0) Console.WriteLine("No Records Found");
                	playList.Shares.ForEach(x => Console.WriteLine("{0}({1})\n", x.FullName, x.Id));
                	Console.ReadKey(true);	
                    cancel = true;
            }
        }

        protected override void BeforeRenderChoice()
        {
            Console.WriteLine("Name: " + playList.Name);
            Console.WriteLine("Description: " + playList.Description);
            Console.WriteLine("Is Public: " + playList.IsPublic);
            Console.WriteLine("#########################################");

            SongRepository songRepo = new SongRepository();
            Console.WriteLine("All Songs In " + playList.Name);
            songRepo.GetAll(x => x.ParentID == playList.Id).ForEach(x => Console.Write("\t{0}((1}", x.Title, x.Id));
            Console.WriteLine("All Shares for " + playList.Name);
            playList.Shares.ForEach(x => Console.WriteLine("{0}({1})", x.FullName, x.Id));

        }

        protected override void DisplayShort(Song item)
        {
            Console.Write("{0}({1})", item.Title, item.Id);
        }

        protected override void ReadFromConsole(Song item)
		{
			Console.WriteLine("Add New Song");

            item.ParentID = playList.Id;

			Console.Write("Title: ");
            item.Title = Console.ReadLine();

			Console.Write("Artst: ");
            item.Artist = Console.ReadLine();

			Console.Write("Year:  ");
			item.Year = Convert.ToInt32(Console.ReadLine());
		}

        protected override void WriteToConsole(Song item)
        {
            Console.WriteLine("Title: " + item.Title);
            Console.WriteLine("Artist: " + item.Artist);
            Console.WriteLine("Year: " + item.Year);
        }
    }
}
