using System;
using System.Windows.Forms;

namespace assignment04
{
	public class Program : Sokoban
	{

		public static Demo player;
		public static Level lv1, lv2, lv3, lv4, lv5, currentLevel;
		public static StaticSprite[,] goals;
		public static StaticSprite[,] goal1s;
		public static StaticSprite[,] goal2s;
		public static StaticSprite[,] goal3s;
		public static StaticSprite[,] goal4s;
		public static StaticSprite[,] walls;
		public static StaticSprite[,] floor;
		public static Demo[,] blocks;
		public static Demo[,] box1s;
		public static Demo[,] box2s;
		public static Demo[,] box3s;
		public static Demo[,] box4s;
		public static int x;
		public static int y;

		public static void levelHelper()
		{
			lv1 = new Level(5, 5, Properties.Resources.level1);
			lv2 = new Level(10, 6, Properties.Resources.level2);
			lv3 = new Level(10, 10, Properties.Resources.level3);
			lv4 = new Level(16, 10, Properties.Resources.level4);
			lv5 = new Level(10, 10, Properties.Resources.level5);
			currentLevel = lv1;
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Right)
			{
				if (canMoveTo(x + 1, y, 1, 0)) x++;
				if (blocks[x, y] != null) moveBlock(x, y, 1, 0, 0);
				else if (box1s[x, y] != null) moveBlock(x, y, 1, 0, 1);
				else if (box2s[x, y] != null) moveBlock(x, y, 1, 0, 2);
				else if (box3s[x, y] != null) moveBlock(x, y, 1, 0, 3);
				else if (box4s[x, y] != null) moveBlock(x, y, 1, 0, 4);
			}
			if (e.KeyCode == Keys.Left)
			{
				if (canMoveTo(x - 1, y, -1, 0)) x--;
				if (blocks[x, y] != null) moveBlock(x, y, -1, 0, 0);
				else if (box1s[x, y] != null) moveBlock(x, y, -1, 0, 1);
				else if (box2s[x, y] != null) moveBlock(x, y, -1, 0, 2);
				else if (box3s[x, y] != null) moveBlock(x, y, -1, 0, 3);
				else if (box4s[x, y] != null) moveBlock(x, y, -1, 0, 4);
			}
			if (e.KeyCode == Keys.Up)
			{
				if (canMoveTo(x, y - 1, 0, -1)) y--;
				if (blocks[x, y] != null) moveBlock(x, y, 0, -1, 0);
				else if (box1s[x, y] != null) moveBlock(x, y, 0, -1, 1);
				else if (box2s[x, y] != null) moveBlock(x, y, 0, -1, 2);
				else if (box3s[x, y] != null) moveBlock(x, y, 0, -1, 3);
				else if (box4s[x, y] != null) moveBlock(x, y, 0, -1, 4);
			}
			if (e.KeyCode == Keys.Down)
			{
				if (canMoveTo(x, y + 1, 0, 1)) y++;
				if (blocks[x, y] != null) moveBlock(x, y, 0, 1, 0);
				else if (box1s[x, y] != null) moveBlock(x, y, 0, 1, 1);
				else if (box2s[x, y] != null) moveBlock(x, y, 0, 1, 2);
				else if (box3s[x, y] != null) moveBlock(x, y, 0, 1, 3);
				else if (box4s[x, y] != null) moveBlock(x, y, 0, 1, 4);
			}
			else if (e.KeyCode == Keys.R) GenerateMap(currentLevel);
			else if (e.KeyCode == Keys.Enter && currentLevel.done == true)
			{
				if(currentLevel == lv1)
				{
					currentLevel = lv2;
				}
				else if(currentLevel == lv2)
				{
					currentLevel = lv3;
				}
				else if (currentLevel == lv3)
				{
					currentLevel = lv4;
				}
				else if (currentLevel == lv4)
				{
					currentLevel = lv5;
				}
				GenerateMap(currentLevel);
			}
			else if (e.KeyCode == Keys.D1)
			{
				if (lv1.played == true)
				{
					currentLevel = lv1;
					GenerateMap(lv1);
				}
			}
			else if (e.KeyCode == Keys.D2)
			{
				if (lv2.played == true)
				{
					currentLevel = lv2;
					GenerateMap(lv2);
				}
			}
			else if (e.KeyCode == Keys.D3)
			{
				if (lv3.played == true)
				{
					currentLevel = lv3;
					GenerateMap(lv3);
				}
			}
			else if (e.KeyCode == Keys.D4)
			{
				if (lv4.played == true)
				{
					currentLevel = lv4;
					GenerateMap(lv4);
				}
			}
			else if (e.KeyCode == Keys.D5)
			{
				if (lv5.played == true)
				{
					currentLevel = lv5;
					GenerateMap(lv5);
				}
			}
			else if(e.KeyCode == Keys.Space)
			{
				currentLevel = lv5;
				GenerateMap(lv5);
			}
			player.TargetX = x * 100;
			player.TargetY = y * 100;
		}

