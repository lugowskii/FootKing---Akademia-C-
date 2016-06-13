using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shapes;

namespace academy_project
{
    public class Game
    {
        private List<FieldObject> _gameObjects;
        private HashSet<Key> _keys;
        private Canvas _pitch;

        public Game(Canvas canvas)
        {
            _pitch = canvas;
            _gameObjects = new List<FieldObject>();
            _keys = new HashSet<Key>();
        }

        public bool IsAnyKeyPressed()
        {
            if (_keys.Count == 0)
                return false;
            return true;
        }

        public void AddObjectToGame(FieldObject obj)
        {
            _gameObjects.Add(obj);
        }

        public void AddKeyToList(Key key)
        {
            _keys.Add(key);
        }

        public void RemoveKeyFromList(Key key)
        {
            _keys.Remove(key);
        }

        public void ProcessKeys()
        {
            foreach (var key in _keys)
            {
                if (key == Key.Down)
                    MovePlayer(1, 0, 1);
                if (key == Key.Up)
                    MovePlayer(1, 0, -1);
                if (key == Key.Left)
                    MovePlayer(1, -1, 0);
                if (key == Key.Right)
                    MovePlayer(1, 1, 0);
                if (key == Key.S)
                    MovePlayer(2, 0, 1);
                if (key == Key.W)
                    MovePlayer(2, 0, -1);
                if (key == Key.A)
                    MovePlayer(2, -1, 0);
                if (key == Key.D)
                    MovePlayer(2, 1, 0);
            }

        }

        public void MoveBall()
        {
            CheckForBallCollisions();
            CheckForBallWallCollision();
            DecrementSpeed();
        }

        public void DecrementSpeed()
        {
            Ball ball = _gameObjects.OfType<Ball>().SingleOrDefault();
            ball.DecrementSpeed();
        }

        public double CalculateDistanceBetweenObjects(FieldObject a, FieldObject b, double x, double y)
        {
            Point midA = new Point(a.Position.X + a.Size.Width/2,
                a.Position.Y + a.Size.Height/2);
            double rA = a.Size.Width/2;
            Point midB = new Point(b.Position.X + b.Size.Width/2,
                b.Position.Y + b.Size.Height/2);
            double rB = b.Size.Width/2;
            double midDistance = Math.Floor(Math.Sqrt(
                (Math.Pow((midB.X - (midA.X + x*a.NormalizedSpeed)), 2)) +
                (Math.Pow((midB.Y - (midA.Y + y*a.NormalizedSpeed)), 2))));
            return midDistance;
        }

        public int GoalLineDecision()
        {
            Ball ball = _gameObjects.OfType<Ball>().SingleOrDefault();
            Point midA = new Point(ball.Position.X + ball.Size.Width / 2,
    ball.Position.Y + ball.Size.Height / 2);
            if (midA.X + ball.Size.Width/2 < 0)
            {
                return 2;
            }
            if (midA.X - ball.Size.Width/2 > Constants.Width)
            {
                return 1;
            }
            return 0;
        }

        public void CheckForBallWallCollision()
        {
            Ball ball = _gameObjects.OfType<Ball>().SingleOrDefault();
            double r = ball.Size.Width / 2;
            Point midA = new Point(ball.Position.X + ball.Size.Width / 2,
    ball.Position.Y + ball.Size.Height / 2);
            double x = ball.LastVector.X;
            double y = ball.LastVector.Y;
            if (ball.Position.Y >= Constants.Height/2-50 &&
                ball.Position.Y <= Constants.Height/2+50)
            {
                var window = (MainWindow)Application.Current.Windows.
                    OfType<Window>().
                    SingleOrDefault(w => w.IsActive);
                int result = GoalLineDecision();
                if (result == 1)
                {
                    int prev = Int32.Parse(window.Player1Points.Content.ToString());
                    window.Player1Points.Content = prev + 1;
                    ball.Position = Constants.StartingBallPosition;
                    ball.Speed = 0;
                }
                else if (result == 2)
                {
                    int prev = Int32.Parse(window.Player2Points.Content.ToString());
                    window.Player2Points.Content = prev + 1;
                    ball.Position = Constants.StartingBallPosition;
                    ball.Speed = 0;
                }
            }
            else if (midA.Y - ball.Size.Height/2 <= 0 && midA.X - ball.Size.Width/2 <= 7 ||
                midA.Y - ball.Size.Height/2 <= 0 && midA.X + ball.Size.Width/2 >= Constants.Width+7 ||
                midA.Y + ball.Size.Height/2 >= Constants.Height && midA.X - ball.Size.Width/2 <= 7 ||
                midA.Y + ball.Size.Height/2 >= Constants.Height && midA.X + ball.Size.Width/2 >= Constants.Width+7)
            {//corners
                x = -x;
                y = -y;
            }
            else if (midA.Y - ball.Size.Height/2 <= 7 || //UP && //DOWN
                midA.Y + ball.Size.Height/2 >= Constants.Height)
            {
                y = -y;
            }
            else if (midA.X - ball.Size.Width/2 <= 7 ||
                midA.X + ball.Size.Width/2 >= Constants.Width+7)
            {
                x = -x;
            }
            else return;
            ball.Move(x, y);
        }

        public void CheckForBallCollisions()
        {
            Ball ball = _gameObjects.OfType<Ball>().SingleOrDefault();
            double rA = ball.Size.Width / 2;
            foreach (var fieldObject in _gameObjects)
            {
                if (ball.Equals(fieldObject)) continue;
                double rB = fieldObject.Size.Width / 2;
                double midDistance = CalculateDistanceBetweenObjects(ball, fieldObject, 0, 0);
                if (midDistance - Constants.DistanceEps <= rA + rB)
                {
                    double x = ball.Position.X - fieldObject.Position.X;
                    double y = ball.Position.Y - fieldObject.Position.Y;
                    double normalizer = Math.Abs(x) + Math.Abs(y);
                    x /= normalizer;
                    y /= normalizer;
                    ball.Speed = fieldObject.Speed;
                    ball.Move(x, y);
                }
            }
        }


        public bool PlayerCollidesWithObject(Player player, double x, double y)
        {
            double rA = player.Size.Width/2;
            foreach (var fieldObject in _gameObjects)
            {
                if (player.Equals(fieldObject)) continue;
                double rB = fieldObject.Size.Width/2;
                double midDistance = CalculateDistanceBetweenObjects(player, fieldObject, x, y);
                if (midDistance+Constants.DistanceEps <= rA + rB)
                {
                    return true;
                }
            }
            return false;
        }

        public void MovePlayer(int id, double x, double y)
        {
            try
            {
                Player player = _gameObjects.
                    OfType<Player>().
                    SingleOrDefault(p => p.Id.Equals(id));
                if (!PlayerCollidesWithObject(player, x, y))
                {
                    player.Move(x, y); 
                }
            }
            catch (ArgumentNullException)
            {
                Console.Error.WriteLine("Argument is null!");
            }
            catch (InvalidOperationException)
            {
                Console.Error.WriteLine("Invalid operation on structure");
            }

        }

        public void DrawObjects()
        {
            _pitch.Children.Clear();
            foreach (var gameObject in _gameObjects)
            {
                gameObject.Draw(_pitch);
            }
        }
    }
}
