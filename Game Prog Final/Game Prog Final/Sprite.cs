using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System;

namespace td
{
	public class Sprite
	{
		private Sprite parent = null;

		public Sprite()
		{

		}

		//instance variable
		private float x = 0;

		public float X
		{
			get { return x; }
			set { x = value; }
		}

		private float y = 0;

		public float Y
		{
			get { return y; }
			set { y = value; }
		}

		private float scale = 1;

		public float Scale
		{
			get { return scale; }
			set { scale = value; }
		}

		private float rotation = 0;

		/// <summary>
		/// The rotation in degrees.
		/// </summary>
		public float Rotation
		{
			get { return rotation; }
			set { rotation = value; }
		}


		public List<Sprite> children = new List<Sprite>();


		public void Kill()
		{
			parent.children.Remove(this);
		}

		//methods
		public void render(Graphics g)
		{
			Matrix original = g.Transform.Clone();
			g.TranslateTransform(x, y);
			g.ScaleTransform(scale, scale);
			g.RotateTransform(rotation);
			paint(g);
			foreach (Sprite s in children)
			{
				s.render(g);
			}
			g.Transform = original;
		}

		public virtual void paint(Graphics g)
		{

		}

		public virtual void Act()
		{
		}

		public void add(Sprite s)
		{
			s.parent = this;
			children.Add(s);
		}
		public void update()
		{
			Act();
			foreach (Sprite s in children)
			{
				s.update();
			}
		}
	}
}