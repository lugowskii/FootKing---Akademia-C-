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

        public Ball()
        {
            Color = Color.FromArgb(255, 255, 255, 255);
            Size = Constants.BallSize;
            Position = Constants.StartingBallPosition;
            Direction = Directions.None;
            IsMoving = false;
            ObjectEllipse = new Ellipse();
            Speed = 0;
        }

        public Ball(Color color, Size size, Point position, Directions direction)
        {
            Color = color;
            Size = size;
            Position = position;
            Direction = direction;
            IsMoving = false;
            ObjectEllipse = new Ellipse();
            Speed = 0;
        }

        public Ball(Color color)
        {
            Color = color;
            Size = Constants.BallSize;
            Position = Constants.StartingBallPosition;
            Direction = Directions.None;
            IsMoving = false;
            ObjectEllipse = new Ellipse();
            Speed = 0;
        }

        public override void Draw(Canvas pitch)
        {
            ObjectEllipse.Height = Size.Height;
            ObjectEllipse.Width = Size.Width;
            ObjectEllipse.Name = "BallEllipse";
            Canvas.SetLeft(ObjectEllipse, -Constants.Width + Position.X- 50);
            Canvas.SetTop(ObjectEllipse, Position.Y);
            SolidColorBrush mySolidColorBrush = new SolidColorBrush(Color);
            ObjectEllipse.Fill = mySolidColorBrush;
            ObjectEllipse.StrokeThickness = 1;
            ObjectEllipse.Stroke = Brushes.Black;
            pitch.Children.Add(ObjectEllipse);
        }

        //public void DecrementSpeed(double )

        public override void Move(double x, double y)
        {
            Position.Offset(x, y);
        }
    }
}
