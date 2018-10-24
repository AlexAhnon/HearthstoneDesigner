using System;
using System.Windows.Input;
using HearthstoneDesigner.ViewModels;

namespace HearthstoneDesigner.Commands
{
	internal class DeleteCardCommand : ICommand
	{
		// Initializes a new instance of the DeleteCardCommand class.
		public DeleteCardCommand(MainViewModel viewModel)
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
			return ViewModel.CanDelete;
		}

		public void Execute(object parameter)
		{
			ViewModel.DeleteSelected();
		}
	}
}
