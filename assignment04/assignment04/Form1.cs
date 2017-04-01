using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assignment04
{
	public partial class Sokoban : Form
	{
		public static Form form;
		public static Sprite canvas = new Sprite();
		public static Thread RenderThread, UpdateThread;

		public static Sprite parent = new Sprite();
		public static int s = 100;
		public static int fps = 30;
		public static double running_fps = 30.0;


		public Sokoban()
		{
			InitializeComponent();
			DoubleBuffered = true;
			form = this;
			UpdateThread = new Thread(new ThreadStart(update));
			RenderThread = new Thread(new ThreadStart(render));
			UpdateThread.Start();
			RenderThread.Start();
		}

		private void Sokoban_Load(object sender, EventArgs e) { }

		public static void update()
		{
			DateTime l = DateTime.Now;
			DateTime n = l;
			TimeSpan frame = new TimeSpan(10000000 / fps);
			while (true)
			{
				DateTime temp = DateTime.Now;
				n = temp;
				TimeSpan diff = n - l;
				if (diff.TotalMilliseconds < frame.TotalMilliseconds)
				{
					Thread.Sleep((frame - diff).Milliseconds);
				}
				l = DateTime.Now;
				canvas.update();
			}
		}

		public static void render()
		{
			DateTime l = DateTime.Now;
			DateTime n = l;
			TimeSpan frame = new TimeSpan(10000000 / fps);
			while (true)
			{
				DateTime temp = DateTime.Now;
				n = temp;
				TimeSpan diff = n - l;
				if (diff.TotalMilliseconds < frame.TotalMilliseconds)
				{
					Thread.Sleep((frame - diff).Milliseconds);
				}
				l = DateTime.Now;
				form.Invoke(new MethodInvoker(form.Refresh));
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			//base.OnPaint(e);
			canvas.render(e.Graphics);
			if(Program.currentLevel.done)
			{
				Rectangle rect = new Rectangle(0, 0, Width, Height);
				Color color = Color.FromArgb(100, Color.ForestGreen);
				Brush brush = new SolidBrush(color);
				e.Graphics.DrawRectangle(Pens.Red, rect);
				e.Graphics.FillRectangle(brush, rect);
				System.Drawing.Font font = new System.Drawing.Font("Ubuntu", (Math.Min(ClientSize.Width, ClientSize.Height) / 50));
				e.Graphics.DrawString("You win!\n Press Enter to proceed or 'r' to reset!", font, Brushes.Black, (ClientSize.Width / 5), (ClientSize.Height / 2));
			}
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			RenderThread.Abort();
			UpdateThread.Abort();
		}
	}
}