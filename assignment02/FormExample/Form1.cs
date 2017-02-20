using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;

namespace FormExample
{
    public partial class Form1 : Form
    {
        public static Form form;
        public static Thread rendHeaven;
		public static Demo Demo = new Demo();

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
			Demo.Image = FormExample.Properties.Resources.download;
			Demo.X = (ClientSize.Width / 2);
			Demo.Y = (ClientSize.Height / 2);
			rendHeaven = new Thread(new ThreadStart(Update));
			rendHeaven.Start();
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			rendHeaven.Abort();
		}

		public static void Update()
		{
			DateTime last = DateTime.Now;
			DateTime now = last;
			TimeSpan frameTime = new TimeSpan(10000000 / fps);
			while(true)
			{
				DateTime temp = DateTime.Now;
				running_fps = .9 * running_fps + .1 * 1000.0 / (temp - now).TotalMilliseconds;
				Console.WriteLine(running_fps);
				now = temp;
				TimeSpan diff = now - last;
				if(diff.TotalMilliseconds < frameTime.TotalMilliseconds)
				{
					Thread.Sleep((frameTime - diff).Milliseconds);
				}
				last = DateTime.Now;
				form.Invoke(new MethodInvoker(form.Refresh));
			}
		}

		private void Form1_Load(Object sender, EventArgs e)
        { }

        protected override void OnKeyDown(KeyEventArgs e)
        {
        }

        private void UpdateSize()
        {

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
        protected override void OnPaint(PaintEventArgs e)
        {
			Demo.Act(this);
			Demo.render(e.Graphics);
        }

    }
}
