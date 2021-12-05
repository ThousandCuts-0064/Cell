using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cell
{
    public partial class Menu : Form
    {
        private static readonly List<Keys> pressedKeys = new List<Keys>();
        public static string DebugString { get; set; } = "None";
        public static Menu Main { get; private set; }
        //public static int OffsetY { get; private set; }
        public static List<Drawable> Drawables { get; } = new List<Drawable>();
        public static List<Projectile> Projectiles { get; } = new List<Projectile>();
        public static Cell MainPlayer { get; private set; }
        public static IReadOnlyList<Keys> PressedKeys => pressedKeys;

        public Menu()
        {
            InitializeComponent();
            Main = this;
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            //OffsetY = RectangleToScreen(ClientRectangle).Y;
            MainPlayer = new Cell(null, Color.Green, ClientSize.Width / 2f, ClientSize.Height / 2f, 10);
        }

        private void Menu_Paint(object sender, PaintEventArgs e)
        {
            foreach (Projectile projectile in Projectiles.ToList()) projectile.Advance();
            foreach (Drawable drawable in Drawables) drawable.Draw(e);
            e.Graphics.DrawString("Cursor.Position: " + Cursor.Position.ToString(), Font, new SolidBrush(Color.Black), 100, 100);
            e.Graphics.DrawString("Projectiles.Count: " + Projectiles.Count.ToString(), Font, new SolidBrush(Color.Black), 100, 125);
            e.Graphics.DrawString("DebugString: " + DebugString, Font, new SolidBrush(Color.Black), 100, 150);
            e.Graphics.DrawString("Drawables.Count: " + Drawables.Count.ToString(), Font, new SolidBrush(Color.Black), 100, 175);
        }

        private void Menu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape) Close();
            if (!pressedKeys.Contains(e.KeyData)) pressedKeys.Add(e.KeyData);
        }

        private void Menu_MouseDown(object sender, MouseEventArgs e)
        {
            pressedKeys.Add(e.Button.ToKeys());
        }

        private void Menu_KeyUp(object sender, KeyEventArgs e)
        {
            pressedKeys.Remove(e.KeyData);
        }

        private void Menu_MouseUp(object sender, MouseEventArgs e)
        {
            pressedKeys.Remove(e.Button.ToKeys());
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //Text = $@"{Math.Round(MainPlayer.Rotation * 180 / Math.PI, 4, MidpointRounding.AwayFromZero)} PX: {MainPlayer.X} PY: {MainPlayer.Y} CX: {Cursor.Position.X} CY: {Cursor.Position.Y} DX: {Cursor.Position.X - MainPlayer.X} DY: {Cursor.Position.Y - MainPlayer.Y - RectangleToScreen(ClientRectangle).Y}";
            MainPlayer.Action(pressedKeys);
            MainPlayer.RotateTowards(Cursor.Position);
            Spawner.TrySpawn();
            Refresh();
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }
    }
}
