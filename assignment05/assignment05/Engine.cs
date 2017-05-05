using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace assignment05
{
	public partial class Engine : Form
	{
		public static Player player = new Player(150, 500);
		public static Sprite canvas = new Sprite();
		public static  bool alive = true;
		public static int enemyCount = 0;
		public static Enemy enemy = new Enemy(850, 500);
		public static Rectangle rect = new Rectangle(0, 0, 1400, 900, 200);
		public static bool win = false;
		public static bool lose = false;
		public static TextSprite text = new TextSprite(0, 0, "You Win!\nPress R to Restart.");
		public static TextSprite loss = new TextSprite(0, 0, "You Lose.\nPress R to Restart.");

		private void Engine_Load(object sender, EventArgs e) { }

		public Sprite Canvas
		{
			set { canvas = value; }
			get { return canvas; }
		}

		public static Engine form;
		public Thread rthread;
		public Thread uthread;
		public static int fps = 30;
		public static double running_fps = 30.0;
		public static bool rendering = false;
		public static bool updating = false;
		public static bool resetting = false;

		public Engine()
		{
			DoubleBuffered = true;
			InitializeComponent();
			form = this;
			Size = new Size(1300, 800);
			StartPosition = FormStartPosition.CenterScreen;
			rthread = new Thread(new ThreadStart(render));
			uthread = new Thread(new ThreadStart(update));
			rthread.Start();
			uthread.Start();
			canvas.add(rect);
			canvas.add(text);
			canvas.add(loss);
			enemyCount++;
		}

		public static void reset()
		{
			canvas.RemoveAll();
			resetting = true;
			win = false;
			lose = false;
			player = new Player(150, 500);
			alive = true;
			canvas.csAdd(player);
			for (int i = 0; i < 13; i++)
			{
				Box box = new Box(i * 100, 0);
				canvas.csAdd(box);
				box = new Box(i * 100, 600);
				canvas.csAdd(box);
			}
			for (int i = 0; i < 7; i++)
			{
				Box box = new Box(0, i * 100);
				canvas.csAdd(box);
				box = new Box(1200, i * 100);
				canvas.csAdd(box);
			}
			for (int i = 3; i < 6; i++)
			{
				Box box = new Box(i * 100, 400);
				canvas.csAdd(box);
			}
			for (int i = 8; i < 11; i++)
			{
				Box box = new Box(i * 100, 300);
				canvas.csAdd(box);
			}
			enemy = new Enemy(850, 500);
			enemyCount = 1;
			canvas.csAdd(enemy);
			canvas.add(rect);
			canvas.add(text);
			canvas.add(loss);
			resetting = false;
		}

		public static void render()
		{
			DateTime last = DateTime.Now;
			DateTime now = last;
			TimeSpan frameTime = new TimeSpan(10000000 / fps);
			while (true)
			{
				DateTime temp = DateTime.Now;
				running_fps = .9 * running_fps + .1 * 1000.0 / (temp - now).TotalMilliseconds;
				now = temp;
				TimeSpan diff = now - last;
				if (diff.TotalMilliseconds < frameTime.TotalMilliseconds)
					Thread.Sleep((frameTime - diff).Milliseconds);
				last = DateTime.Now;
				if (resetting) continue;
				rendering = true;
				if (win)
				{
					rect.setColor(Rectangle.initColor);
					rect.setVisibility(true);
					text.setVisibility(true);
					text.changeLocation((form.ClientSize.Width / 2) - 50, (form.ClientSize.Height / 2) - 50);
				}
				else if (!alive)
				{
					rect.setColor(Color.FromArgb(200, Color.Red));
					rect.setVisibility(true);
					loss.changeLocation((form.ClientSize.Width / 2) - 50, (form.ClientSize.Height / 2) - 50);
					loss.setVisibility(true);
					lose = true;
				}
				else
				{
					rect.setVisibility(false);
					text.setVisibility(false);
					loss.setVisibility(false);
					win = false;
					lose = false;
				}
				form.Invoke(new MethodInvoker(form.Refresh));
				rendering = false;
			}
		}

		public static void update()
		{
			DateTime last = DateTime.Now;
			DateTime now = last;
			TimeSpan frameTime = new TimeSpan(10000000 / fps);
			while (true)
			{
				DateTime temp = DateTime.Now;
				now = temp;
				TimeSpan diff = now - last;
				if (diff.TotalMilliseconds < frameTime.TotalMilliseconds)
					Thread.Sleep((frameTime - diff).Milliseconds);
				last = DateTime.Now;
				if (resetting) continue;
				updating = true;
				canvas.update();
				updating = false;
				if (!updating && !rendering)
				{
					canvas.queueClear();
					canvas.updateAllTracking();
				}
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			canvas.render(e.Graphics);
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.Down)
			{
				player.Vy = 5;
				player.Ay = -0.1f;
			}
			else if (e.KeyCode == Keys.Up)
			{
				player.Vy = -5;
				player.Ay = 0.1f;
			}
			else if (e.KeyCode == Keys.Left)
			{
				player.Vx = -5;
				player.Ax = 0.1f;
				//if (player.Image == Properties.Resources.player)
				{
					player.Image = Properties.Resources.player1;
				}
			}
			else if (e.KeyCode == Keys.Right)
			{
				player.Vx = 5;
				player.Ax = -0.1f;
				//if (player.Image == Properties.Resources.player1)
				{
					player.Image = Properties.Resources.player;
				}
			}
			else if (e.KeyCode == Keys.D)
			{
				player.shoot(1);
			}
			else if (e.KeyCode == Keys.S)
			{
				player.shoot(2);
			}
			else if (e.KeyCode == Keys.A)
			{
				player.shoot(3);
			}
			else if (e.KeyCode == Keys.R)
			{
				reset();
			}
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			uthread.Abort();
			rthread.Abort();
		}

	}

}