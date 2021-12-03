using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cell
{
    public abstract class Drawable : IDisposable
    {
        private float x;
        private float y;
        //protected PointF[] Points { get; set; }
        protected List<Drawable> Children { get; } = new List<Drawable>();
        public static BorderReference Border { get; } = new BorderReference(null, Color.White, 0, 0);
        public Drawable Parent { get; }
        public IReadOnlyList<Drawable> ReadChildren => Children;
        public SolidBrush Brush { get; }
        public double Rotation { get; private set; } = 0;
        public float Radius { get; set; }
        public float X
        {
            get => x;
            set
            {
                x = value;
                OnMove();
            }
        }
        public float Y
        {
            get => y; 
            set
            {
                y = value;
                OnMove();
            }
        }

        public Drawable(Drawable parent, Color color, float x, float y)
        {
            Menu.Drawables.Add(this);
            Brush = new SolidBrush(color);
            Parent = parent;
            parent?.Children.Add(this);
            this.x = x;
            this.y = y;
        }

        public abstract Drawable VisualClone();
        public abstract void Draw(PaintEventArgs e);

        public void SetXY(float x, float y)
        {
            this.x = x;
            this.y = y;
            OnMove();
        }

        public void AddXY(float x, float y)
        {
            this.x += x;
            this.y += y;
            OnMove();
        }

        public void Move(float speed) => AddXY((float)(Math.Sin(Rotation) * speed), -(float)(Math.Cos(Rotation) * speed));

        public void Move(double angle, float speed) => AddXY((float)(Math.Sin(angle) * speed), -(float)(Math.Cos(angle) * speed));

        public void Move(PointF point, float speed)
        {
            if (!point.IsEmpty) Move(Math.Atan2(point.X, point.Y), speed);
        }

        public void RotateTowards(Point point)
        {
            //SetRotation(Math.Atan2(point.X - X, Y - point.Y - Menu.Main.RectangleToScreen(Menu.Main.ClientRectangle).Y));
            SetRotation(Math.Atan2(X - point.X, point.Y - Y) + Math.PI);
            OnRotation();
        }

        public void Rotate(double radians)
        {
            Rotation += radians;
            Rotation %= Math.PI * 2;
            OnRotation();
            foreach (Drawable child in Children)
            {
                child.Rotate(radians);
                ChildAfterRotation(child);
            }
        }

        public void SetRotation(double radians)
        {
            Rotation = radians % (Math.PI * 2);
            OnRotation();
            foreach (Drawable child in Children)
            {
                child.SetRotation(radians);
                ChildAfterRotation(child);
            }
        }

        public virtual void Dispose()
        {
            Brush.Dispose();
            Parent.Children.Remove(this);
            Menu.Drawables.Remove(this);
        }

        protected virtual void OnRotation() { }
        protected virtual void OnMove() { }

        private void ChildAfterRotation(Drawable child) => child.SetXY((float)(Math.Sin(Rotation) * Radius), -(float)(Math.Cos(Rotation) * Radius));
    }
}