		public void moveBlock(int i, int j, int dx, int dy, int type)
		{
			if (type == 0)
			{
				blocks[i + dx, j + dy] = blocks[i, j];
				blocks[i, j] = null;

				blocks[i + dx, j + dy].TargetX = (i + dx) * 100;
				blocks[i + dx, j + dy].TargetY = (j + dy) * 100;
				if (goals[i + dx, j + dy] != null)
				{
					blocks[i + dx, j + dy].Image = Properties.Resources.final;
					blocks[i + dx, j + dy].correct = true;
					if (Win())
					{
						currentLevel.done = true;
					}
				}
				else
				{
					blocks[i + dx, j + dy].Image = Properties.Resources.box;
					blocks[i + dx, j + dy].correct = false;
				}
			}
			if (type == 1)
			{
				box1s[i + dx, j + dy] = box1s[i, j];
				box1s[i, j] = null;
				box1s[i + dx, j + dy].TargetX = (i + dx) * 100;
				box1s[i + dx, j + dy].TargetY = (j + dy) * 100;
				if (goal1s[i + dx, j + dy] != null)
				{
					box1s[i + dx, j + dy].Image = Properties.Resources.box1final;
					box1s[i + dx, j + dy].correct = true;
					if (Win())
					{
						currentLevel.done = true;
						currentLevel.played = true;
					}
				}
				else
				{
					box1s[i + dx, j + dy].Image = Properties.Resources.box1;
					box1s[i + dx, j + dy].correct = false;
				}
			}
			if (type == 2)
			{
				box2s[i + dx, j + dy] = box2s[i, j];
				box2s[i, j] = null;
				box2s[i + dx, j + dy].TargetX = (i + dx) * 100;
				box2s[i + dx, j + dy].TargetY = (j + dy) * 100;
				if (goal2s[i + dx, j + dy] != null)
				{
					box2s[i + dx, j + dy].Image = Properties.Resources.box2final;
					box2s[i + dx, j + dy].correct = true;
					if (Win())
					{
						currentLevel.done = true;
						currentLevel.played = true;
					}
				}
				else
				{
					box2s[i + dx, j + dy].Image = Properties.Resources.box2;
					box2s[i + dx, j + dy].correct = false;
				}
			}
			if (type == 3)
			{
				box3s[i + dx, j + dy] = box3s[i, j];
				box3s[i, j] = null;
				box3s[i + dx, j + dy].TargetX = (i + dx) * 100;
				box3s[i + dx, j + dy].TargetY = (j + dy) * 100;
				if (goal3s[i + dx, j + dy] != null)
				{
					box3s[i + dx, j + dy].Image = Properties.Resources.box3final;
					box3s[i + dx, j + dy].correct = true;
					if (Win())
					{
						currentLevel.done = true;
						currentLevel.played = true;
					}
				}
				else
				{
					box3s[i + dx, j + dy].Image = Properties.Resources.box3;
					box3s[i + dx, j + dy].correct = false;
				}
			}
			if (type == 4)
			{
				box4s[i + dx, j + dy] = box4s[i, j];
				box4s[i, j] = null;
				box4s[i + dx, j + dy].TargetX = (i + dx) * 100;
				box4s[i + dx, j + dy].TargetY = (j + dy) * 100;
				if (goal4s[i + dx, j + dy] != null)
				{
					box4s[i + dx, j + dy].Image = Properties.Resources.box4final;
					box4s[i + dx, j + dy].correct = true;
					if (Win())
					{
						currentLevel.done = true;
						currentLevel.played = true;
					}
				}
				else
				{
					box4s[i + dx, j + dy].Image = Properties.Resources.box4;
					box4s[i + dx, j + dy].correct = false;
				}
			}
		}

		public Boolean Win()
		{
			Boolean win = true;
			foreach(Sprite i in canvas.children)
			{
				if(i.isDemo)
				{
					if(i.correct == false)
					{
						win = false;
					}
				}
			}
			return win;
		}

