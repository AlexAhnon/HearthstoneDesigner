using System;
using System.Windows.Input;
using HearthstoneDesigner.ViewModels;

namespace HearthstoneDesigner.Commands
{
    internal class CreateNewTypeCommand : ICommand
    {
		// Initializes a new instance of the CreateNewTypeCommand class.
		public CreateNewTypeCommand(MainViewModel viewModel)
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
			return ViewModel.CanCreate;
		}

		public void Execute(object parameter)
		{
			ViewModel.CreateNewType();
		}
	}
}
