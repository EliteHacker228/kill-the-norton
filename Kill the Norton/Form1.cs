using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kill_the_Norton
{
    public partial class Form1 : Form
    {
        private int playerSpeed = 5;
        private bool goLeft, goRight, goForward, goBackward;
        private Image img;
        private float angle = 0;
        private Point playerCooridantes;
        private int[,] map;
        private const int mapWidth = 32;
        private const int mapHeight = 32;
        private int sideOfMapObject = 64;

        private Point delta = new Point(0, 0);


        Image asphaltImage = Image.FromFile("C:\\Users\\Max\\Desktop\\asphaltHR.png");
        Image wallImage = Image.FromFile("C:\\Users\\Max\\Desktop\\wallblockHR.png");

        public Form1()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
            label1.Hide();
            label2.Hide();
            pictureBox1.Hide();

            DoubleBuffered = true;

            timer1.Interval = 10;
            timer1.Tick += update;
            timer1.Start();

            KeyDown += keyDown;
            KeyUp += keyUp;

            map = new int[mapWidth, mapHeight]
            {
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 2, 2, 2, 1, 2, 2, 1, 1, 2, 1, 1, 2, 1, 2, 2, 1, 1, 2, 2, 2, 1, 2, 2, 2, 1, 2, 1, 1, 1, 1},
                {1, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 2, 1, 2, 1, 2, 1, 2, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1},
                {1, 1, 2, 1, 2, 1, 2, 2, 1, 1, 2, 2, 1, 2, 1, 2, 2, 1, 1, 2, 2, 2, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1},
                {1, 1, 2, 1, 2, 1, 2, 1, 1, 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 2, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 2, 1, 2, 1, 2, 1, 1, 1, 2, 1, 1, 2, 1, 2, 2, 1, 1, 2, 2, 2, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            };

            //CreateMap();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            img = Image.FromFile("C:\\Users\\Max\\Desktop\\turbokiller.png");
        }

        /*private void CreateMap()
        {
            for (var x = 0; x < mapWidth; x++)
            {
                for (var y = 0; y < mapHeight; y++)
                {
                    if (map[x, y] == 1)
                    {
                        gameGraphics.DrawImage(asphaltImage, x * 64, y * 64,
                            new Rectangle(new Point(0, 0), new Size(64, 64)), GraphicsUnit.Pixel);
                    }
                }
            }
        }*/

        private double GetAngle(Point p1, Point p2)
        {
            float xDiff = p2.X - p1.X;
            float yDiff = p2.Y - p1.Y;
            return Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            angle = (float) GetAngle(playerCooridantes, new Point(e.X, e.Y));
            label1.Text = "Координаты мыши: " + e.X + " " + e.Y + ", Угол: " + " " + angle + "\n" +
                          "Координацты игрока: " + playerCooridantes.X + " " + playerCooridantes.Y + "\n";
            //"Столкновение: " + IsCollided();

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics gameGraphic = e.Graphics;
            CreateMap(gameGraphic);

            Bitmap bitmap = new Bitmap(img.Width, img.Height);
            Graphics g = Graphics.FromImage(bitmap);
            g.TranslateTransform(bitmap.Width / 2, bitmap.Height / 2);
            g.RotateTransform(angle);
            g.TranslateTransform(-bitmap.Width / 2, -bitmap.Height / 2);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(img, 0, 0);
            e.Graphics.TranslateTransform(playerCooridantes.X, playerCooridantes.Y);
            e.Graphics.DrawImage(bitmap, -bitmap.Width / 2, -bitmap.Height / 2);
        }

        private void CreateMap(Graphics gameGraphic)
        {
            for (var x = 0; x < mapWidth; x++)
            {
                for (var y = 0; y < mapHeight; y++)
                {
                    if (map[x, y] == 1)
                    {
                        gameGraphic.DrawImage(asphaltImage, y * sideOfMapObject - delta.X,
                            x * sideOfMapObject - delta.Y,
                            new Rectangle(new Point(0, 0), new Size(sideOfMapObject, sideOfMapObject)),
                            GraphicsUnit.Pixel);
                    }

                    if (map[x, y] == 2)
                    {
                        gameGraphic.DrawImage(wallImage, y * sideOfMapObject - delta.X, x * sideOfMapObject - delta.Y,
                            new Rectangle(new Point(0, 0), new Size(sideOfMapObject, sideOfMapObject)),
                            GraphicsUnit.Pixel);
                    }
                }
            }
        }

        /*private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Bitmap bitmap = new Bitmap(img.Width, img.Height);
            Graphics g = Graphics.FromImage(bitmap);
            g.TranslateTransform(bitmap.Width / 2, bitmap.Height / 2);
            g.RotateTransform(angle);
            g.TranslateTransform(-bitmap.Width / 2, -bitmap.Height / 2);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(img, 0, 0);
            e.Graphics.TranslateTransform(playerCooridantes.X, playerCooridantes.Y);
            e.Graphics.DrawImage(bitmap, -bitmap.Width / 2, -bitmap.Height / 2);
            
            //CreateMap();
        }*/

        private void keyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                goForward = false;
            }

            if (e.KeyCode == Keys.S)
            {
                goBackward = false;
            }

            if (e.KeyCode == Keys.D)
            {
                goRight = false;
            }

            if (e.KeyCode == Keys.A)
            {
                goLeft = false;
            }
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                goForward = true;
            }

            if (e.KeyCode == Keys.S)
            {
                goBackward = true;
            }

            if (e.KeyCode == Keys.D)
            {
                goRight = true;
            }

            if (e.KeyCode == Keys.A)
            {
                goLeft = true;
            }
        }

        private void update(object sender, EventArgs e)
        {
            if (goLeft)
            {
                if (!IsCollided(-playerSpeed, 0))
                {
                    playerCooridantes.X -= playerSpeed;

                    if (playerCooridantes.X > 55 && playerCooridantes.X < sideOfMapObject * mapWidth)
                        delta.X -= playerSpeed;
                }

                Invalidate();
            }

            if (goRight)
            {
                if (!IsCollided(+playerSpeed, 0))
                {
                    playerCooridantes.X += playerSpeed;

                    if (playerCooridantes.X > sideOfMapObject && playerCooridantes.X < sideOfMapObject * mapWidth)
                        delta.X += playerSpeed;
                    Invalidate();
                }
            }

            if (goBackward)
            {
                if (!IsCollided(0, +playerSpeed))
                {
                    playerCooridantes.Y += playerSpeed;

                    if (playerCooridantes.Y > sideOfMapObject && playerCooridantes.Y < sideOfMapObject * mapHeight)
                        delta.Y += playerSpeed;
                    Invalidate();
                }
            }

            if (goForward)
            {
                if (!IsCollided(0, -playerSpeed))
                {
                    playerCooridantes.Y -= playerSpeed;

                    if (playerCooridantes.Y > 55 && playerCooridantes.Y < sideOfMapObject * mapHeight)
                        delta.Y -= playerSpeed;
                }

                Invalidate();
            }

            Cursor.Position = MousePosition;
        }

        private bool IsCollided(int dx, int dy)
        {
            var playerX = (playerCooridantes.X + dx);
            var playerY = (playerCooridantes.Y + dy);

            var processedX = (playerX + delta.X) / 64;
            var processedY = (playerY + delta.Y) / 64;

            label2.Text = "PlayerX: " + playerX + "\n" +
                          "PlayerY: " + playerY + "\n" +
                          "DeltaX: " + delta.X + "\n" +
                          "DeltaY: " + delta.Y + "\n" +
                          "ProcessedX: " + processedX + "\n" +
                          "ProcessedY: " + processedY + "\n";

            if (map[processedY, processedX] == 2)
                return true;
            return false;
        }
    }
}