using Newtonsoft.Json;
using System;
using System.IO;

namespace Pong_WinForms
{
    public class Settings
    {
        public int Player1Speed { get; set; }
        public int Player2Speed { get; set; }

        public bool Player1UpKeyDown = false;
        public bool Player2UpKeyDown = false;

        public bool Player1DownKeyDown = false;
        public bool Player2DownKeyDown = false;

        public int BallSpeed { get; set; }
        public int WinScore { get; set; }

        private const string SettingsPath = "settings.json";

        public Settings()
        {
            LoadSettings();
        }

        public void LoadSettings()
        {
            if (File.Exists(SettingsPath))
            {
                try
                {
                    var jsonString = File.ReadAllText(SettingsPath);
                    var settings = JsonConvert.DeserializeObject<Settings>(jsonString);

                    Player1Speed = settings.Player1Speed;
                    Player2Speed = settings.Player2Speed;
                    BallSpeed = settings.BallSpeed;
                    WinScore = settings.WinScore;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed to load settings: {e.Message}");
                }
            }
            else
            {
                Player1Speed = 7;
                Player2Speed = 7;
                BallSpeed = 5;
                WinScore = 10;
            }
        }

        public void SaveSettings()
        {
            try
            {
                var jsonString = JsonConvert.SerializeObject(this);
                File.WriteAllText(SettingsPath, jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to save settings: {e.Message}");
            }
        }

    }
}
