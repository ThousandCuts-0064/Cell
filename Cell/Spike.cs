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
        private readonly PointF[] referencePoints = new PointF[3];
        private readonly PointF[] localPoints = new PointF[3];
        private readonly PointF[] globalPoints = new PointF[3];
        private PointF referenceJoint;
        private PointF localJoint;
        protected Attack RealAttack { get; }
        public IReadOnlyList<PointF> LocalPoints => localPoints;
        public IReadOnlyList<PointF> GlobalPoints => globalPoints;
        public IAttack Attack => RealAttack;
        public float Median { get; private set; }

        public Spike(Drawable parent, Cell owner, Color color, float x, float y) : base(parent, owner, color, x, y)
        {
            RealAttack = new Attack(this);
            Array.Copy(defaultSize, referencePoints, 3);
            Array.Copy(referencePoints, localPoints, 3);
            //Array.Copy(localPoints, globalPoints, 3);
            OnMove();
            referenceJoint = defaultJoint;
            localJoint = referenceJoint;
            //float midHypotenuseX = (localPoints[0].X + localPoints[2].X) / 2;
            //float midHypotenuseY = (localPoints[0].Y + localPoints[2].Y) / 2;
            Median = (float)Math.Sqrt(Math.Pow(referencePoints[1].X - referenceJoint.X, 2) + Math.Pow(referencePoints[1].Y - referenceJoint.Y, 2));
        }

        public override Drawable VisualClone() => Parent == null
            ? new Spike(null, null, Brush.Color, X, Y)
            : new Spike(null, null, Brush.Color, Parent.X + X - localJoint.X, Parent.Y + Y - localJoint.Y);

        public override void Draw(PaintEventArgs e) => e.Graphics.FillPolygon(Brush, globalPoints);

        public void Shoot()
        {
            new Projectile(VisualClone(), RealAttack, Rotation, 20);
        }

        protected override void OnMove()
        {
            base.OnMove();
            if (Parent == null)
                for (int i = 0; i < globalPoints.Length; i++)
                {
                    globalPoints[i].X = localPoints[i].X + X;
                    globalPoints[i].Y = localPoints[i].Y + Y;
                }
            else
                for (int i = 0; i < globalPoints.Length; i++)
                {
                    globalPoints[i].X = Parent.X + localPoints[i].X + X - localJoint.X;
                    globalPoints[i].Y = Parent.Y + localPoints[i].Y + Y - localJoint.Y;
                }
        }

        protected override void OnRotation()
        {
            base.OnRotation();
            for (int i = 0; i < localPoints.Length; i++)
            {
                localPoints[i].X = (float)(referencePoints[i].X * Math.Cos(Rotation) - referencePoints[i].Y * Math.Sin(Rotation));
                localPoints[i].Y = (float)(referencePoints[i].X * Math.Sin(Rotation) + referencePoints[i].Y * Math.Cos(Rotation));
            }
            localJoint = new PointF((float)(referenceJoint.X * Math.Cos(Rotation) - referenceJoint.Y * Math.Sin(Rotation)),
                               (float)(referenceJoint.X * Math.Sin(Rotation) + referenceJoint.Y * Math.Cos(Rotation)));
        }

        protected override Drawable CollisionCheck()
        {
            if (GlobalPoints[1].X < 0
                || GlobalPoints[1].Y < 0
                || GlobalPoints[1].X >= Menu.Main.Right
                || GlobalPoints[1].Y >= Menu.Main.Bottom)
            {
                return Border.Reference;
            }

            return null;
        }
    }
}
