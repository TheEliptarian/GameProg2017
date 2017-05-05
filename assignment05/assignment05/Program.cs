using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assignment05
{
	class Program : Engine
	{
		public Program() : base()
		{

		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Program.canvas.csAdd(player);
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
			Program.canvas.csAdd(enemy);
			Application.Run(new Program());
		}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// Program
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(1284, 761);
			this.Name = "Program";
			this.Load += new System.EventHandler(this.Program_Load);
			this.ResumeLayout(false);

		}

		private void Program_Load(object sender, EventArgs e)
		{

		}
	}
}