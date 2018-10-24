using Microsoft.Win32;
using System;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

using HearthstoneDesigner.Commands;
using HearthstoneDesigner.Models;

namespace HearthstoneDesigner.ViewModels
{
	class MainViewModel : INotifyPropertyChanged
	{
		// Variables for connecting to the SQL database.
		static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CardDatabase;";
		SqlConnection con;
		SqlCommand cmd;
		SqlDataAdapter adapter;
		DataSet ds;

		// Initializes a new instance of the CardViewModel class.
		public MainViewModel()
		{
			// Initialize CardTypeSource and add some basic types.
			ObservableCollection<CardType> temp = new ObservableCollection<CardType>();
			temp.Add(new CardType("Minion", 0, 24));
			temp.Add(new CardType("Spell", 0, 0));
			CardTypeSource = temp;

			// Populate data grid.
			PopulateGrid();

			// Initialize some empty objects.
			_card = new Card();
			_cardType = new CardType();

			// Initialize commands.
			SaveCardCommand = new SaveCardCommand(this);
			DeleteCardCommand = new DeleteCardCommand(this);
			ViewCardCommand = new ViewCardCommand(this);
			BrowseAndLoadImageCommand = new BrowseAndLoadImageCommand(this);
			CreateNewTypeCommand = new CreateNewTypeCommand(this);
			ExportCardCommand = new ExportCardCommand(this);
			ImportCardCommand = new ImportCardCommand(this);
		}

		// Since we use strings to identify the card type, we need a get function to find it.
		public CardType GetCardType(string name)
		{
			// Returns card type based on name.
			foreach (CardType ct in CardTypeSource) {
				if (ct.Name == name)
				{
					return ct;
				}
			}

			// Returns the first index as default value (null would return errors).
			return CardTypeSource[0];
		}

		// Mana rules for the card. Also checks for restrictions if restrictions is enabled.
		public bool CheckCardValues()
		{
			// If combined stats goes above or below the card's type stat values, return false.
			if ((Card.Attack + Card.Health) > Card.CardType.MaxStats)
			{
				MessageBox.Show("Combined stat values goes beyond the type's maximum stats.");
				return false;
			} else if ((Card.Attack + Card.Health) < Card.CardType.MinStats)
			{
				MessageBox.Show("Combined stat values goes below the type's minimum stats.");
				return false;
			}

			// Integrated rules to check for combined stat value depending on mana value.
			// Could use a Case to check through everything, might be bad practice to use if-statements.
			if (Card.Mana == 0 && (Card.Attack + Card.Health) > 2)
			{
				MessageBox.Show("Combined stat values goes above what the mana value allows.\nMax stat value of 2 for 0 mana!");
				return false;
			} else if (Card.Mana == 1 && (Card.Attack + Card.Health) > 3)
			{
				MessageBox.Show("Combined stat values goes above what the mana value allows.\nMax stat value of 3 for 1 mana!");
				return false;
			}
			else if (Card.Mana == 2 && (Card.Attack + Card.Health) > 5)
			{
				MessageBox.Show("Combined stat values goes above what the mana value allows.\nMax stat value of 5 for 2 mana!");
				return false;
			}
			else if (Card.Mana == 3 && (Card.Attack + Card.Health) > 7)
			{
				MessageBox.Show("Combined stat values goes above what the mana value allows.\nMax stat value of 7 for 3 mana!");
				return false;
			}
			else if (Card.Mana == 4 && (Card.Attack + Card.Health) > 9)
			{
				MessageBox.Show("Combined stat values goes above what the mana value allows.\nMax stat value of 9 for 4 mana!");
				return false;
			}
			else if (Card.Mana == 5 && (Card.Attack + Card.Health) > 11)
			{
				MessageBox.Show("Combined stat values goes above what the mana value allows.\nMax stat value of 11 for 5 mana!");
				return false;
			}
			else if (Card.Mana == 6 && (Card.Attack + Card.Health) > 13)
			{
				MessageBox.Show("Combined stat values goes above what the mana value allows.\nMax stat value of 13 for 6 mana!");
				return false;
			}
			else if (Card.Mana == 7 && (Card.Attack + Card.Health) > 15)
			{
				MessageBox.Show("Combined stat values goes above what the mana value allows.\nMax stat value of 15 for 7 mana!");
				return false;
			}
			else if (Card.Mana == 8 && (Card.Attack + Card.Health) > 17)
			{
				MessageBox.Show("Combined stat values goes above what the mana value allows.\nMax stat value of 17 for 8 mana!");
				return false;
			}
			else if (Card.Mana == 9 && (Card.Attack + Card.Health) > 19)
			{
				MessageBox.Show("Combined stat values goes above what the mana value allows.\nMax stat value of 19 for 9 mana!");
				return false;
			}
			else if (Card.Mana == 10 && (Card.Attack + Card.Health) > 24)
			{
				MessageBox.Show("Combined stat values goes above what the mana value allows.\nMax stat value of 24 for 10 mana!");
				return false;
			}

			return true;
		}

