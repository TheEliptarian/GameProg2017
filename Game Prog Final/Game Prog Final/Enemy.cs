using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System;

namespace td
{
	public class Enemy : Slider
	{
		int hp;

		public Enemy(Image img) : base(img)
		{
		}

		public Enemy(Image img, int x, int y) : base(img, x, y)
		{
			
		}

	}
}
