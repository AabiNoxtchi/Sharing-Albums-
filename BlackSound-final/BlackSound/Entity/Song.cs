using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound.Entity
{
	public class Song : BaseEntity
	{
		public int ParentID { get; set; }
		public string Title { get; set; }
		public string Artist { get; set; }
		public int Year { get; set; }
	}
}
