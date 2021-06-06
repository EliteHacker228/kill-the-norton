using System;
using System.Drawing;
using System.IO;

namespace Kill_the_Norton.Entities
{
    public class Resources
    {
        public static readonly String Path = Directory.GetCurrentDirectory();
        public static readonly Image PlayerSprite = Image.FromFile(Path + "\\turbokillerTR.png");
        public static readonly Image AsphaltSprite = Image.FromFile(Path + "\\asphaltHR.png");
        public static readonly Image WallSprite = Image.FromFile(Path + "\\wallblock.png");
        public static readonly Image CarpetSprite = Image.FromFile(Path + "\\carpet.png");
        public static readonly Image FloorSprite1 = Image.FromFile(Path + "\\floorblock.png");
        public static readonly Image EnemySprite = Image.FromFile(Path + "\\enemyTR.png");
        public static readonly Image TransparentSprite = Image.FromFile(Path + "\\transparent.png");
    }
}