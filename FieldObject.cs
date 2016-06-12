using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace academy_project
{
    public abstract class FieldObject
    {
        public Point Position { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }

        public virtual void Draw(Canvas pitch)
        {
            
        }

        public virtual void Move(double x, double y)
        {
            
        }

    }
}
