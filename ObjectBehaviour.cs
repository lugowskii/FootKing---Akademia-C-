using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace academy_project
{
    interface ObjectBehaviour
    {
        void Draw(Canvas pitch);
        void Move(double x, double y);
    }
}
