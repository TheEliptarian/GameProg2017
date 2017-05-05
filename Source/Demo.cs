using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;

namespace assignment04
{
    public class Demo : Sprite
    {
		public int TargetX = 100, TargetY = 100, Velocity = 10;

		public Demo(Image img)
		{
			Image = img;
			X = 100;
			Y = 100;
			isDemo = true;
		}

		public Demo (Image img, int x, int y)
		{
			Image = img;
			X = x;
			Y = y;
			TargetX = x;
			TargetY = y;
			isDemo = true;
		}

        private Image image;
        public Image Image
        {
            get {return image;  }
            set
			{
				image = value;
				if (image == Properties.Resources.final || image == Properties.Resources.box1final) correct = true;
			}
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
			else if(Math.Abs(X-TargetX) <= Velocity)
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
