using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;

namespace FormExample
{
    public class Demo : Sprite
    {
		int a = 0;
		private Image image;
        public Image Image
        {
            get {return image;  }
            set {image = value; }
        }

        public void Act(int amount)
        {
			base.Act();
			
			if ((a%100) < 50)
			{
				this.X += amount;
				this.Y += amount;
			}
			else
			{
				this.X -= amount;
				this.Y -= amount;
			}
			a++;
		}

        public override void paint(Graphics g)
        {
            g.DrawImage(image, X, Y);
        }

    }
}
