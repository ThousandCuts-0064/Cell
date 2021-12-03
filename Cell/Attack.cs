using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell
{
    public interface IAttack
    {
        Drawable Owner { get; }
        float Damage { get; }

        void DealDamage(ILife target);
    }

    public class Attack : IAttack
    {
        public Drawable Owner { get; }
        public float Damage { get; set; } = 1;

        public Attack(Drawable owner)
        {
            Owner = owner;
        }

        public void DealDamage(ILife target)
        {
            target.TakeDamage(this);
        }
    }
}
