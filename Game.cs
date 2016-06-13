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

        public double CalculateDistanceBetweenObjects(FieldObject a, FieldObject b, int x, int y)
        {
            Point midA = new Point(a.Position.X + a.Size.Width/2,
                a.Position.Y + a.Size.Height/2);
            double rA = a.Size.Width/2;
            Point midB = new Point(b.Position.X + b.Size.Width/2,
                b.Position.Y + b.Size.Height/2);
            double rB = b.Size.Width/2;
            double midDistance = (Math.Sqrt(
                (Math.Pow((midB.X - (midA.X + x*a.NormalizedSpeed)), 2)) +
                (Math.Pow((midB.Y - (midA.Y + y*a.NormalizedSpeed)), 2))));
        }

        public void CheckForBallCollisions()
        {
            Ball ball = _gameObjects.OfType<Ball>().SingleOrDefault();
            Point midA = new Point(ball.Position.X + ball.Size.Width / 2,
                ball.Position.Y + ball.Size.Height / 2);
            double rA = ball.Size.Width / 2;
            foreach (var fieldObject in _gameObjects)
            {
                if (ball.Equals(fieldObject)) continue;
                Point midB = new Point(fieldObject.Position.X + fieldObject.Size.Width / 2,
    fieldObject.Position.Y + fieldObject.Size.Height / 2);
                double rB = fieldObject.Size.Width / 2;
                double midDistance = (Math.Sqrt(
                    (Math.Pow((midB.X - (midA.X + x * player.NormalizedSpeed)), 2)) +
                    (Math.Pow((midB.Y - (midA.Y + y * player.NormalizedSpeed)), 2))));
                if (midDistance < rA + rB)
                {
                    return true;
                }
            }
        }

        

        public bool PlayerCollidesWithObject(Player player, double x, double y)
        {
            Point midA = new Point(player.Position.X + player.Size.Width/2,
                player.Position.Y + player.Size.Height/2);
            double rA = player.Size.Width/2;
            foreach (var fieldObject in _gameObjects)
            {
                if (player.Equals(fieldObject)) continue;
                Point midB = new Point(fieldObject.Position.X + fieldObject.Size.Width/2,
    fieldObject.Position.Y + fieldObject.Size.Height/2);
                double rB = fieldObject.Size.Width/2;
                double midDistance = (Math.Sqrt(
                    (Math.Pow((midB.X - (midA.X+x*player.NormalizedSpeed)), 2)) + 
                    (Math.Pow((midB.Y - (midA.Y+y*player.NormalizedSpeed)), 2))));
                if (midDistance < rA + rB)
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
                else CheckForBallCollisions();
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
