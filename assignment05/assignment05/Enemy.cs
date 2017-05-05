using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment05
{
	public class Enemy : PhysicsSprite
	{
		private Boolean left = false;
		private Random r = new Random();

		public Enemy() : base(Properties.Resources.enemy)
		{
			Vx = 4f;
		}

		public Enemy(int x, int y) : base(Properties.Resources.enemy, x, y)
		{
			Vx = 4f;
		}

		public void Shoot()
		{

			Bullet bullet = new Bullet((int)(X + Width * Scale * 1.1f), (int)(Y + Height * Scale / 2));
			if (Engine.player.X < X)
			{
				bullet.X = X - 26;
				bullet.Vx *= -1;
			}
			Engine.canvas.add(bullet);
		} 

		public bool isWall()
		{
			X += Vx;
			if (getCollisions().Count > 0)
			{
				X -= Vx;
				return true;
			}
			X -= Vx;
			return false;
		}

		public void killCharacter()
		{
			X += Vx;
			List<CollisionSprite> list = getCollisions();
			X -= Vx;
			foreach (CollisionSprite s in list)
			{
				if (s.GetType() == typeof(Player))
				{
					s.Kill();
					Engine.alive = false;
				}
			}
		}
	
		public override void act()
		{
			base.act();
			killCharacter();
			if (r.NextDouble() < .005) Vy = -10;
			//if (r.NextDouble() < .005) Shoot();
			if (isWall()) Vx *= -1;
			if (Vx < 0) left = true;
			if (Vx > 0) left = false;
		}

	}
}