		private Card _card;

		// Gets the card instance.
		public Card Card
		{
			get
			{
				return _card;
			}

			set
			{
				_card = value;
				RaisePropertyChanged("Card");
			}
		}

		private Card _selectedCard;

		// Gets the card instance.
		public Card SelectedCard
		{
			get
			{
				return _selectedCard;
			}

			set
			{
				_selectedCard = value;
				RaisePropertyChanged("SelectedCard");
			}
		}

		private string _imagePath;

		// Sets the image path. When the image path is set, also set the current card's image path to it.
		public string ImagePath
		{
			get
			{
				return _imagePath;
			}

			set
			{
				_imagePath = value;
				Card.ImagePath = value;
				RaisePropertyChanged("ImagePath");
			}
		}

		private CardType _cardType;

		public CardType CardType
		{
			get
			{
				return _cardType;
			}

			set
			{
				_cardType = value;
				RaisePropertyChanged("CardType");
			}
		}

		private ObservableCollection<Card> _cardSource;

		public ObservableCollection<Card> CardSource
		{
			get { return _cardSource; }
			set
			{
				_cardSource = value;
				RaisePropertyChanged("CardSource");
			}
		}

		// Populates _cardSource with the cards from the database.
		public void PopulateGrid()
		{
			try
			{
				con = new SqlConnection(connectionString);
				con.Open();
				cmd = new SqlCommand("select * from CardTable", con);
				adapter = new SqlDataAdapter(cmd);
				ds = new DataSet();
				adapter.Fill(ds, "CardTable");

				if (_cardSource == null)
					_cardSource = new ObservableCollection<Card>();

				// Converts the data from the database into working data for the card objects.
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					Card temp = new Card();

					temp.Name = dr[0].ToString();
					temp.CardType = GetCardType(dr[1].ToString());
					temp.Mana = Convert.ToInt32(dr[2].ToString());
					temp.Rarity = (CardRarity)Convert.ToInt32(dr[3].ToString());
					temp.Attack = Convert.ToInt32(dr[4].ToString());
					temp.Health = Convert.ToInt32(dr[5].ToString());
					temp.Text = dr[6].ToString();

					if (Convert.ToInt32(dr[7].ToString()) == 0)
					{
						temp.IsRestricted = false;
					}
					else if (Convert.ToInt32(dr[7].ToString()) == 1)
					{
						temp.IsRestricted = true;
					}

					temp.ImagePath = dr[8].ToString();

					_cardSource.Add(temp);
				}
			} finally
			{
				// Close the connection.
				ds = null;
				adapter.Dispose();
				con.Close();
				con.Dispose();
			}
		}

		private ObservableCollection<CardType> _cardTypeSource;

		public ObservableCollection<CardType> CardTypeSource
		{
			get { return _cardTypeSource; }
			set
			{
				_cardTypeSource = value;
				RaisePropertyChanged("CardTypeSource");
			}
		}

		private IEnumerable<CardRarity> _cardRaritySource;

		// Passes the CardRarity enum to the ComboBox, showing all the different options.
		public IEnumerable<CardRarity> CardRaritySource
		{
			get
			{
				return Enum.GetValues(typeof(CardRarity)).Cast<CardRarity>();
			}

			set
			{
				_cardRaritySource = value;
				RaisePropertyChanged("CardRaritySources");
			}
		}

		// Gets or sets a boolean value indicating whetever the card can be saved in the database.
		// The save button fades out if it returns false, visible otherwise.
		public bool CanSave
		{
			get
			{
				if (Card == null)
				{
					return false;
				}
				// If namefield is null, card type is null or costs are set to below 0, disable the button.
				// Check for image path can easily be added but was left out for ease of testing.
				else if (String.IsNullOrWhiteSpace(Card.Name) || Card.CardType == null || Card.Mana < 0 || Card.Attack < 0 || Card.Health < 0)
				{
					return false;
				}

				return true;
			}
		}

		// Gets or sets the boolean value for being able to delete a card from data grid.
		public bool CanDelete
		{
			get
			{
				if (SelectedCard == null)
				{
					return false;
				}

				return true;
			}
		}

		// Gets or sets the boolean value for being able to view a card from the data grid.
		public bool CanView
		{
			get { return true; }
		}

		// Gets or set the boolean value for being able to browse for an image.
		public bool CanBrowse
		{
			get { return true; }
		}

		// Gets or set the boolean value for being able to create a new type.
		public bool CanCreate
		{
			get { return true; }
		}

		// Gets or set the boolean value for being able to export a card.
		public bool CanExport
		{
			get { return true; }
		}

		// Gets or set the boolean value for being able to import a card.
		public bool CanImport
		{
			get { return true; }
		}

		// Gets the SaveCommand for the ViewModel.
		public ICommand SaveCardCommand
		{
			get;
			private set;
		}

		// Gets the DeleteCardCommand for the ViewModel.
		public ICommand DeleteCardCommand
		{
			get;
			private set;
		}

		public ICommand ViewCardCommand
		{
			get;
			private set;
		}

