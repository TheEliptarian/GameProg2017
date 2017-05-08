using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace td
{
	public class Map
	{
		Image image;
		String text;

		Map(Image img, String text)
		{
			image = img;
			this.text = text;
		}
	}
}
