using System.Drawing;
using System.Windows.Forms;

namespace Kill_the_Norton.Entities
{
    public class TimeMachine
    {
        public ProgressBar ProgressBar { get; set; }

        private int _reapedSoulsLimit = 9;
        private int _reapedSouls;

        public int ReapedSouls
        {
            get { return _reapedSouls; }
            set
            {
                if(value > _reapedSoulsLimit)
                    return;
                ProgressBar.Value = value;
                _reapedSouls = value;
                if (_reapedSouls == 3)
                {
                    ProgressBar.ForeColor = Color.Red;
                }
                else if (_reapedSouls == 6)
                {
                    ProgressBar.ForeColor = Color.Yellow;
                }
                else if (_reapedSouls == 9)
                {
                    ProgressBar.ForeColor = Color.Green;
                }
            }
        }

        public void StopTime()
        {
        }
    }
}