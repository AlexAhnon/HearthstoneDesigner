using System;
using System.Windows.Input;
using HearthstoneDesigner.ViewModels;

namespace HearthstoneDesigner.Commands
{
	internal class ImportCardCommand : ICommand
	{
		// Initializes a new instance of the ImportCardCommand class.
		public ImportCardCommand(MainViewModel viewModel)
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
			return ViewModel.CanImport;
		}

		public void Execute(object parameter)
		{
			ViewModel.ImportCard();
		}
	}
}
