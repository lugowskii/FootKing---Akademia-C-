using System;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace academy_project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game _game;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void InitGame(StartingData data)
        {
            _game = new Game(data.Pitch);
            Player1Name.Content = data.Player1.Name;
            Player1Points.Content = 0;
            Player2Name.Content = data.Player2.Name;
            Player2Points.Content = 0;
            _game.AddObjectToGame(data.Player1);
            _game.AddObjectToGame(data.Player2);
            _game.AddObjectToGame(data.Ball);
            _game.DrawObjects();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(Update);
            timer.Interval = TimeSpan.FromMilliseconds(Constants.TimeSpan);
            timer.Start();
        }

        private void Update(object sender, EventArgs e)
        {
            if (_game.IsAnyKeyPressed())
            {
                _game.ProcessKeys();
            }
            _game.MoveBall();
            _game.DrawObjects();
                
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            _game.AddKeyToList(e.Key);
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            _game.RemoveKeyFromList(e.Key);
        }

        private void MainWindow_OnActivated(object sender, EventArgs e)
        {
            DoubleAnimation animFadeIn = new DoubleAnimation();
            animFadeIn.From = 0;
            animFadeIn.To = 1;
            animFadeIn.Duration = new Duration(TimeSpan.FromSeconds(1));
            BeginAnimation(Window.OpacityProperty, animFadeIn);
        }
    }
}
