using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cell
{
    public class Border : Drawable
    {
        public static Border Reference { get; } = new Border(null, Color.White, 0, 0);

        private Border(Drawable parent, Color color, float x, float y) : base(parent, color, x, y) { }

        public override void Draw(PaintEventArgs e) { }
        public override Drawable VisualClone() => throw new NotImplementedException();

        protected override Drawable CollisionCheck()
        {
            throw new NotImplementedException();
        }
    }
}
