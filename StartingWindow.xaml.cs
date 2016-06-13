using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace academy_project
{
    /// <summary>
    /// Interaction logic for StartingWindow.xaml
    /// </summary>
    public partial class StartingWindow : Window
    {
        private Color BallColor { get; set; }
        private Color Player1Color { get; set; }
        private Color Player2Color { get; set; }
        private StartingData StartingData { get; set; }

        public StartingWindow()
        {
            InitializeComponent();
        }

        private void Player1ColorPicker_OnSelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Player1Color = Player1ColorPicker.SelectedColor.Value;
        }

        private void Player2ColorPicker_OnSelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Player2Color = Player2ColorPicker.SelectedColor.Value;
        }

        private void BallColorPicker_OnSelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            BallColor = BallColorPicker.SelectedColor.Value;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            StartingData = new StartingData(
                new Player(1, Player1Name.Text, Player1Color),
                new Player(2,Player2Name.Text,
                    Player2Color, 
                    Constants.PlayerSize,
                    Constants.StartingPlayer2Position),
                new Ball(BallColor),
                window.Pitch);
            window.InitGame(StartingData);
            window.Show();
            this.Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Closing -= Window_Closing;
            e.Cancel = true;
            var anim = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(1));
            anim.Completed += (s, _) => this.Close();
            BeginAnimation(UIElement.OpacityProperty, anim);
        }
    }
}
