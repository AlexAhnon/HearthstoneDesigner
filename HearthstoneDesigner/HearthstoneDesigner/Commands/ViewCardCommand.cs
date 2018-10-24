using System;
using System.Windows.Input;
using HearthstoneDesigner.ViewModels;

namespace HearthstoneDesigner.Commands
{
    internal class ViewCardCommand : ICommand
    {
		// Initializes a new instance of the ViewCardCommand class.
		public ViewCardCommand(MainViewModel viewModel)
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
			return ViewModel.CanView;
		}

		public void Execute(object parameter)
		{
			ViewModel.ViewCard();
		}
	}
}
