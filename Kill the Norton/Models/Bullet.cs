using System.Drawing;

namespace Kill_the_Norton.Entities
{
    public class Bullet
    {
        public Point Target { get; set; }
        public PointF Coordinates { get; set; }
        public PointF SpeedDelta { get; set; }
        public PointF Delta { get; set; }
        public int Speed { get; set; }
    }
}