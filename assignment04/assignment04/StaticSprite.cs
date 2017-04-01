using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;

namespace assignment04
{
	public class StaticSprite : Sprite
	{
		public StaticSprite(Image img)
		{
			Image = img;
			X = 100;
			Y = 100;
		}

		public StaticSprite(Image img, int x, int y)
		{
			Image = img;
			X = x;
			Y = y;
		}

		private Image image;
		public Image Image
		{
			get { return image; }
			set { image = value; }
		}
		public override void paint(Graphics g)
		{
			g.DrawImage(image, 0, 0);
		}

	}
}