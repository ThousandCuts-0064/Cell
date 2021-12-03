using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cell
{
    public class Cell : Drawable
    {
        private readonly List<Spike> spikes = new List<Spike>();
        protected Life RealLife { get; }
        public ILife Life => RealLife;
        public IReadOnlyList<Spike> Spikes => spikes;
        public float Speed { get; private set; } = 1;

        public Cell(Drawable parent, Color color, float x, float y, float radius) : base(parent, color, x, y)
        {
            RealLife = new Life(this);
            Radius = radius;
            spikes.Add(new Spike(this, this, Color.Gray, 0, -radius));
        }

        public override Drawable VisualClone() => new Cell(null, Brush.Color, X, Y, Radius);
        public override void Draw(PaintEventArgs e) => e.Graphics.FillEllipse(Brush, X - Radius, Y - Radius, Radius * 2, Radius * 2);    

        public void Action(List<Keys> keys)
        {
            PointF point = new PointF();
            foreach (Keys key in keys)
            {
                switch (key)
                {
                    case Keys.LButton:
                        spikes[0].Shoot();
                        break;
                    case Keys.W:
                        point.Y += 1;
                        break;
                    case Keys.S:
                        point.Y -= 1;
                        break;
                    case Keys.A:
                        point.X -= 1;
                        break;
                    case Keys.D:
                        point.X += 1;
                        break;
                }
            }
            Move(point, Speed);
        }

        public override void Dispose()
        {
            base.Dispose();
            foreach (Spike spike in spikes) spike.Dispose();
        }
    }
}
