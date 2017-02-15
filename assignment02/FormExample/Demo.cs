using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;

namespace FormExample
{
    class Demo : Sprite
    {

        private Image image;
        public Image Image
        {
            get {return image;  }
            set {image = value; }
        }

        Thread rendHeaven;
        Thread update;

        public override void act()
        {
           
        }

        public override void paint(Graphics g)
        {
            g.DrawImage(image, this.X, this.Y);
        }

    }
}
