using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthstoneDesigner.Models
{
	public class CardType
	{
		public CardType()
		{
			Name = "";
			MinStats = 0;
			MaxStats = 0;
		}

		public CardType(string name, int minStats, int maxStats)
		{
			Name = name;
			MinStats = minStats;
			MaxStats = maxStats;
		}

		// Name of the card type.
		public string Name { get; set; }

		// Minimum stats
		public int MinStats { get; set; }

		// Maximum stats
		public int MaxStats { get; set; }
	}
}
