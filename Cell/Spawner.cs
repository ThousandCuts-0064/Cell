using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell
{
    static class Spawner
    {
        private static readonly Random random = new Random();
        public static List<Cell> Cells { get; } = new List<Cell>();
        public static int ThresholdTicks { get; set; } = 20;
        public static int PassedTicks { get; private set; }

        public static bool TrySpawn()
        {
            PassedTicks++;
            if (PassedTicks != ThresholdTicks) return false;

            PassedTicks = 0;
            Cell @new = new Cell(null, Color.Red, random.Next(0, Menu.Main.Right), random.Next(0, Menu.Main.Bottom), 10);
            Cells.Add(@new);
            @new.Life.DieEvent += () => Cells.Remove(@new);
            Menu.DebugString = @new.Spikes[0].X + " " + @new.Spikes[0].Y;
            return true;
        }
    }
}
