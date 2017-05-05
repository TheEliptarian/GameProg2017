using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assignment05
{
    public class Player:PhysicsSprite
    {
        
        public DateTime lastshot=DateTime.Now;
        public TimeSpan firerate = new TimeSpan(0, 0, 1);

        public Player(int x, int y) : base(Properties.Resources.player, x, y)
        {
            image = Properties.Resources.player;
            X = x;
            Y = y;
            Engine.alive = true;
        }

        public override void act()
        {
            base.act();
            if ((Vx > 0 && Ax > 0) || (Vx < 0 && Ax < 0))
            {
                Vx = 0;
                Ax = 0;
            }
            if ((Vy > 0 && Ay > 0) || (Vy < 0 && Ay < 0))
            {
                Vy = 0;
                Ay = 0;
            }
        }

        public new void Kill()
        {
			Engine.alive = false;
			base.Kill();
        }

        public void shoot(int dir)
        {
            if (!Engine.alive) return;
            if (DateTime.Now - lastshot <= firerate) return;
            Bullet1 bullet = new Bullet1((int)(X + 2 * width * Scale * 1.1f), (int)(Y + height * Scale / 2));
            bullet.X = X + 2 * width * Scale * 1.1f;
            bullet.Y = Y + height * Scale / 2;
            bullet.Vx = 50f;
            if (dir == 3)
            {
                bullet.X = X - width * Scale * 1.1f;
                bullet.Vx *= -1;
            }
            else if(dir!=1)
            {
                bullet.X = X + (width / 2);
                bullet.Vx = 0;
                bullet.Vy = 50;
            }
            if(dir == 2)
            {
                bullet.Y = Y + 2 * height * Scale * 1.1f;
            }
            else if (dir == 0)
            {
                bullet.Y = Y - height * Scale * 1.1f;
                bullet.Vy *= -1;
            }
            Engine.canvas.csAdd(bullet);
            lastshot = DateTime.Now;
        }

    }
}