		public Boolean canMoveTo(int i, int j, int dx, int dy)
		{

			if (walls[i, j] == null 
			&& blocks[i, j] == null 
			&& box1s[i, j] == null 
			&& box2s[i, j] == null
			&& box3s[i, j] == null
			&& box4s[i, j] == null) return true;
			if (walls[i, j] != null) return false;
			if (blocks[i, j] != null 
			 && blocks[i + dx, j + dy] == null 
			 && walls[i + dx, j + dy] == null) return true;
			if (box1s[i, j] != null
			 && box1s[i + dx, j + dy] == null
			 && walls[i + dx, j + dy] == null) return true;
			if (box2s[i, j] != null
			 && box2s[i + dx, j + dy] == null
			 && walls[i + dx, j + dy] == null) return true;
			if (box3s[i, j] != null
			 && box3s[i + dx, j + dy] == null
			 && walls[i + dx, j + dy] == null) return true;
			if (box4s[i, j] != null
			 && box4s[i + dx, j + dy] == null
			 && walls[i + dx, j + dy] == null) return true;
			return false;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			fixScale();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			fixScale();
		}

		private void fixScale()
		{
			Sokoban.canvas.Scale = Math.Min(ClientSize.Width, ClientSize.Height) / (Math.Max(currentLevel.height,currentLevel.width) * 100.0f);
		}

