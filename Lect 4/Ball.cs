namespace Lect_4
{

    public class Ball
    {
        private Random rnd = new Random();
        private Size _containerSize;
        public int Size { get; set; }
        private RectangleF ballRect => new RectangleF(Position, new SizeF(Size, Size));
        public Color Color { get; set; }
        public PointF Position { get; set; }
        private SizeF Shift { get; set; } = new SizeF(1f, 1f);
        private PointF _target = new PointF(0f, 0f);
        private int _steps;
        public PointF Target
        {
            get => _target;
            set
            {
                _target.X = Math.Min(
                    Math.Max(0f, value.X - Size / 2f),
                    _containerSize.Width - Size);
                _target.Y = Math.Min(
                    Math.Max(0f, value.Y - Size / 2f),
                    _containerSize.Height - Size);
                Shift = new SizeF(_target.X - Position.X, _target.Y - Position.Y);
                _steps = (int)Math.Max(Math.Abs(Shift.Width), Math.Abs(Shift.Height));
            }
        }
        public Ball(Size containerSize)
        {
            Size = rnd.Next(30, 100);
            Color = Color.FromArgb(rnd.Next(250), rnd.Next(250), rnd.Next(250));
            _containerSize = containerSize;
            Position = new PointF(
                rnd.Next(_containerSize.Width - Size),
                rnd.Next(_containerSize.Height - Size)
                );
        }

        public void Paint(Graphics g)
        {
            var b = new SolidBrush(Color);
            g.FillEllipse(b, ballRect);
        }

        public bool Move()
        {
            Position = new(
                Position.X + Shift.Width / _steps,
                Position.Y + Shift.Height / _steps
                );
            return Math.Abs(Position.X - Target.X) < 1.5 * Math.Abs(Shift.Width) / _steps &&
                Math.Abs(Position.Y - Target.Y) < 1.5 * Math.Abs(Shift.Height) / _steps;
        }

        public bool Contains(PointF point)
        {
            var center = new PointF(Position.X + Size / 2f, Position.Y + Size / 2f);
            return Math.Pow(center.X - point.X, 2) + Math.Pow(center.Y - point.Y, 2) <= Math.Pow(Size,2) / 2f;
        }

        public void MoveTo(PointF point)
        {
            Position = point;
        }
    }
}
