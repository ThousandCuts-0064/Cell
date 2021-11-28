using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cell
{
    public class Spike : CellPart
    {
        public static IReadOnlyList<PointF> DefaultSize => defaultSize;
        private static readonly PointF[] defaultSize = { new PointF(-4, 3), new PointF(0, -6), new PointF(4, 3) };
        private static readonly PointF defaultJoint = new PointF(0, 2.75f);
        private readonly PointF[] localPoints = new PointF[3];
        private PointF localJoint;
        private PointF joint;
        private readonly PointF[] points = new PointF[3];
        public Cell Owner { get; }
        public IReadOnlyList<PointF> Points => points;
        public float Median { get; private set; }
        public float Damage { get; private set; } = 1;

        public Spike(Drawable parent, Cell owner, Color color, float x, float y) : base(parent, color, x, y)
        {
            Owner = owner;
            Array.Copy(defaultSize, localPoints, 3);
            Array.Copy(localPoints, points, 3);
            localJoint = defaultJoint;
            joint = localJoint;
            //float midHypotenuseX = (localPoints[0].X + localPoints[2].X) / 2;
            //float midHypotenuseY = (localPoints[0].Y + localPoints[2].Y) / 2;
            Median = (float)Math.Sqrt(Math.Pow(localPoints[1].X - localJoint.X, 2) + Math.Pow(localPoints[1].Y - localJoint.Y, 2));
        }

        public override void Draw(PaintEventArgs e) => e.Graphics.FillPolygon(Brush, new PointF[]
        {
            new PointF(Owner.X + points[0].X + X - joint.X, Owner.Y + points[0].Y + Y - joint.Y),
            new PointF(Owner.X + points[1].X + X - joint.X, Owner.Y + points[1].Y + Y - joint.Y),
            new PointF(Owner.X + points[2].X + X - joint.X, Owner.Y + points[2].Y + Y - joint.Y)
        });

        public void DealDamage(Cell target)
        {
            target.TakeDamage(this);
        }

        protected override void OnRotation()
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i].X = (float)(localPoints[i].X * Math.Cos(Rotation) - localPoints[i].Y * Math.Sin(Rotation));
                points[i].Y = (float)(localPoints[i].X * Math.Sin(Rotation) + localPoints[i].Y * Math.Cos(Rotation));
            }
            joint = new PointF((float)(localJoint.X * Math.Cos(Rotation) - localJoint.Y * Math.Sin(Rotation)),
                               (float)(localJoint.X * Math.Sin(Rotation) + localJoint.Y * Math.Cos(Rotation)));
        }
    }
}
