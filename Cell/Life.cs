using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell
{
    public interface ILife
    {
        Drawable Owner { get; }
        float Health { get; }

        void TakeDamage(IAttack attacker);
    }

    public class Life : ILife
    {
        public Drawable Owner { get; }
        public float Health { get; set; } = 100;

        public Life(Drawable owner)
        {
            Owner = owner;
        }

        public void TakeDamage(IAttack attacker)
        {
            Health -= attacker.Damage;
            if (Health < 0) Owner.Dispose();
        }
    }
}
