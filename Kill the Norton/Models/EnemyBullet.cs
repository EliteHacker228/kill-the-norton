using System.Drawing;

namespace Kill_the_Norton.Entities
{
    public class EnemyBullet
    {
        public Point Target { get; set; }
        //public PointF RenderCoordinates { get; set; }
        public PointF OwnCoordinates { get; set; }
        public PointF SpeedDelta { get; set; }
        public PointF Delta { get; set; }
        public int Speed { get; set; } = 24;
    }
}