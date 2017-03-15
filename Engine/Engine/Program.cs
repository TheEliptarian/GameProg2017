using System;
using System.Windows.Forms;

namespace Engine
{
	public class Program : Engine
	{

		public static Demo demo1;


		protected override void OnKeyDown(KeyEventArgs e)
		{
			//base.OnKeyDown(e);
			//Console.WriteLine("asdffasdf");
			if (e.KeyCode == Keys.Left)
			{
				demo1.TargetX -= 30;
				demo1.Velocity = 5;
			}
			if (e.KeyCode == Keys.Right)
			{
				demo1.TargetX += 30;
				demo1.Velocity = 5;
			}
			if (e.KeyCode == Keys.Up)
			{
				demo1.TargetY -= 30;
				demo1.Velocity = 5;
			}
			if (e.KeyCode == Keys.Down)
			{
				demo1.TargetY += 30;
				demo1.Velocity = 5;
			}
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			demo1 = new Demo(Properties.Resources.download);
			demo1.TargetX = 100; 
			demo1.TargetY = 100;
			demo1.Velocity = 5;
			Program.canvas.add(demo1);
			Application.Run(new Program());
		}
	}
}