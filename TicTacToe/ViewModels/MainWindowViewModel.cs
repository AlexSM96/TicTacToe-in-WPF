using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TicTacToe.Commands;
using TicTacToe.Model;
using TicTacToe.ViewModels.Base;
using static TicTacToe.Model.GameModel;

namespace TicTacToe.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public ICommand CloseApplicationCommand { get; }
        public ICommand RestartGameCommand { get; }


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
