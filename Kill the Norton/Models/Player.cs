using System.Drawing;

namespace Kill_the_Norton.Entities
{
    public class Player
    {
        public int Speed { get; set; } = 5;
        public Point Cooridantes { get; set; } = new Point(0, 0);
        public Point Delta { get; set; } = new Point(0, 0);

        public bool GoLeft { get; set; }
        public bool GoRight { get; set; }
        public bool GoForward { get; set; }
        public bool GoBackward { get; set; }
        public Image Sprite { get; set; }
        public float Angle { get; set; }
    }
}