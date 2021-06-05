using Kill_the_Norton.Entities;

namespace Kill_the_Norton.Calculations
{
    public class PatrolEnemyMover : EnemyMover
    {
        public override void MoveEnemy(Enemy enemy, Game game)
        {
            if (enemy.GoRight)
            {
                if (!GameMath.IsEnemyCollidedWalls(+enemy.Speed, 0, game, enemy))
                {
                    var enemyCoordinates = enemy.Cooridantes;
                    enemyCoordinates.X += enemy.Speed;
                    enemy.Cooridantes = enemyCoordinates;
                }
                else
                {
                    enemy.GoRight = false;
                    enemy.GoForward = true;
                }
            }

            if (enemy.GoLeft)
            {
                if (!GameMath.IsEnemyCollidedWalls(-enemy.Speed, 0, game, enemy))
                {
                    var enemyCoordinates = enemy.Cooridantes;
                    enemyCoordinates.X -= enemy.Speed;
                    enemy.Cooridantes = enemyCoordinates;
                }
                else
                {
                    enemy.GoLeft = false;
                    enemy.GoBackward = true;
                }
            }

            if (enemy.GoBackward)
            {
                if (!GameMath.IsEnemyCollidedWalls(0, -enemy.Speed, game, enemy))
                {
                    var enemyCoordinates = enemy.Cooridantes;
                    enemyCoordinates.Y -= enemy.Speed;
                    enemy.Cooridantes = enemyCoordinates;
                }
                else
                {
                    enemy.GoBackward = false;
                    enemy.GoRight = true;
                }
            }

            if (enemy.GoForward)
            {
                if (!GameMath.IsEnemyCollidedWalls(0, +enemy.Speed, game, enemy))
                {
                    var enemyCoordinates = enemy.Cooridantes;
                    enemyCoordinates.Y += enemy.Speed;
                    enemy.Cooridantes = enemyCoordinates;
                }
                else
                {
                    enemy.GoForward = false;
                    enemy.GoLeft = true;
                }
            }
        }
    }
}