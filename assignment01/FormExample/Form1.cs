using System;
using System.Drawing;

using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;

namespace FormExample
{
    public partial class Form1 : Form
    {
        public static Form form;
        public static Thread thread;

        public static int counter = 1;
        public static bool color = false;
        public static int fps = 30;
        public static double running_fps = 30.0;
        public static int v;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            form = this;
            thread = new Thread(new ThreadStart(run));
            thread.Start();

        }

        public static void run()
        {
            DateTime last = DateTime.Now;
            DateTime now = last;
            TimeSpan frameTime = new TimeSpan(10000000 / fps);
            while (true)
            {
                DateTime temp = DateTime.Now;
                running_fps = .9 * running_fps + .1 * 1000.0 / (temp - now).TotalMilliseconds;
                Console.WriteLine(running_fps);
                now = temp;
                TimeSpan diff = now - last;
                if (diff.TotalMilliseconds < frameTime.TotalMilliseconds)
                    Thread.Sleep((frameTime-diff).Milliseconds);
                last = DateTime.Now;
                
                CalcAnim();
                form.Invoke(new MethodInvoker(form.Refresh));
                
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                counter += 50;
            }
            else if (e.KeyCode == Keys.C)
            {
                color = !color;
            }
        }

        private void UpdateSize()
        {
            
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            thread.Abort();
        }

        protected override void OnResize(EventArgs e)
        {

            
            Refresh();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            //Refresh();
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            //Refresh();
        }

        private static void CalcAnim()
        {
            v = (int)(200 + 100 * Math.Cos(DateTime.Now.Millisecond / 500.0));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DateTime now = DateTime.Now;

            //Console.WriteLine(s);           
            int n = 0;
            for (int i = counter; i > 0; i--)
            {
                Random rng = new Random((int)(i * Math.Sin(v) * Math.Sin(i) * 12));
                Color rndclr = Color.FromArgb(rng.Next(256), rng.Next(256), rng.Next(256));
                SolidBrush brush = new SolidBrush(rndclr);
                if (n % 5 == 0) n = 0;
               
                Rectangle x = new Rectangle(0, 0, v, v);
                e.Graphics.DrawRectangle(Pens.Black, x);
                if (color)
                {
                    e.Graphics.FillRectangle(brush, x);
                }
                n++;
            }
            System.Drawing.Font font = new System.Drawing.Font("Verdana", 12);
            e.Graphics.DrawString(counter.ToString(), font, Brushes.Black, 50, 50);
            e.Graphics.DrawString(running_fps.ToString(), font, Brushes.Black, 50, 0 + ClientSize.Height - 50);
        }


    }
    
}
