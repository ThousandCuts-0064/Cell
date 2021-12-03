using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cell
{
    public static class Extensions
    {
        public static Keys ToKeys(this MouseButtons mouseButtons)
        {
            switch (mouseButtons)
            {
                case MouseButtons.Left: return Keys.LButton;
                case MouseButtons.Right: return Keys.RButton;
                case MouseButtons.Middle: return Keys.MButton;
                case MouseButtons.XButton1: return Keys.XButton1;
                case MouseButtons.XButton2: return Keys.XButton2;
                default: return Keys.None;
            }
        }
    }
}
