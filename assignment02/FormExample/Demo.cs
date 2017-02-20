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

        private Image image;
        public Image Image
        {
            get {return image;  }
            set {image = value; }
        }

        public void Act(Form1 f)
        {
			base.Act();
			int a = 0;
			if ((a%100) < 50)
			{
				this.X++;
				this.Y++;
			}
			else
			{
				this.X--;
				this.Y--;
			}
			a++;
		}

        public override void paint(Graphics g)
        {
            g.DrawImage(image, X, Y);
        }

    }
}
