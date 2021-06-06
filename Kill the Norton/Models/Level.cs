namespace Kill_the_Norton.Entities
{
    public class Level
    {
        public int[,] Map { get; set; }
        public int MapWidth { get; set; }
        public int MapHeight { get; set; }
        public int SideOfMapObject { get; set; } = 64;
    }
}