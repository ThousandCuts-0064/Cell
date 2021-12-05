using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell
{
    public delegate void DieDelegate();

    public interface ILife
    {
        event DieDelegate DieEvent;
        Drawable Owner { get; }
        float Health { get; }

        void TakeDamage(IAttack attacker);
    }

    public class Life : ILife
    {
        public event DieDelegate DieEvent;
        public Drawable Owner { get; }
        public float Health { get; set; } = 100;

        public Life(Drawable owner)
        {
            Owner = owner;
        }


        public void TakeDamage(IAttack attacker)
        {
            Health -= attacker.Damage;
            if (Health < 0)
            {
                DieEvent.Invoke();
                Owner.Dispose();
            }
        }
    }
}
