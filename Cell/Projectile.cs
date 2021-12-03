﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell
{
    public class Projectile : IDisposable
    {
        private readonly float speed;
        protected Attack RealAttack { get; }
        public Drawable Drawable { get; }
        public IAttack Attack => RealAttack;

        public Projectile(Drawable drawable, Attack attack, double angle, float speed)
        {
            Menu.Projectiles.Add(this);
            this.speed = speed;
            Drawable = drawable;
            Drawable.SetRotation(angle);
            RealAttack = attack;
        }

        public void Advance()
        {
            //if (Drawable)
            Drawable.Move(speed);
        }

        public void Dispose()
        {
            Menu.Projectiles.Remove(this);
            Drawable.Dispose();
        }
    }
}
