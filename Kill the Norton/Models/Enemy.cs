﻿using System.Collections.Generic;
using System.Drawing;
using Kill_the_Norton.Calculations;

namespace Kill_the_Norton.Entities
{
    public class Enemy
    {
        public Point Cooridantes { get; set; }
        public int Speed { get; set; } = 5;

        public bool GoLeft { get; set; }
        public bool GoRight { get; set; }
        public bool GoForward { get; set; }
        public bool GoBackward { get; set; }

        public int ShootLatencyLimit = 100;
        private int _shootLatency = 100;

        public int ShootLatency
        {
            get { return _shootLatency; }
            set { _shootLatency = value; }
        }

        public float Angle { get; set; }

        public Enemy(Point cooridantes)
        {
            Cooridantes = cooridantes;
        }

        public void Shoot(Player player, List<Bullet> bullets)
        {
            var bullet = new Bullet(Sender.Enemy);
            bullet.Target = player.Cooridantes;

            //bullet.RenderCoordinates = new Point(Game.Player.Cooridantes.X - Game.Player.Delta.X - 64,
            //    Game.Player.Cooridantes.Y - Game.Player.Delta.Y - 64);

            bullet.OwnCoordinates =
                new PointF(Cooridantes.X - player.Delta.X + 32, Cooridantes.Y - player.Delta.Y + 32);

            bullet.SpeedDelta = GameMath.GetDelta(bullet);
            bullets.Add(bullet);
        }

        public void MoveEnemy(Game game)
        {
            if (GoRight)
            {
                if (!GameMath.IsEnemyCollidedWalls(+Speed, 0, game, this))
                {
                    var enemyCoordinates = Cooridantes;
                    enemyCoordinates.X += Speed;
                    Cooridantes = enemyCoordinates;
                }
                else
                {
                    GoRight = false;
                    GoForward = true;
                }
            }

            if (GoLeft)
            {
                if (!GameMath.IsEnemyCollidedWalls(-Speed, 0, game, this))
                {
                    var enemyCoordinates = Cooridantes;
                    enemyCoordinates.X -= Speed;
                    Cooridantes = enemyCoordinates;
                }
                else
                {
                    GoLeft = false;
                    GoBackward = true;
                }
            }

            if (GoBackward)
            {
                if (!GameMath.IsEnemyCollidedWalls(0, -Speed, game, this))
                {
                    var enemyCoordinates = Cooridantes;
                    enemyCoordinates.Y -= Speed;
                    Cooridantes = enemyCoordinates;
                }
                else
                {
                    GoBackward = false;
                    GoRight = true;
                }
            }

            if (GoForward)
            {
                if (!GameMath.IsEnemyCollidedWalls(0, +Speed, game, this))
                {
                    var enemyCoordinates = Cooridantes;
                    enemyCoordinates.Y += Speed;
                    Cooridantes = enemyCoordinates;
                }
                else
                {
                    GoForward = false;
                    GoLeft = true;
                }
            }
        }
    }
}