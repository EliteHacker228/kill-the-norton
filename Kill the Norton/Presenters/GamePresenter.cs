#nullable enable
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Kill_the_Norton.Calculations;
using Kill_the_Norton.Entities;

namespace Kill_the_Norton.Presenters
{
    public class GamePresenter
    {
        public Game Game { get; set; }

        public Form form;

        //public Bullet? bullet;
        public List<Bullet> bullets = new List<Bullet>();

        public void DrawMap(Graphics gameGraphic)
        {
            for (var x = 0; x < Game.Level.MapWidth; x++)
            {
                for (var y = 0; y < Game.Level.MapHeight; y++)
                {
                    if (Game.Level.Map[x, y] == 1)
                    {
                        gameGraphic.DrawImage(Resources.AsphaltSprite,
                            y * Game.Level.SideOfMapObject - Game.Player.Delta.X,
                            x * Game.Level.SideOfMapObject - Game.Player.Delta.Y,
                            new Rectangle(new Point(0, 0),
                                new Size(Game.Level.SideOfMapObject, Game.Level.SideOfMapObject)),
                            GraphicsUnit.Pixel);
                    }

                    if (Game.Level.Map[x, y] == 2)
                    {
                        gameGraphic.DrawImage(Resources.WallSprite,
                            y * Game.Level.SideOfMapObject - Game.Player.Delta.X,
                            x * Game.Level.SideOfMapObject - Game.Player.Delta.Y,
                            new Rectangle(new Point(0, 0),
                                new Size(Game.Level.SideOfMapObject, Game.Level.SideOfMapObject)),
                            GraphicsUnit.Pixel);
                    }
                }
            }
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            Game.Player.Angle = (float) GameMath.GetAngle(Game.Player.PlayerCooridantes, new Point(e.X, e.Y));
            form.Controls[1].Text =
                "Координаты мыши: " + e.X + " " + e.Y + ", Угол: " + " " + Game.Player.Angle + "\n" +
                "Координацты игрока: " + Game.Player.PlayerCooridantes.X + " " +
                Game.Player.PlayerCooridantes.Y + "\n";

            form.Invalidate();
        }

        public void keyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                Game.Player.GoForward = false;
            }

            if (e.KeyCode == Keys.S)
            {
                Game.Player.GoBackward = false;
            }

            if (e.KeyCode == Keys.D)
            {
                Game.Player.GoRight = false;
            }

            if (e.KeyCode == Keys.A)
            {
                Game.Player.GoLeft = false;
            }
        }

        public void keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                Game.Player.GoForward = true;
            }

            if (e.KeyCode == Keys.S)
            {
                Game.Player.GoBackward = true;
            }

            if (e.KeyCode == Keys.D)
            {
                Game.Player.GoRight = true;
            }

            if (e.KeyCode == Keys.A)
            {
                Game.Player.GoLeft = true;
            }
        }

        public void MouseClickHandler(object sender, MouseEventArgs e)
        {
            var bullet = new Bullet();
            bullet.Target = new Point(e.X - Game.Player.Delta.X, e.Y - Game.Player.Delta.Y);
            bullet.Coordinates = new Point(Game.Player.PlayerCooridantes.X - Game.Player.Delta.X - 64,
                Game.Player.PlayerCooridantes.Y - Game.Player.Delta.Y - 64);
            bullet.Speed = 2;
            bullet.SpeedDelta = GameMath.GetDelta(bullet);
            form.Controls[0].Text = "Координаты клика: " + e.X + ", " + e.Y + "\n"
                                    + bullet.Target + "\n"
                                    + bullet.SpeedDelta;
            bullets.Add(bullet);
        }

        public void update(object sender, EventArgs e)
        {
            if (bullets.Count != 0)
            {
                foreach (var bullet in bullets)
                {
                    if (!GameMath.IsCollided(bullet, Game, form))
                    {
                        var bulletCoordinates = bullet.Coordinates;
                        bulletCoordinates.X += bullet.SpeedDelta.X * bullet.Speed;
                        bulletCoordinates.Y += bullet.SpeedDelta.Y * bullet.Speed;
                        bullet.Coordinates = bulletCoordinates;
                    }
                }
            }

            if (Game.Player.GoLeft)
            {
                if (!GameMath.IsCollided(-Game.Player.Speed, 0, Game))
                {
                    var playerCooridantes = Game.Player.PlayerCooridantes;
                    playerCooridantes.X -= Game.Player.Speed;
                    Game.Player.PlayerCooridantes = playerCooridantes;


                    if (playerCooridantes.X > 55 &&
                        playerCooridantes.X < Game.Level.SideOfMapObject * Game.Level.MapWidth)
                    {
                        var playerDelta = Game.Player.Delta;
                        playerDelta.X -= Game.Player.Speed;
                        Game.Player.Delta = playerDelta;
                    }
                }

                form.Invalidate();
            }

            if (Game.Player.GoRight)
            {
                if (!GameMath.IsCollided(+Game.Player.Speed, 0, Game))
                {
                    var playerCooridantes = Game.Player.PlayerCooridantes;
                    playerCooridantes.X += Game.Player.Speed;
                    Game.Player.PlayerCooridantes = playerCooridantes;


                    if (playerCooridantes.X > Game.Level.SideOfMapObject &&
                        playerCooridantes.X < Game.Level.SideOfMapObject * Game.Level.MapWidth)
                    {
                        var playerDelta = Game.Player.Delta;
                        playerDelta.X += Game.Player.Speed;
                        Game.Player.Delta = playerDelta;
                    }

                    form.Invalidate();
                }
            }

            if (Game.Player.GoBackward)
            {
                if (!GameMath.IsCollided(0, +Game.Player.Speed, Game))
                {
                    var playerPlayerCooridantes = Game.Player.PlayerCooridantes;
                    playerPlayerCooridantes.Y += Game.Player.Speed;
                    Game.Player.PlayerCooridantes = playerPlayerCooridantes;


                    if (Game.Player.PlayerCooridantes.Y > Game.Level.SideOfMapObject &&
                        Game.Player.PlayerCooridantes.Y < Game.Level.SideOfMapObject * Game.Level.MapHeight)
                    {
                        var playerDelta = Game.Player.Delta;
                        playerDelta.Y += Game.Player.Speed;
                        Game.Player.Delta = playerDelta;
                    }

                    form.Invalidate();
                }
            }

            if (Game.Player.GoForward)
            {
                if (!GameMath.IsCollided(0, -Game.Player.Speed, Game))
                {
                    var playerPlayerCooridantes = Game.Player.PlayerCooridantes;
                    playerPlayerCooridantes.Y -= Game.Player.Speed;
                    Game.Player.PlayerCooridantes = playerPlayerCooridantes;

                    if (Game.Player.PlayerCooridantes.Y > 55 && Game.Player.PlayerCooridantes.Y <
                        Game.Level.SideOfMapObject * Game.Level.MapHeight)
                    {
                        var playerDelta = Game.Player.Delta;
                        playerDelta.Y -= Game.Player.Speed;
                        Game.Player.Delta = playerDelta;
                    }
                }

                form.Invalidate();
            }

            Cursor.Position = Form1.MousePosition;
        }
    }
}