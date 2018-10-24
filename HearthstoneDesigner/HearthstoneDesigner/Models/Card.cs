using System;

namespace HearthstoneDesigner.Models
{
	// Cards have different rarities. Since it is static, we just use enums.
	public enum CardRarity
	{
		Common,
		Uncommon,
		Rare,
		Epic,
		Legendary
	}

	public class Card
	{
		public Card()
		{
			Name = "";
			CardType = null;
			Mana = 0;
			Rarity = CardRarity.Common;
			Attack = 0;
			Health = 0;
			Text = "";
			IsRestricted = false;
			ImagePath = "";
		}

		public Card(string cardName, CardType cardType, int cardMana, CardRarity rare, int cardAttack, int cardHealth, string cardText, bool restricted, string image)
		{
			Name = cardName;
			CardType = cardType;
			Mana = cardMana;
			Rarity = rare;
			Attack = cardAttack;
			Health = cardHealth;
			Text = cardText;
			IsRestricted = restricted;
			ImagePath = image;
		}

		// Gets or sets card name.
		public string Name { get; set; }

		// Gets or sets the card type.
		public CardType CardType { get; set; }

		// Gets or sets card mana value.
		public int Mana { get; set; }

		public CardRarity Rarity { get; set; }

		// Gets or sets card attack value.
		public int Attack { get; set; }

		// Gets or sets card health value.
		public int Health { get; set; }

		// Gets or sets card text.
		public string Text { get; set; }

		// Gets or sets if this specific card has rules and restrictions applied to it.
		public bool IsRestricted { get; set; }

		// Gets or sets the image path.
		public string ImagePath { get; set; }
	}
}
