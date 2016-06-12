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

        public void MovePlayer(int id, double x, double y)
        {
            Player player = _gameObjects.OfType<Player>().
                Where(p => p.Id.Equals(id)).
                SingleOrDefault();
            player.Move(x, y);
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
