using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Kill_the_Norton.Entities
{
    public class TimeMachine
    {
        public ProgressBar ProgressBar { get; set; }

        public double SlowingProportion { get; private set; } = 1;
        public bool IsTimeStopped { get; private set; } = false;

        private int _reapedSoulsLimit = 9;
        private int _reapedSouls;

        private long lastStoppingTimestamp = 0;

        public int ReapedSouls
        {
            get { return _reapedSouls; }
            set
            {
                if (value > _reapedSoulsLimit)
                    return;

                ProgressBar.Value = value;
                _reapedSouls = value;
                UpdateProgressBarColor();
            }
        }

        public void Check()
        {
            if (((DateTimeOffset) DateTime.Now).ToUnixTimeMilliseconds() - lastStoppingTimestamp >= 4000)
            {
                Enemy.Speed = Enemy.SpeedStandard;
                Bullet.Speed = Bullet.SpeedStandard;
                IsTimeStopped = false;
                if (Player.IsInvincible)
                    Player.IsInvincible = false;
                SlowingProportion = 1;
            }

            //return ((DateTimeOffset) DateTime.Now).ToUnixTimeMilliseconds() - lastStoppingTimestamp;
        }

        public void StopTime(Game game, List<Enemy> enemies, List<Bullet> bullets)
        {
            if (_reapedSouls < 3)
            {
                return;
            }

            if (_reapedSouls >= 3 && _reapedSouls < 6)
            {
                _reapedSouls -= 3;
                SlowIt(0.3);
            }
            else if (_reapedSouls >= 6 && _reapedSouls < 9)
            {
                _reapedSouls -= 6;
                SlowIt(0.6);
            }
            else if (_reapedSouls == 9)
            {
                _reapedSouls -= 9;
                SlowIt(1);
                IsTimeStopped = true;
                Player.IsInvincible = true;
            }

            UpdateProgressBarColor();

            lastStoppingTimestamp = ((DateTimeOffset) DateTime.Now).ToUnixTimeMilliseconds();
            ProgressBar.Value = _reapedSouls;
        }

        private void UpdateProgressBarColor()
        {
            if (_reapedSouls < 3)
            {
                ProgressBar.ForeColor = Color.Black;
            }
            else if (_reapedSouls >= 3 && _reapedSouls < 6)
            {
                ProgressBar.ForeColor = Color.Red;
            }
            else if (_reapedSouls >= 6 && _reapedSouls < 9)
            {
                ProgressBar.ForeColor = Color.Yellow;
            }
            else if (_reapedSouls == 9)
            {
                ProgressBar.ForeColor = Color.Green;
            }
        }

        private void SlowIt(double proportion)
        {
            Enemy.Speed -= (int) (Enemy.Speed * proportion);
            Bullet.Speed -= (int) (Bullet.Speed * proportion);
            SlowingProportion = proportion;
        }
    }
}