		private void GenerateMap(Level l)
		{
			fixScale();
			canvas.children.Clear();
			currentLevel.done = false;
			String[] lines = l.lines;
			int width = l.width, height = l.height;
			goals = new StaticSprite[width, height];
			walls = new StaticSprite[width, height];
			floor = new StaticSprite[width, height];
			blocks = new Demo[width, height];
			box1s = new Demo[width, height];
			box2s = new Demo[width, height];
			box3s = new Demo[width, height];
			box4s = new Demo[width, height];
			goal1s = new StaticSprite[width, height];
			goal2s = new StaticSprite[width, height];
			goal3s = new StaticSprite[width, height];
			goal4s = new StaticSprite[width, height];
			for (int j = 0; j < height; j++)
			{
				for (int i = 0; i < width; i++)
				{
					floor[i, j] = new StaticSprite(Properties.Resources.ground_06, i * 100, j * 100);
					canvas.add(floor[i, j]);
					//Normal Goal
					if (lines[j][i] == 'g' || lines[j][i] == 'B')
					{
						goals[i, j] = new StaticSprite(Properties.Resources.goal, i * 100, j * 100);
						Sokoban.canvas.add(goals[i, j]);
					}
					//Wall
					if (lines[j][i] == 'w')
					{
						walls[i, j] = new StaticSprite(Properties.Resources.wall, i * 100, j * 100);
						Sokoban.canvas.add(walls[i, j]);
					}
					//Normal Box
					if (lines[j][i] == 'b' || lines[j][i] == 'B')
					{
						blocks[i, j] = new Demo(Properties.Resources.box, i * 100, j * 100);
						if (lines[j][i] == 'B')
						{
							blocks[i, j].Image = Properties.Resources.final;
							blocks[i, j].correct = true;
						}
					}
					//Character
					if (lines[j][i] == 'c')
					{
						player = new Demo(Properties.Resources.player, i * 100, j * 100);

						x = i;
						y = j;
					}
					//Box 1
					if (lines[j][i] == '[' || lines[j][i] == '|')
					{
						box1s[i, j] = new Demo(Properties.Resources.box1, i * 100, j * 100);
						if (lines[j][i] == '|')
						{
							box1s[i, j].Image = Properties.Resources.box1final;
							box1s[i, j].correct = true;
						}
					}
					//Goal 1
					if (lines[j][i] == ']' || lines[j][i] == '|')
					{
						goal1s[i, j] = new StaticSprite(Properties.Resources.box1goal, i * 100, j * 100);
						if (lines[j][i] == '|') goal1s[i, j].Image = Properties.Resources.box1final;
						Sokoban.canvas.add(goal1s[i, j]);
					}
					//Box 2
					if (lines[j][i] == 'v' || lines[j][i] == 'n')
					{
						box2s[i, j] = new Demo(Properties.Resources.box2, i * 100, j * 100);
						if (lines[j][i] == 'n')
						{
							box2s[i, j].Image = Properties.Resources.box2final;
							box2s[i, j].correct = true;
						}
					}
					//Goal 2
					if (lines[j][i] == 'm' || lines[j][i] == 'n')
					{
						goal2s[i, j] = new StaticSprite(Properties.Resources.box2goal, i * 100, j * 100);
						if (lines[j][i] == 'n') goal2s[i, j].Image = Properties.Resources.box2final;
						Sokoban.canvas.add(goal2s[i, j]);
					}
					//Box 3
					if (lines[j][i] == 't' || lines[j][i] == 'y')
					{
						box3s[i, j] = new Demo(Properties.Resources.box3, i * 100, j * 100);
						if (lines[j][i] == 'y')
						{
							box3s[i, j].Image = Properties.Resources.box3final;
							box3s[i, j].correct = true;
						}
					}
					//Goal 3
					if (lines[j][i] == 'u' || lines[j][i] == 'y')
					{
						goal3s[i, j] = new StaticSprite(Properties.Resources.box3goal, i * 100, j * 100);
						if (lines[j][i] == 'y') goal3s[i, j].Image = Properties.Resources.box3final;
						Sokoban.canvas.add(goal3s[i, j]);
					}
					//Box 4
					if (lines[j][i] == 'i' || lines[j][i] == 'o')
					{
						box4s[i, j] = new Demo(Properties.Resources.box4, i * 100, j * 100);
						if (lines[j][i] == 'o')
						{
							box4s[i, j].Image = Properties.Resources.box4final;
							box4s[i, j].correct = true;
						}
					}
					//Goal 4
					if (lines[j][i] == 'p' || lines[j][i] == 'o')
					{
						goal4s[i, j] = new StaticSprite(Properties.Resources.box4goal, i * 100, j * 100);
						if (lines[j][i] == 'o') goal4s[i, j].Image = Properties.Resources.box4final;
						Sokoban.canvas.add(goal4s[i, j]);
					}
					if (lines[j][i] == 'R')
					{
						goal1s[i, j] = new StaticSprite(Properties.Resources.box1goal, i * 100, j * 100);
						canvas.add(goal1s[i, j]);
						blocks[i, j] = new Demo(Properties.Resources.box, i * 100, j * 100);
					}
					if (lines[j][i] == 'S')
					{
						goals[i, j] = new StaticSprite(Properties.Resources.goal, i * 100, j * 100);
						canvas.add(goals[i, j]);
						box1s[i, j] = new Demo(Properties.Resources.box1, i * 100, j * 100);
					}

				}

			}
			for (int j = 0; j < height; j++)
			{
				for (int i = 0; i < width; i++)
				{
					if (blocks[i, j] != null) Sokoban.canvas.add(blocks[i, j]);
					if (box1s[i, j] != null) Sokoban.canvas.add(box1s[i, j]);
					if (box2s[i, j] != null) Sokoban.canvas.add(box2s[i, j]);
					if (box3s[i, j] != null) Sokoban.canvas.add(box3s[i, j]);
					if (box4s[i, j] != null) Sokoban.canvas.add(box4s[i, j]);
				}
			}
			player.correct = true;
			Sokoban.canvas.add(player);
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			levelHelper();
			Level l = lv1;
			canvas.children.Clear();
			currentLevel.done = false;
			String[] lines = l.lines;
			int width = l.width, height = l.height;
			goals = new StaticSprite[width, height];
			walls = new StaticSprite[width, height];
			floor = new StaticSprite[width, height];
			blocks = new Demo[width, height];
			box1s = new Demo[width, height];
			box2s = new Demo[width, height];
			box3s = new Demo[width, height];
			box4s = new Demo[width, height];
			goal1s = new StaticSprite[width, height];
			goal2s = new StaticSprite[width, height];
			goal3s = new StaticSprite[width, height];
			goal4s = new StaticSprite[width, height];
			for (int j = 0; j < height; j++)
			{
				for (int i = 0; i < width; i++)
				{
					floor[i, j] = new StaticSprite(Properties.Resources.ground_06, i * 100, j * 100);
					canvas.add(floor[i, j]);
					//Normal Goal
					if (lines[j][i] == 'g' || lines[j][i] == 'B')
					{
						goals[i, j] = new StaticSprite(Properties.Resources.goal, i * 100, j * 100);
						Sokoban.canvas.add(goals[i, j]);
					}
					//Wall
					if (lines[j][i] == 'w')
					{
						walls[i, j] = new StaticSprite(Properties.Resources.wall, i * 100, j * 100);
						Sokoban.canvas.add(walls[i, j]);
					}
					//Normal Box
					if (lines[j][i] == 'b' || lines[j][i] == 'B')
					{
						blocks[i, j] = new Demo(Properties.Resources.box, i * 100, j * 100);
						if (lines[j][i] == 'B')
						{
							blocks[i, j].Image = Properties.Resources.final;
							blocks[i, j].correct = true;
						}
					}
					//Character
					if (lines[j][i] == 'c')
					{
						player = new Demo(Properties.Resources.player, i * 100, j * 100);

						x = i;
						y = j;
					}
					//Box 1
					if (lines[j][i] == '[' || lines[j][i] == '|')
					{
						box1s[i, j] = new Demo(Properties.Resources.box1, i * 100, j * 100);
						if (lines[j][i] == '|')
						{
							box1s[i, j].Image = Properties.Resources.box1final;
							box1s[i, j].correct = true;
						}
					}
					//Goal 1
					if (lines[j][i] == ']' || lines[j][i] == '|')
					{
						goal1s[i, j] = new StaticSprite(Properties.Resources.box1goal, i * 100, j * 100);
						if (lines[j][i] == '|') goal1s[i, j].Image = Properties.Resources.box1final;
						Sokoban.canvas.add(goal1s[i, j]);
					}
					//Box 2
					if (lines[j][i] == 'v' || lines[j][i] == 'n')
					{
						box2s[i, j] = new Demo(Properties.Resources.box2, i * 100, j * 100);
						if (lines[j][i] == 'n')
						{
							box2s[i, j].Image = Properties.Resources.box2final;
							box2s[i, j].correct = true;
						}
					}
					//Goal 2
					if (lines[j][i] == 'm' || lines[j][i] == 'n')
					{
						goal2s[i, j] = new StaticSprite(Properties.Resources.box2goal, i * 100, j * 100);
						if (lines[j][i] == 'n') goal2s[i, j].Image = Properties.Resources.box2final;
						Sokoban.canvas.add(goal2s[i, j]);
					}
					//Box 3
					if (lines[j][i] == 't' || lines[j][i] == 'y')
					{
						box3s[i, j] = new Demo(Properties.Resources.box3, i * 100, j * 100);
						if (lines[j][i] == 'y')
						{
							box3s[i, j].Image = Properties.Resources.box3final;
							box3s[i, j].correct = true;
						}
					}
					//Goal 3
					if (lines[j][i] == 'u' || lines[j][i] == 'y')
					{
						goal3s[i, j] = new StaticSprite(Properties.Resources.box3goal, i * 100, j * 100);
						if (lines[j][i] == 'y') goal3s[i, j].Image = Properties.Resources.box3final;
						Sokoban.canvas.add(goal3s[i, j]);
					}
					//Box 4
					if (lines[j][i] == 'i' || lines[j][i] == 'o')
					{
						box4s[i, j] = new Demo(Properties.Resources.box4, i * 100, j * 100);
						if (lines[j][i] == 'o')
						{
							box4s[i, j].Image = Properties.Resources.box4final;
							box4s[i, j].correct = true;
						}
					}
					//Goal 4
					if (lines[j][i] == 'p' || lines[j][i] == 'o')
					{
						goal4s[i, j] = new StaticSprite(Properties.Resources.box4goal, i * 100, j * 100);
						if (lines[j][i] == 'o') goal4s[i, j].Image = Properties.Resources.box4final;
						Sokoban.canvas.add(goal4s[i, j]);
					}
					if (lines[j][i] == 'R')
					{
						goal1s[i, j] = new StaticSprite(Properties.Resources.box1goal, i * 100, j * 100);
						canvas.add(goal1s[i, j]);
						blocks[i, j] = new Demo(Properties.Resources.box, i * 100, j * 100);
					}
					if (lines[j][i] == 'S')
					{
						goals[i, j] = new StaticSprite(Properties.Resources.goal, i * 100, j * 100);
						canvas.add(goals[i, j]);
						box1s[i, j] = new Demo(Properties.Resources.box1, i * 100, j * 100);
					}

				}

			}
			for (int j = 0; j < height; j++)
			{
				for (int i = 0; i < width; i++)
				{
					if (blocks[i, j] != null) Sokoban.canvas.add(blocks[i, j]);
					if (box1s[i, j] != null) Sokoban.canvas.add(box1s[i, j]);
					if (box2s[i, j] != null) Sokoban.canvas.add(box2s[i, j]);
					if (box3s[i, j] != null) Sokoban.canvas.add(box3s[i, j]);
					if (box4s[i, j] != null) Sokoban.canvas.add(box4s[i, j]);
				}
			}
			player.correct = true;
			Sokoban.canvas.add(player);
			Application.Run(new Program());
		}
	}
	public class Level
	{
		public int width, height;
		public String map;
		public String[] lines;
		public Boolean done = false, played = false;
		public Level(int w, int h, String m)
		{
			width = w;
			height = h;
			map = m;
			lines = map.Split('\n');
		}
	}
}