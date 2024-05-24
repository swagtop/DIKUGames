/*
namespace Breakout.Entities;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;

public static class BallMover {
    public static bool MoveBalls(EntityContainer<Block> balls, EntityContainer<Block> blocks, Player player) {
        int ballCount = balls.CountEntities();

        balls.Iterate(ball => {
            movementStrategy.Move(ball);
            CollisionData colCheckPlayer = CollisionDetection.Aabb(
                ball.Dynamic, 
                player.Shape.AsDynamicShape()
            );

            if (colCheckPlayer.Collision) {
                float ballMiddle = ball.Shape.Position.X + (ball.Shape.Extent.X / 2.0f);
                float playerMiddle = player.Shape.Position.X + (player.Shape.Extent.X / 2.0f);
                float relativeRotation = (playerMiddle - ballMiddle) * 12.0f;

                ball.ChangeDirection(colCheckPlayer.CollisionDir);

                Vec2F newDir = defaultBallDirection.Copy();

                ball.Dynamic.ChangeDirection(new Vec2F(
                    newDir.X * MathF.Cos(relativeRotation) - newDir.Y * MathF.Sin(relativeRotation),
                    newDir.X * MathF.Sin(relativeRotation) + newDir.Y * MathF.Cos(relativeRotation)
                ));
            }

            currentLevel.Blocks.Iterate(block => {
                CollisionData colCheckBlock = CollisionDetection.Aabb(
                    ball.Dynamic, 
                    block.Shape
                );
                if (colCheckBlock.Collision) {
                    block.Hit();
                    ball.ChangeDirection(colCheckBlock.CollisionDir);
                }
                if (block.IsDeleted()) {
                    points.AwardPointsFor(block);
                }
            });

        });

        if (ballCount != 0 && balls.CountEntities() == 0) {
            bool playerLost = hearts.BreakHeart();
            if (playerLost) { EndGame(false); }
            else { ballLauncher.AddNewBall(); }
        }
    }
}
*/