using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Kill_the_Norton.Entities;

namespace Kill_the_Norton.Calculations
{
    public class GameMath
    {
        public static double GetAngle(Point p1, Point p2)
        {
            float xDiff = p2.X - p1.X;
            float yDiff = p2.Y - p1.Y;
            return Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
        }

        public static bool IsCollided(int dx, int dy, Game game)
        {
            var playerX = (game.Player.Cooridantes.X + dx);
            var playerY = (game.Player.Cooridantes.Y + dy);

            var processedX = (playerX + game.Player.Delta.X) / 64;
            var processedY = (playerY + game.Player.Delta.Y) / 64;

            if (game.Level.Map[processedY, processedX] == 2)
                return true;
            return false;
        }

        public static bool IsCollided(Bullet bullet, Game game, Form form, List<Enemy> enemies)
        {
            var bulletX = (bullet.OwnCoordinates.X + bullet.SpeedDelta.X);
            var bulletY = (bullet.OwnCoordinates.Y + bullet.SpeedDelta.Y);

            /*var processedX = (int) Math.Round((bulletX) / 16);
            var processedY = (int) Math.Round((bulletY) / 16);*/

            var processedX = (bulletX + game.Player.Delta.X) / 64;
            var processedY = (bulletY + game.Player.Delta.Y) / 64;

            form.Controls[2].Text = "Преобразованные координаты пули: " + (int) processedX + " " + (int) processedY +
                                    "\n"
                                    + "Собственные координаты пули: " + (int) bullet.OwnCoordinates.X + " " +
                                    (int) bullet.OwnCoordinates.Y;

            foreach (var enemy in enemies)
            {
                if (bullet.OwnCoordinates.X == enemy.Cooridantes.X && bullet.OwnCoordinates.X == enemy.Cooridantes.X)
                {
                    if (bullet.OwnCoordinates.Y == enemy.Cooridantes.Y && bullet.OwnCoordinates.Y == enemy.Cooridantes.Y)
                    {
                        return true;
                    }
                }
            }

            try
            {
                if (game.Level.Map[(int) processedY, (int) processedX] == 2)
                    return true;
            }
            catch (Exception e)
            {
                return false;
            }

            return false;
        }

        public static bool IsCollided(int dx, int dy, Game game, Label label)
        {
            var playerX = (game.Player.Cooridantes.X + dx);
            var playerY = (game.Player.Cooridantes.Y + dy);

            var processedX = (playerX + game.Player.Delta.X) / 64;
            var processedY = (playerY + game.Player.Delta.Y) / 64;

            label.Text = "PlayerX: " + playerX + "\n" +
                         "PlayerY: " + playerY + "\n" +
                         "DeltaX: " + game.Player.Delta.X + "\n" +
                         "DeltaY: " + game.Player.Delta.Y + "\n" +
                         "ProcessedX: " + processedX + "\n" +
                         "ProcessedY: " + processedY + "\n" +
                         "Collided: " + (game.Level.Map[processedY, processedX] == 2);

            if (game.Level.Map[processedY, processedX] == 2)
                return true;
            return false;
        }

        public static PointF GetDelta(Bullet bullet)
        {
            var sideX = bullet.Target.X - bullet.OwnCoordinates.X;
            var sideY = bullet.Target.Y - bullet.OwnCoordinates.Y;
            var k = Math.Sqrt(sideX * sideX + sideY * sideY);
            var deltaX = (float) (sideX / k);
            var deltaY = (float) (sideY / k);
            return new PointF(deltaX, deltaY);
        }
    }
}