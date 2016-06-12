using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shapes;

namespace academy_project
{
    public class Player :FieldObject
    {
        public int Id { get; set; }
        public String Name { get; set; }
        private double Speed { get; set; }
        private int Points { get; set; }
        private Ellipse PlayerEllipse { get; set; }
        public Player(int id)
        {
            Id = id;
            Color = new Color();
            Color = Color.FromArgb(255, 255, 20, 0);
            Position = new Point(100, 
                (Constants.Height / 2) - (Constants.PlayerSize.Height / 2));
            Size = Constants.PlayerSize;
            PlayerEllipse = new Ellipse();
            Speed = Constants.PlayerSpeed;
        }

        public Player(int id, String name, Color color, Size size, Point position)
        {
            Id = id;
            Name = name;
            Color = color;
            Size = size;
            Position = position;
            PlayerEllipse = new Ellipse();
            Speed = Constants.PlayerSpeed;
        }

        public Player(int id, String name, Color color)
        {
            Id = id;
            Name = name;
            Color = color;
            Position = new Point(100,
                (Constants.Height / 2) - (Constants.PlayerSize.Height / 2));
            Size = Constants.PlayerSize;
            PlayerEllipse = new Ellipse();
            Speed = Constants.PlayerSpeed;
        }

        public override void Draw(Canvas pitch)
        {
            PlayerEllipse.Height = Size.Height;
            PlayerEllipse.Name = "PlayerEllipse";
            PlayerEllipse.Width = Size.Width;
            Canvas.SetLeft(PlayerEllipse, -Constants.Width+Position.X-50);
            Canvas.SetTop(PlayerEllipse, Position.Y);
            //PlayerEllipse.Margin = new Thickness(-Constants.Width + Position.X, Position.Y, 0, 0);
            SolidColorBrush mySolidColorBrush = new SolidColorBrush(Color);
            PlayerEllipse.Fill = mySolidColorBrush;
            PlayerEllipse.StrokeThickness = 1;
            PlayerEllipse.Stroke = Brushes.Black;
            pitch.Children.Add(PlayerEllipse);
        }

        public override void Move(double x, double y)
        {
            double normalizedSpeed = Speed*Constants.TimeSpan/30;
            if ((Position.X + x * normalizedSpeed < Constants.Width - 7) &&
                (Position.X + x * normalizedSpeed > 0 - 7) &&
                (Position.Y + y * normalizedSpeed > 0 - Constants.PlayerSize.Height / 2) &&
                (Position.Y + y * normalizedSpeed < (Constants.Height - Constants.PlayerSize.Height / 2)))
            {
                Position = new Point(Position.X + x * normalizedSpeed, 
                    Position.Y + y * normalizedSpeed);
            }
            //Position.Offset(x, y);
        }
    }
}
