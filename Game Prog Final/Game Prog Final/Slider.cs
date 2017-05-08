using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;

namespace td
{
	public class Slider : Sprite
	{
		public int TargetX = 100, TargetY = 100, Velocity = 10;

		public Slider(Image img)
		{
			Image = img;
			X = 100;
			Y = 100;
		}

		public Slider(Image img, int x, int y)
		{
			Image = img;
			X = x;
			Y = y;
			TargetX = x;
			TargetY = y;
		}

		private Image image;
		public Image Image
		{
			get { return image; }
			set { image = value; }
		}

		public override void Act()
		{
			if (X + Velocity < TargetX)
			{
				X += Velocity;
			}
			else if (X - Velocity > TargetX)
			{
				X -= Velocity;
			}
			else if (Math.Abs(X - TargetX) <= Velocity)
			{
				X = TargetX;
			}
			if (Y + Velocity < TargetY)
			{
				Y += Velocity;
			}
			else if (Y - Velocity > TargetY)
			{
				Y -= Velocity;
			}
			else if (Math.Abs(Y - TargetY) <= Velocity)
			{
				Y = TargetY;
			}
			if (X == TargetX && Y == TargetY)
			{
				Velocity = 10;
			}
			else Velocity += 10;
		}

		public override void paint(Graphics g)
		{
			g.DrawImage(image, 0, 0);
		}

	}
}