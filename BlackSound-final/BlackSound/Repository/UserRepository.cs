using BlackSound.Entity;
using System.Collections.Generic;

namespace BlackSound.Repository
{
	public class UserRepository : BaseRepository<User>
	{
        protected override void BeforeDelete(User item,bool all)
        {
            PlayListRepository playListRepo = new PlayListRepository();

            List<PlayList> playLists = playListRepo.GetAll(p => p.ParentId == item.Id);
            foreach(PlayList playList in playLists)
            {
                playListRepo.Delete(playList);
            }
        }
    }
}
