using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace academy_project
{
    public abstract class FieldObject : ObjectBehaviour
    {
        public Point Position { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }
        public Ellipse ObjectEllipse { get; set; }
        public double Speed { get; set; }
        public double NormalizedSpeed { get; set; }

        public abstract void Draw(Canvas pitch);
        public abstract void Move(double x, double y);
    }
}
