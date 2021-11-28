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
        public float Health { get; private set; } = 100;
        public IReadOnlyList<Spike> Spikes => spikes;

        public Cell(Drawable parent, Color color, float x, float y, float radius) : base(parent, color, x, y)
        {
            Radius = radius;
            spikes.Add(new Spike(this, this, Color.Gray, 0, -radius));
        }

        public override void Draw(PaintEventArgs e) => e.Graphics.FillEllipse(Brush, X - Radius, Y - Radius, Radius * 2, Radius * 2);    

        public void Action(Keys key)
        {
            switch (key)
            {
                case Keys.W:
                    Y--; 
                    break;
                case Keys.S:
                    Y++;
                    break;
                case Keys.A:
                    X--;
                    break;
                case Keys.D:
                    X++;
                    break;
            }
        }

        public void TakeDamage(Spike spike)
        {
            Health -= spike.Damage;
            if (Health < 0) Dispose();
        }

        public override void Dispose()
        {
            base.Dispose();
            foreach (Spike spike in spikes) spike.Dispose();
        }
    }
}
