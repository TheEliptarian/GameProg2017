using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Engine
{
	public partial class Engine : Form
	{
		public static Form form;
		public static Demo demo = new Demo(Properties.Resources.download);
		public static Sprite canvas = new Sprite();
		public static Thread RenderThread, UpdateThread;

		public static Sprite parent = new Sprite();
		public static int s = 100;
		public static int fps = 30;
		public static double running_fps = 30.0;


		public Engine()
		{
			InitializeComponent();
			DoubleBuffered = true;
			form = this;
			UpdateThread = new Thread(new ThreadStart(update));
			RenderThread = new Thread(new ThreadStart(render));
			UpdateThread.Start();
			RenderThread.Start();
		}

		private void Form1_Load(object sender, EventArgs e) { }

		public static void update()
		{
			DateTime l = DateTime.Now;
			DateTime n = l;
			TimeSpan frame = new TimeSpan(10000000 / fps);
			while (true)
			{
				Console.WriteLine("Update");
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

		private void Engine_Load(object sender, EventArgs e)
		{

		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			//base.OnKeyDown(e);
			//Console.WriteLine("asdffasdf");
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			//base.OnPaint(e);
			canvas.render(e.Graphics);			
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			RenderThread.Abort();
			UpdateThread.Abort();
		}
	}
}
