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
        //protected PointF[] Points { get; set; }
        protected List<Drawable> Children { get; } = new List<Drawable>();
        public Drawable Parent { get; }
        public IReadOnlyList<Drawable> ReadChildren => Children;
        public SolidBrush Brush { get; }
        public double Rotation { get; private set; } = 0;
        public float Radius { get; set; }
        public float X { get; protected set; }
        public float Y { get; protected set; }

        public Drawable(Drawable parent, Color color, float x, float y)
        {
            Menu.Drawables.Add(this);
            Brush = new SolidBrush(color);
            Parent = parent;
            parent?.Children.Add(this);
            X = x;
            Y = y;
        }

        public abstract void Draw(PaintEventArgs e);

        public void RotateTowards(Point point)
        {
            //SetRotation(Math.Atan2(point.X - X, Y - point.Y - Menu.Main.RectangleToScreen(Menu.Main.ClientRectangle).Y));
            SetRotation(Math.Atan2(X - point.X, point.Y - Y) + Math.PI);
        }

        public void Move(float speed, double angle)
        {
            X += (float)(Math.Sin(angle) * speed);
            Y += -(float)(Math.Cos(angle) * speed);
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

        private void ChildAfterRotation(Drawable child)
        {
            child.X = (float)(Math.Sin(Rotation) * Radius);
            child.Y = -(float)(Math.Cos(Rotation) * Radius);
        }
    }
}
