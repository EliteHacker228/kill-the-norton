using System.Drawing;

namespace Kill_the_Norton.Entities
{
    public class Enemy
    {
        public Point Cooridantes { get; set; }

        public Enemy(Point cooridantes)
        {
            Cooridantes = cooridantes;
        }
    }
}