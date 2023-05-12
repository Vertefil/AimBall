namespace Lect_4
{
    public partial class Form1 : Form
    {
        private Ball b;
        //Двойная буфферизация
        private BufferedGraphics bg;
        public Form1()
        {
            InitializeComponent();
            b = new Ball(panel1.Size);
            bg = BufferedGraphicsManager.Current.Allocate(panel1.CreateGraphics(), new Rectangle(new Point(0, 0), panel1.Size));
        }

        private void PaintScene()
        {
            bg.Graphics.Clear(Color.White);
            b.Paint(bg.Graphics);
            bg.Render();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            PaintScene();
            if (b.Move()) timer1.Stop();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                b.Target = e.Location;
                timer1.Start();
            }
        }

        private PointF? mousePos = null;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right
                && b.Contains(e.Location)
                )
            {
                timer1.Stop();
                mousePos = e.Location;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mousePos = null;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousePos != null)
            {
                var currPos = e.Location;
                var shift = new SizeF(currPos.X - (mousePos?.X ?? 0f), currPos.Y - (mousePos?.Y ?? 0f));
                b.MoveTo(new PointF(b.Position.X + shift.Width, b.Position.Y + shift.Height));
                PaintScene();
                mousePos = currPos;
            }
        }
    }
}