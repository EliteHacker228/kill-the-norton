using System.Drawing;

namespace Kill_the_Norton.Entities
{
    public class Player
    {
        public int Speed { get; set; } = 3;
        public static bool IsInvincible { get; set; } = false;
        public Point Cooridantes { get; set; } = new Point(0, 0);
        public Point Delta { get; set; } = new Point(0, 0);

        public bool IsAlive { get; set; } = true;

        public bool GoLeft { get; set; }
        public bool GoRight { get; set; }
        public bool GoForward { get; set; }
        public bool GoBackward { get; set; }
        public Image Sprite { get; set; }
        public float Angle { get; set; }
    }
}