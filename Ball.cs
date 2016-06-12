using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace academy_project
{
    public class Ball:FieldObject
    {
        private Enum Direction { get; set; }
        private Boolean IsMoving { get; set; }
        private Double Speed { get; set; }
        private Ellipse BallEllipse { get; set; }

        public Ball()
        {
            Color = Color.FromArgb(255, 255, 255, 255);
            Size = Constants.BallSize;
            Position = Constants.StartingBallPosition;
            Direction = Directions.None;
            IsMoving = false;
            BallEllipse = new Ellipse();
            Speed = 0;
        }

        public Ball(Color color, Size size, Point position, Directions direction)
        {
            Color = color;
            Size = size;
            Position = position;
            Direction = direction;
            IsMoving = false;
            BallEllipse = new Ellipse();
            Speed = 0;
        }

        public Ball(Color color)
        {
            Color = color;
            Size = Constants.BallSize;
            Position = Constants.StartingBallPosition;
            Direction = Directions.None;
            IsMoving = false;
            BallEllipse = new Ellipse();
            Speed = 0;
        }

        public override void Draw(Canvas pitch)
        {
            BallEllipse.Height = Size.Height;
            BallEllipse.Width = Size.Width;
            BallEllipse.Name = "BallEllipse";
            Canvas.SetLeft(BallEllipse, -Constants.Width + Position.X- 50);
            Canvas.SetTop(BallEllipse, Position.Y);
            SolidColorBrush mySolidColorBrush = new SolidColorBrush(Color);
            BallEllipse.Fill = mySolidColorBrush;
            BallEllipse.StrokeThickness = 1;
            BallEllipse.Stroke = Brushes.Black;
            pitch.Children.Add(BallEllipse);
        }

        //public void DecrementSpeed(double )

        public override void Move(double x, double y)
        {
            
            Position.Offset(x, y);
        }
    }
}
