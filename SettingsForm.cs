using System;
using System.Windows.Forms;

namespace Pong_WinForms
{
    public partial class SettingsForm : Form
    {
        private Label ballSpeedLabel = new Label { Text = "Ball speed:" };
        private NumericUpDown ballSpeedNumeric = new NumericUpDown { Minimum = 1, Maximum = 20, Value = 5 };
        private Label paddleSpeedLabel = new Label { Text = "Paddle speed:" };
        private NumericUpDown paddleSpeedNumeric = new NumericUpDown { Minimum = 1, Maximum = 20, Value = 10 };
        private Button saveButton = new Button { Text = "Save" };
        private Button cancelButton = new Button { Text = "Cancel" };
        private bool saved = false;

        private Settings settings = new Settings();

        public SettingsForm(int ballSpeed, int paddleSpeed)
        {
            Width = 300;
            Height = 150;
            Text = "Settings";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            ShowInTaskbar = false;
            ControlBox = false;
            ballSpeedNumeric.Value = ballSpeed;
            paddleSpeedNumeric.Value = paddleSpeed;
            saveButton.Click += SaveButton_Click;
            cancelButton.Click += CancelButton_Click;
            Controls.Add(ballSpeedLabel);
            Controls.Add(ballSpeedNumeric);
            Controls.Add(paddleSpeedLabel);
            Controls.Add(paddleSpeedNumeric);
            Controls.Add(saveButton);
            Controls.Add(cancelButton);
            AcceptButton = saveButton;
            CancelButton = cancelButton;
        }

        public int BallSpeed
        {
            get { return (int)ballSpeedNumeric.Value; }
        }

        public int PaddleSpeed
        {
            get { return (int)paddleSpeedNumeric.Value; }
        }

        public bool Saved
        {
            get { return saved; }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            settings.BallSpeed = this.BallSpeed;
            //settings.Player1Speed = 

            settings.SaveSettings();
            saved = true;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
