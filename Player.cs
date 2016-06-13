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
        private int Points { get; set; }
        public Player(int id)
        {
            Id = id;
            Color = new Color();
            Color = Color.FromArgb(255, 255, 20, 0);
            Position = new Point(100 - Constants.PlayerSize.Width/2, 
                (Constants.Height / 2) - (Constants.PlayerSize.Height / 2));
            Size = Constants.PlayerSize;
            ObjectEllipse = new Ellipse();
            Speed = Constants.PlayerSpeed;
            NormalizedSpeed = Speed / Constants.TimeSpan * Constants.TimeSpeedMultiplier;
        }

        public Player(int id, String name, Color color, Size size, Point position)
        {
            Id = id;
            Name = name;
            Color = color;
            Size = size;
            Position = position;
            ObjectEllipse = new Ellipse();
            Speed = Constants.PlayerSpeed;
            NormalizedSpeed = Speed / Constants.TimeSpan * Constants.TimeSpeedMultiplier;
        }

        public Player(int id, String name, Color color)
        {
            Id = id;
            Name = name;
            Color = color;
            Position = new Point(100- Constants.PlayerSize.Width/2,
                (Constants.Height / 2) - (Constants.PlayerSize.Height / 2));
            Size = Constants.PlayerSize;
            ObjectEllipse = new Ellipse();
            Speed = Constants.PlayerSpeed;
            NormalizedSpeed = Speed / Constants.TimeSpan * Constants.TimeSpeedMultiplier;
        }

        public override void Draw(Canvas pitch)
        {
            ObjectEllipse.Height = Size.Height;
            ObjectEllipse.Name = "PlayerEllipse";
            ObjectEllipse.Width = Size.Width;
            Canvas.SetLeft(ObjectEllipse, -Constants.Width+Position.X-50);
            Canvas.SetTop(ObjectEllipse, Position.Y);
            //ObjectEllipse.Margin = new Thickness(-Constants.Width + Position.X, Position.Y, 0, 0);
            SolidColorBrush mySolidColorBrush = new SolidColorBrush(Color);
            ObjectEllipse.Fill = mySolidColorBrush;
            ObjectEllipse.StrokeThickness = 1;
            ObjectEllipse.Stroke = Brushes.Black;
            pitch.Children.Add(ObjectEllipse);
        }

        public override void Move(double x, double y)
        {
            if ((Position.X + x * NormalizedSpeed < Constants.Width - 7) &&
                (Position.X + x * NormalizedSpeed > 0 - 7) &&
                (Position.Y + y * NormalizedSpeed > 0 - Constants.PlayerSize.Height / 2) &&
                (Position.Y + y * NormalizedSpeed < (Constants.Height - Constants.PlayerSize.Height / 2)))
            {
                Position = new Point(Position.X + x * NormalizedSpeed, 
                    Position.Y + y * NormalizedSpeed);
            }
            //Position.Offset(x, y);
        }
    }
}
