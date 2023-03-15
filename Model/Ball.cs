using System.Drawing;

namespace Pong_WinForms.Model
{
    public class Ball
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int SpeedX { get; set; }
        public int SpeedY { get; set; }
        public int Size { get; set; }
        public Brush Brush { get; set; }

        public Ball(int x, int y, int speedX, int speedY, int size, Brush brush)
        {
            X = x;
            Y = y;
            SpeedX = speedX;
            SpeedY = speedY;
            Size = size;
            Brush = brush;
        }

        public void Move()
        {
            X += SpeedX;
            Y += SpeedY;
        }

        public RectangleF GetBounds()
        {
            return new RectangleF(X, Y, Size, Size);
        }
    }
}
