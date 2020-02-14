using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound.Entity
{
    public class PlayList : BaseEntity
    {
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }

        public List<User> Shares { get; set; }
        
        public PlayList()
        {
            Shares = new List<User>();
        }
    }
}
