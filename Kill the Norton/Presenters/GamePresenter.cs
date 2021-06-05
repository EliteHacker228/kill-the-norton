#nullable enable
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        public TimeMachine TimeMachine = new TimeMachine();
        public List<Bullet> bullets = new List<Bullet>();
        public List<Enemy> enemies = new List<Enemy>();

        public GamePresenter(int[,] map)
        {
            for (var x = 0; x < map.GetLength(0); x++)
            {
                for (var y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y] == 9)
                    {
                        var enemy = new Enemy(new Point(y * 64, x * 64), new PatrolEnemyMover());
                        enemy.GoLeft = true;
                        enemies.Add(enemy);
                        map[x, y] = 1;
                    }

                    if (map[x, y] == 8)
                    {
                        var enemy = new Enemy(new Point(y * 64, x * 64), new SentryEnemyMover());
                        enemy.GoLeft = true;
                        enemies.Add(enemy);
                        map[x, y] = 1;
                    }
                }
            }
        }

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
            Game.Player.Angle = (float) GameMath.GetAngle(Game.Player.Cooridantes, new Point(e.X, e.Y));
            form.Controls[1].Text =
                "Координаты мыши: " + e.X + " " + e.Y + ", Угол: " + " " + Game.Player.Angle + "\n" +
                "Координацты игрока: " + Game.Player.Cooridantes.X + " " +
                Game.Player.Cooridantes.Y + "\n" +
                "Дельта игрока: " + Game.Player.Delta.X + " " + Game.Player.Delta.Y;

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

        public void keyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'f')
            {
                TimeMachine.StopTime(Game, enemies, bullets);
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
            var bullet = new Bullet(Sender.Player);
            bullet.Target = new Point(e.X, e.Y);

            //bullet.RenderCoordinates = new Point(Game.Player.Cooridantes.X - Game.Player.Delta.X - 64,
            //    Game.Player.Cooridantes.Y - Game.Player.Delta.Y - 64);

            bullet.OwnCoordinates = Game.Player.Cooridantes;

            bullet.SpeedDelta = GameMath.GetDelta(bullet);
            form.Controls[0].Text = "Координаты клика: " + e.X + ", " + e.Y + "\n"
                                    + "Координаты цели: " + bullet.Target + "\n"
                                    + "Дельта скорости: " + bullet.SpeedDelta;
            bullets.Add(bullet);
        }

        public void update(object sender, EventArgs e)
        {
            form.Invalidate();
            //form.Controls[3].Text = ""+TimeMachine.Check();
            TimeMachine.Check();

            if (!Game.Player.IsAlive)
            {
                Game.Player.Sprite = Resources.TransparentSprite;
                //return;
            }

            if (!TimeMachine.IsTimeStopped)
                foreach (var enemy in enemies)
                {
                    var moddedCoorinates = new Point(Game.Player.Cooridantes.X + Game.Player.Delta.X,
                        Game.Player.Cooridantes.Y + Game.Player.Delta.Y);

                    /*form.Controls[2].Text = ""+
                        GameMath.GetDistanceBetweenTwoPoints(Game.Player.Cooridantes, enemy.Cooridantes);*/
                    var moddedPlayerCoordinates = new Point(Game.Player.Cooridantes.X + Game.Player.Delta.X,
                        Game.Player.Cooridantes.Y + Game.Player.Delta.Y);
                    if (GameMath.GetDistanceBetweenTwoPoints(moddedPlayerCoordinates, enemy.Cooridantes) < 640)
                    {
                        enemy.Angle = (float) (GameMath.GetAngle(enemy.Cooridantes, moddedCoorinates) *
                                               TimeMachine.SlowingProportion);
                        enemy.ShootLatency--;

                        enemy.MoveEnemy(Game);

                        if (enemy.ShootLatency == 0)
                        {
                            enemy.Shoot(Game.Player, bullets);
                            enemy.ShootLatency = enemy.ShootLatencyLimit;
                        }
                    }
                }

            if (bullets.Count != 0)
            {
                //bullets.RemoveAll(x => GameMath.IsCollided(x, Game, form, enemies).Item1);

                foreach (var bullet in bullets)
                {
                    if (!GameMath.IsBulletCollidedWithEnemiesOrWalls(bullet, Game, form, enemies).Item1)
                    {
                        var bulletOwnCoordinates = bullet.OwnCoordinates;
                        bulletOwnCoordinates.X += bullet.SpeedDelta.X * Bullet.Speed;
                        bulletOwnCoordinates.Y += bullet.SpeedDelta.Y * Bullet.Speed;
                        bullet.OwnCoordinates = bulletOwnCoordinates;
                    }
                }

                var bulletsToDelete = bullets.Select(x => x)
                    .Where(x => GameMath.IsBulletCollidedWithEnemiesOrWalls(x, Game, form, enemies).Item1).ToList();

                var result = bullets.Select(x => GameMath.IsBulletCollidedWithEnemiesOrWalls(x, Game, form, enemies));

                foreach (var element in result)
                {
                    if (enemies.Remove(element.Item2))
                    {
                        TimeMachine.ReapedSouls++;
                    }
                }

                foreach (var bullet in bulletsToDelete)
                {
                    bullets.Remove(bullet);
                }

                var bulletsHittedPlayer = bullets.Where(x => GameMath.IsBulletCollidedWithPlayerOrWalls(x, Game, form))
                    .Select(x => x);

                foreach (var bullet in bulletsToDelete)
                {
                    bullets.Remove(bullet);
                }

                if (bulletsHittedPlayer.Count() != 0 && !Player.IsInvincible)
                {
                    Game.Player.IsAlive = false;
                }

                bullets.RemoveAll(x => bulletsHittedPlayer.Contains(x));
                /*foreach (var bullet in bulletsHittedPlayer)
                {
                    bullets.Remove(bullet);
                }*/
            }

            if (Game.Player.GoLeft)
            {
                if (!GameMath.IsPlayerCollidedEnemiesOrWalls(-Game.Player.Speed, 0, Game, enemies))
                {
                    var playerCooridantes = Game.Player.Cooridantes;
                    playerCooridantes.X -= Game.Player.Speed;
                    Game.Player.Cooridantes = playerCooridantes;

                    foreach (var bullet in bullets)
                    {
                        var coords = bullet.OwnCoordinates;
                        coords.X += Game.Player.Speed;
                        bullet.OwnCoordinates = coords;
                    }

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
                if (!GameMath.IsPlayerCollidedEnemiesOrWalls(+Game.Player.Speed, 0, Game, enemies))
                {
                    var playerCooridantes = Game.Player.Cooridantes;
                    playerCooridantes.X += Game.Player.Speed;
                    Game.Player.Cooridantes = playerCooridantes;

                    foreach (var bullet in bullets)
                    {
                        var coords = bullet.OwnCoordinates;
                        coords.X -= Game.Player.Speed;
                        bullet.OwnCoordinates = coords;
                    }

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
                if (!GameMath.IsPlayerCollidedEnemiesOrWalls(0, +Game.Player.Speed, Game, enemies))
                {
                    var playerPlayerCooridantes = Game.Player.Cooridantes;
                    playerPlayerCooridantes.Y += Game.Player.Speed;
                    Game.Player.Cooridantes = playerPlayerCooridantes;

                    foreach (var bullet in bullets)
                    {
                        var coords = bullet.OwnCoordinates;
                        coords.Y -= Game.Player.Speed;
                        bullet.OwnCoordinates = coords;
                    }

                    if (Game.Player.Cooridantes.Y > Game.Level.SideOfMapObject &&
                        Game.Player.Cooridantes.Y < Game.Level.SideOfMapObject * Game.Level.MapHeight)
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
                if (!GameMath.IsPlayerCollidedEnemiesOrWalls(0, -Game.Player.Speed, Game, enemies))
                {
                    var playerPlayerCooridantes = Game.Player.Cooridantes;
                    playerPlayerCooridantes.Y -= Game.Player.Speed;
                    Game.Player.Cooridantes = playerPlayerCooridantes;

                    foreach (var bullet in bullets)
                    {
                        var coords = bullet.OwnCoordinates;
                        coords.Y += Game.Player.Speed;
                        bullet.OwnCoordinates = coords;
                    }

                    if (Game.Player.Cooridantes.Y > 55 && Game.Player.Cooridantes.Y <
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

        /*private void MoveEnemy(Enemy enemy)
        {
            if (enemy.GoRight)
            {
                if (!GameMath.IsEnemyCollidedWalls(+enemy.Speed, 0, Game, enemy))
                {
                    var enemyCoordinates = enemy.Cooridantes;
                    enemyCoordinates.X += enemy.Speed;
                    enemy.Cooridantes = enemyCoordinates;

                    form.Invalidate();
                }
                else
                {
                    enemy.GoRight = false;
                    enemy.GoForward = true;
                }
            }

            if (enemy.GoLeft)
            {
                if (!GameMath.IsEnemyCollidedWalls(-enemy.Speed, 0, Game, enemy))
                {
                    var enemyCoordinates = enemy.Cooridantes;
                    enemyCoordinates.X -= enemy.Speed;
                    enemy.Cooridantes = enemyCoordinates;

                    form.Invalidate();
                }
                else
                {
                    enemy.GoLeft = false;
                    enemy.GoBackward = true;
                }
            }

            if (enemy.GoBackward)
            {
                if (!GameMath.IsEnemyCollidedWalls(0, -enemy.Speed, Game, enemy))
                {
                    var enemyCoordinates = enemy.Cooridantes;
                    enemyCoordinates.Y -= enemy.Speed;
                    enemy.Cooridantes = enemyCoordinates;

                    form.Invalidate();
                }
                else
                {
                    enemy.GoBackward = false;
                    enemy.GoRight = true;
                }
            }

            if (enemy.GoForward)
            {
                if (!GameMath.IsEnemyCollidedWalls(0, +enemy.Speed, Game, enemy))
                {
                    var enemyCoordinates = enemy.Cooridantes;
                    enemyCoordinates.Y += enemy.Speed;
                    enemy.Cooridantes = enemyCoordinates;

                    form.Invalidate();
                }
                else
                {
                    enemy.GoForward = false;
                    enemy.GoLeft = true;
                }
            }
        }*/
    }
}