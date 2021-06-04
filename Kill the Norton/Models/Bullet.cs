using System.Drawing;

namespace Kill_the_Norton.Entities
{
    public enum Sender
    {
        Player,
        Enemy
    }

    public class Bullet
    {
        public Point Target { get; set; }

        //public PointF RenderCoordinates { get; set; }
        public Sender Sender { get; }
        public PointF OwnCoordinates { get; set; }
        public PointF SpeedDelta { get; set; }
        public PointF Delta { get; set; }
        public int Speed { get; set; } = 24;

        public Bullet()
        {
        }
        public Bullet(Sender sender)
        {
            Sender = sender;
        }
    }
}