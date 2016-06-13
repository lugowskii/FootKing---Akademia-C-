using System.Windows.Controls;

namespace academy_project
{
    interface ObjectBehaviour
    {
        void Draw(Canvas pitch);
        void Move(double x, double y);
    }
}
