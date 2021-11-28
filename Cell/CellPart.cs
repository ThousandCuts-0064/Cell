using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell
{
    public abstract class CellPart : Drawable
    {

        public CellPart(Drawable parent, Color color, float x, float y) : base(parent, color, x, y)
        {

        }
    }
}
