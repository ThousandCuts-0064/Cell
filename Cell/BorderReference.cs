using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cell
{
    public class BorderReference : Drawable
    {
        public BorderReference(Drawable parent, Color color, float x, float y) : base(parent, color, x, y) { }

        public override void Draw(PaintEventArgs e) => throw new NotImplementedException();
        public override Drawable VisualClone() => throw new NotImplementedException();
    }
}
