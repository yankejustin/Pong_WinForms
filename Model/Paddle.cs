using System.Drawing;

namespace Pong_WinForms.Model
{
    public class Paddle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Speed { get; set; }
        public Brush Brush { get; set; }

        public Paddle(int x, int y, int width, int height, int speed, Brush brush)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Speed = speed;
            Brush = brush;
        }

        public void MoveUp()
        {
            Y -= Speed;
        }

        public void MoveDown()
        {
            Y += Speed;
        }

        public RectangleF GetBounds()
        {
            return new RectangleF(X, Y, Width, Height);
        }
    }
}