		// Gets or sets the load image command.
		public ICommand BrowseAndLoadImageCommand
		{
			get;
			private set;
		}

		// Gets or sets the create new type command.
		public ICommand CreateNewTypeCommand
		{
			get;
			private set;
		}

		// Gets or sets the export card command.
		public ICommand ExportCardCommand
		{
			get;
			private set;
		}

		// Gets or sets the import card command.
		public ICommand ImportCardCommand
		{
			get;
			private set;
		}

		// Saves the card into the database.
		public void SaveCard()
		{
			// Check the values on the card if restriction is enabled.
			if (Card.IsRestricted == true)
			{
				if (CheckCardValues() == false)
				{
					return;
				}
			}

			// Converting bool value to int
			int i;
			if (Card.IsRestricted == false)
			{
				i = 0;
			}
			else
			{
				i = 1;
			}

			// Set connection.
			con = new SqlConnection(connectionString);

			// Iterate to check cards in database.
			foreach (Card c in CardSource)
			{
				// If card already exists, update database entry and reset selection.
				// BUG: If you view a card and change the name, saving does not save to the database. FIX: Add an id entry for all cards to make them unique.
				if (c.Name == Card.Name)
				{
					cmd = new SqlCommand(String.Format("UPDATE CardTable " +
						"SET CardType='{0}', Mana={1}, Rarity={2}, Attack={3}, Health={4}, Text='{5}', IsRestricted={6}, ImagePath='{7}' " +
						"WHERE Name='{8}'",
						Card.CardType.Name, Card.Mana, (int)Card.Rarity, Card.Attack, Card.Health, Card.Text, i, Card.ImagePath, Card.Name), con);

					// Open connection, run query and close it.
					con.Open();
					cmd.ExecuteNonQuery();
					con.Close();

					Card = new Card();
					ImagePath = "";
					return;
				}
			}

			// If it does not exist, add as a new entry to database.
			cmd = new SqlCommand(String.Format("INSERT INTO CardTable " +
				"VALUES ('{0}', '{1}', {2}, {3}, {4}, {5}, '{6}', {7}, '{8}')",
				Card.Name, Card.CardType.Name, Card.Mana, (int)Card.Rarity, Card.Attack, Card.Health, Card.Text, i, Card.ImagePath), con);
			con.Open();
			cmd.ExecuteNonQuery();
			con.Close();

			// Then add to visible collection and reset.
			CardSource.Add(Card);
			Card = new Card();
			ImagePath = "";
		}

		// Deletes the currently selected card.
		public void DeleteSelected()
		{
			// Delete based by primary key which is the name.
			con = new SqlConnection(connectionString);
			cmd = new SqlCommand(String.Format("DELETE FROM CardTable WHERE Name='{0}';", SelectedCard.Name), con);
			con.Open();
			cmd.ExecuteNonQuery();
			con.Close();

			// Remove from visible collection and reset.
			CardSource.Remove(SelectedCard);
			Card = new Card();
			ImagePath = "";
		}

		// View the values of the currently selected card.
		public void ViewCard()
		{
			Card = SelectedCard;
			ImagePath = Card.ImagePath;
		}

		// Loads the image path.
		public void BrowseAndLoadImage()
		{
			OpenFileDialog open = new OpenFileDialog();
			open.DefaultExt = (".png");
			open.Filter = "Pictures (*.jpg;*.bmp;*.png)|*.jpg;*.bmp;*.png";

			if (open.ShowDialog() == true)
				ImagePath = open.FileName;
		}

		// Creates a new card type. Checks for the name field, displays boxes for confirmation.
		public void CreateNewType()
		{
			if (CardType == null || String.IsNullOrWhiteSpace(CardType.Name))
			{
				MessageBox.Show("Fields can't be empty!");
				return;
			}

			// Check to see if object already exists.
			foreach (CardType ct in CardTypeSource)
			{
				if (ct.Name == CardType.Name)
				{
					MessageBox.Show("This type already exists!");
					return;
				}
			}

			CardTypeSource.Add(new CardType(CardType.Name, CardType.MinStats, CardType.MaxStats));
			MessageBox.Show(string.Format("{0} was successfully created.", CardType.Name));
		}

		// Exports the currently made card to a .json file.
		public void ExportCard()
		{
			using (StreamWriter file = File.CreateText(String.Format("{0}.json", Card.Name)))
			{
				JsonSerializer serializer = new JsonSerializer();
				serializer.Serialize(file, Card);
			}
		}

		// Opens a file dialog and imports a .json card file.
		public void ImportCard()
		{
			OpenFileDialog open = new OpenFileDialog();
			open.DefaultExt = (".json");
			open.Filter = "JSON object (*.json)|*.json";

			if (open.ShowDialog() == true)
			{
				ImagePath = open.FileName;

				using (StreamReader file = File.OpenText(open.FileName))
				{
					JsonSerializer serializer = new JsonSerializer();
					Card = (Card)serializer.Deserialize(file, typeof(Card));
				}
			}

		}

		// INPC handling.
		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
