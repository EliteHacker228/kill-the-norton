namespace Kill_the_Norton.Entities
{
    public class Level
    {
        public int[,] Map { get; set; }
        public int MapWidth { get; } = 32;
        public int MapHeight { get; } = 32;
        public int SideOfMapObject { get; set; } = 64;
    }
}