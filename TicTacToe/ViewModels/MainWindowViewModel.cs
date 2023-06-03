using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TicTacToe.Commands;
using TicTacToe.ViewModels.Base;

namespace TicTacToe.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public ICommand CloseApplicationCommand { get; }

        public MainWindowViewModel()
        {
            CloseApplicationCommand = new LambdaCommand
                (OnCloseApplicationCommandExecuted, CanCloseApplicationCommandEcecute);
        }
        private bool CanCloseApplicationCommandEcecute(object parameter) => true;
        private void OnCloseApplicationCommandExecuted(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
