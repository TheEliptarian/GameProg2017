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
        }

        private void Form1_Load(Object sender, EventArgs e)
        { }

        protected override void OnKeyDown(KeyEventArgs e)
        {
        }

        private void UpdateSize()
        {

        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
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
        }

    }
}
