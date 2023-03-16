using Pong_WinForms.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pong_WinForms
{
    public partial class Pong : Form
    {
        private const int FormWidth = 800;
        private const int FormHeight = 600;

        private Timer _timer;
        private Ball _ball;
        private Paddle _player1;
        private Paddle _player2;
        private Settings _settings;

        private int _player1Score;
        private int _player2Score;

        private bool _gameOver;

        public Pong()
        {
            InitializeComponent();

            _settings = new Settings();

            // Create ball
            var ballSize = 50;
            var ballX = FormWidth / 2 - ballSize / 2;
            var ballY = FormHeight / 2 - ballSize / 2;
            var ballSpeedX = _settings.BallSpeed;
            var ballSpeedY = _settings.BallSpeed;
            var ballBrush = Brushes.WhiteSmoke;
            _ball = new Ball(ballX, ballY, ballSize, ballSpeedX, ballSpeedY, ballBrush);
            _ball.Size = ballSize;

            // Create players
            var paddleWidth = 10;
            var paddleHeight = 80;
            var paddleSpeed = _settings.Player1Speed;
            var player1X = 20;
            var player1Y = FormHeight / 2 - paddleHeight / 2;
            var player1Brush = Brushes.Red;
            _player1 = new Paddle(player1X, player1Y, paddleWidth, paddleHeight, paddleSpeed, player1Brush);

            paddleSpeed = _settings.Player2Speed;
            var player2X = FormWidth - paddleWidth - 20;
            var player2Y = FormHeight / 2 - paddleHeight / 2;
            var player2Brush = Brushes.Blue;
            _player2 = new Paddle(player2X, player2Y, paddleWidth, paddleHeight, paddleSpeed, player2Brush);

            this.Paint += Pong_Paint;
            this.KeyDown += this.Pong_KeyDown;
            this.KeyUp += this.Pong_KeyUp;

            // Create timer
            _timer = new Timer();
            _timer.Interval = 10;
            _timer.Tick += Timer_Tick;
            _timer.Start();

            // Set form properties
            DoubleBuffered = true;
            ClientSize = new Size(FormWidth, FormHeight);
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void Pong_Paint(object sender, PaintEventArgs e)
        {
            // Draw ball
            e.Graphics.FillEllipse(_ball.Brush, _ball.GetBounds());

            // Draw paddles
            e.Graphics.FillRectangle(_player1.Brush, _player1.GetBounds());
            e.Graphics.FillRectangle(_player2.Brush, _player2.GetBounds());

            // Draw scores
            var scoreFont = new Font("Arial", 30);
            var scoreBrush = Brushes.White;
            var player1ScorePosition = new PointF(FormWidth / 2 - 50, 20);
            var player2ScorePosition = new PointF(FormWidth / 2 + 25, 20);
            e.Graphics.DrawString(_player1Score.ToString(), scoreFont, scoreBrush, player1ScorePosition);
            e.Graphics.DrawString(_player2Score.ToString(), scoreFont, scoreBrush, player2ScorePosition);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Move ball
            _ball.Move();

            // Check for collision with paddles
            if (_player1.GetBounds().IntersectsWith(_ball.GetBounds()))
            {
                _ball.SpeedX = Math.Abs(_ball.SpeedX);
            }
            else if (_player2.GetBounds().IntersectsWith(_ball.GetBounds()))
            {
                _ball.SpeedX = -Math.Abs(_ball.SpeedX);
            }

            // Check for collision with top/bottom walls
            // ball hits top wall
            if (_ball.Y < 0)
            {
                _ball.SpeedY = Math.Abs(_ball.SpeedY);
            }
            // ball hits bottom wall
            else if (_ball.Y > FormHeight - _ball.Size)
            {
                _ball.SpeedY = -Math.Abs(_ball.SpeedY);
            }

            // Check for point scored
            if (_ball.X < 0)
            {
                _player2Score++;
                Reset();
            }
            else if (_ball.X > FormWidth - _ball.Size)
            {
                _player1Score++;
                Reset();
            }

            // Move paddles
            if (_settings.Player1UpKeyDown && _player1.Y > 0)
            {
                _player1.MoveUp();
            }
            if (_settings.Player1DownKeyDown && _player1.Y < FormHeight - _player1.Height)
            {
                _player1.MoveDown();
            }
            if (_settings.Player2UpKeyDown && _player2.Y > 0)
            {
                _player2.MoveUp();
            }
            if (_settings.Player2DownKeyDown && _player2.Y < FormHeight - _player2.Height)
            {
                _player2.MoveDown();
            }

            // Redraw form
            Invalidate();

            // Check for game over
            if (_player1Score == _settings.WinScore || _player2Score == _settings.WinScore)
            {
                _gameOver = true;
                _timer.Stop();
                MessageBox.Show("Game Over!");
            }
        }

        private void Reset()
        {
            _ball.X = FormWidth / 2 - _ball.Size / 2;
            _ball.Y = FormHeight / 2 - _ball.Size / 2;
            _ball.SpeedX = _settings.BallSpeed;
            _ball.SpeedY = _settings.BallSpeed;
        }

        private void Pong_KeyDown(object sender, KeyEventArgs e)
        {
            // Update settings based on key press
            switch (e.KeyCode)
            {
                case Keys.W:
                    _settings.Player1UpKeyDown = true;
                    break;
                case Keys.S:
                    _settings.Player1DownKeyDown = true;
                    break;
                case Keys.Up:
                    _settings.Player2UpKeyDown = true;
                    break;
                case Keys.Down:
                    _settings.Player2DownKeyDown = true;
                    break;
            }
        }

        private void Pong_KeyUp(object sender, KeyEventArgs e)
        {
            // Update settings based on key release
            switch (e.KeyCode)
            {
                case Keys.W:
                    _settings.Player1UpKeyDown = false;
                    break;
                case Keys.S:
                    _settings.Player1DownKeyDown = false;
                    break;
                case Keys.Up:
                    _settings.Player2UpKeyDown = false;
                    break;
                case Keys.Down:
                    _settings.Player2DownKeyDown = false;
                    break;
            }
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            // Show settings form
            var settingsForm = new SettingsForm(_settings.BallSpeed, _settings.Player1Speed);
            settingsForm.ShowDialog();
        }
    }
}