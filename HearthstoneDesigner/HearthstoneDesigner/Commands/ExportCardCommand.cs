using System;
using System.Windows.Input;
using HearthstoneDesigner.ViewModels;

namespace HearthstoneDesigner.Commands
{
	internal class ExportCardCommand : ICommand
	{
		// Initializes a new instance of the ExportCardCommand class.
		public ExportCardCommand(MainViewModel viewModel)
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
			return ViewModel.CanExport;
		}

		public void Execute(object parameter)
		{
			ViewModel.ExportCard();
		}
	}
}
