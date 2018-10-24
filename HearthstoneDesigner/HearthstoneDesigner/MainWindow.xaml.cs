using System.Windows;
using HearthstoneDesigner.ViewModels;

namespace HearthstoneDesigner
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		MainViewModel vwm;

		public MainWindow()
		{
			InitializeComponent();
			vwm = new MainViewModel();
			DataContext = vwm;
		}

		// I believe this invalidates the MVVM pattern, but because I couldn't figure any other solution I resorted to using a simple click button function.
		private void typeNewButton_Click(object sender, RoutedEventArgs e)
		{
			TypeWindow win = new TypeWindow();
			win.Show();
			win.DataContext = vwm;
		}
	}
}
