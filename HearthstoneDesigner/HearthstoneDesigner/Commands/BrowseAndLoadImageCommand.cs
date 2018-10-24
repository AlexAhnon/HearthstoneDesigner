using System;
using System.Windows.Input;
using HearthstoneDesigner.ViewModels;

namespace HearthstoneDesigner.Commands
{
    internal class BrowseAndLoadImageCommand : ICommand
	{
		// Initializes a new instance of the BrowseAndLoadImageCommand class.
		public BrowseAndLoadImageCommand(MainViewModel viewModel)
		{
			ViewModel = viewModel;
		}

		private MainViewModel ViewModel;

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public bool CanExecute(object parameter)
		{
			return ViewModel.CanBrowse;
		}

		public void Execute(object parameter)
		{
			ViewModel.BrowseAndLoadImage();
		}
	}
}
