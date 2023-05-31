using FontAwesome5;
using System;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private static Player _player;
        private static GameState _state;
        private static bool _isNextPlayer;
        private Button[] _buttons;
        private int[] _field;
        private int _steps = 0;
        private int _countWinsX = 0;
        private int _countWinsO = 0;

        public MainWindow()
        {
            InitializeComponent();
            AddButtonsToGrid(_buttons = CreateButtons());
            InitializeField();
        }

        private Button CreateButton(int buttonCount)
        {

            Button button = new Button
            {
                Content = string.Empty,
                Tag = buttonCount,
                Style = (Style)TryFindResource("FieldButton")
            };
            button.Click += OnButtonClick;
            return button;
        }

        private Button[] CreateButtons()
        {
            int column = UniformGridPanel.Columns;
            int row = UniformGridPanel.Rows;
            int field = column * row;
            int buttonsCount = 0;
            Button[] buttons = new Button[field];
            for (int i = 0; i < field; i++)
            {
                buttons[i] = CreateButton(buttonsCount++);
            }
            return buttons;
        }

        private void AddButtonsToGrid(Button[] buttons)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                UniformGridPanel.Children.Add(buttons[i]);
            }
        }

        private void InitializeField()
        {
            _field = new int[_buttons.Length];
            for (int i = 0; i < _buttons.Length; i++)
            {
                _field[i] = (int)Player.Empty;
            }
        }

        private Func<int> ChangePlayer = ()
            => _isNextPlayer == true ? (int)(_player = Player.Circle) : (int)(_player = Player.Cross);

        private void ChangeButtonContent(int index)
        {
            if (_field[index] == (int)Player.Cross)
            {
                _buttons[index].Content =
                    new ImageAwesome { Icon = EFontAwesomeIcon.Solid_Times };
            }
            if (_field[index] == (int)Player.Circle)
            {
                _buttons[index].Content =
                    new ImageAwesome { Icon = EFontAwesomeIcon.Regular_Circle };
            }
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button) return;
            int index = (int)button.Tag;
            _steps++;
            if (_field[index] == (int)Player.Empty)
            {
                _field[index] = ChangePlayer();
                NextPlayer.Text = "Next -> " + NextPlayerDiscription();
            }
            ChangeButtonContent(index);
            _buttons[index].IsEnabled = false;
            var player = _field[index];
            switch (player)
            {
                case 1:
                case 2:
                    CheckWinner(player);
                    break;
            }
            ShowDiscription();
            _isNextPlayer = !_isNextPlayer;
        }

        private void CheckWinner(int player, params int[] index)
        {
            if (_field[0] == player && _field[1] == player && _field[2] == player
                || _field[3] == player && _field[4] == player && _field[5] == player
                || _field[6] == player && _field[7] == player && _field[8] == player
                || _field[0] == player && _field[3] == player && _field[6] == player
                || _field[1] == player && _field[4] == player && _field[7] == player
                || _field[2] == player && _field[5] == player && _field[8] == player
                || _field[0] == player && _field[4] == player && _field[8] == player
                || _field[2] == player && _field[4] == player && _field[6] == player)
            {
                if (player == 1)
                {
                    _state = GameState.CircleWin;
                    _countWinsO++;
                }
                if (player == 2)
                {
                    _state = GameState.CrossWin;
                    _countWinsX++;
                }
                DesableButtons();
            }
            if (_steps == 9)
            {
                _state = GameState.Draw;
            }
        }

        private void DesableButtons()
        {
            for (int i = 0; i < _field.Length; i++)
            {
                if (_field[i] == (int)Player.Empty)
                {
                    _buttons[i].IsEnabled = false;
                }
            }
        }

        private void ShowDiscription()
        {
            switch (_state)
            {
                case GameState.Start:
                    TextBlock.Text = $"X Wins - {_countWinsX}\n" +
                        $"O Wins - {_countWinsO}";
                    break;
                case GameState.Draw:
                    TextBlock.Text = "DRAW";
                    break;
                case GameState.CrossWin:
                case GameState.CircleWin:
                    TextBlock.Text = $"{_player} is Winner!!!";
                    break;
            }
        }

        public string NextPlayerDiscription()
        {
            string nextPlayer;
            if (ChangePlayer() == (int)Player.Circle)
            {
                nextPlayer = Player.Cross.ToString();
            }
            else
            {
                nextPlayer = Player.Circle.ToString();
            }
            return nextPlayer;
        }

        private void RestartGame(object sender, RoutedEventArgs e)
        {
            _state = GameState.Start;
            _steps = 0;
            for (int i = 0; i < _field.Length; i++)
            {
                _field[i] = (int)Player.Empty;
                _buttons[i].Content = string.Empty;
                _buttons[i].IsEnabled = true;
            }
            ShowDiscription();
        }
    }
}

