using System;
using System.Windows.Input;
using HearthstoneDesigner.ViewModels;

namespace HearthstoneDesigner.Commands
{
	internal class SaveCardCommand : ICommand
	{
		// Initializes a new instance of the SaveCardCommand class.
		public SaveCardCommand(MainViewModel viewModel)
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
			return ViewModel.CanSave;
		}

		public void Execute(object parameter)
		{
			ViewModel.SaveCard();
		}
	}
}
