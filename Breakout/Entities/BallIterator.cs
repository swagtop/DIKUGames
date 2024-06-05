namespace Breakout.Entities;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using Breakout;
using Breakout.Effects;
using Breakout.Entities;
using Breakout.Entities.Blocks;
using Breakout.LevelHandling;
using Breakout.GUI;

/// <summary>
/// This class handles the interaction between balls, blocks, and the player. Its main method 
/// moves all balls, and returns the result of each iteration. This result should be used to 
/// trigger other in-game events.
/// </summary>
public static class BallIterator {
    private static readonly Vec2F defaultBallDirection = new Vec2F(0.0f, 0.0150f);

    /// <summary> Iterates the balls, moving them, checking for collisions. </summary>
    /// <param name="currentLevel"> A level instance, should be the one in GameRunning. </param>
    /// <param name="balls"> A ball container, should be the one in GameRunning. </param>
    /// <param name="player"> A player instance, should be the one in GameRunning. </param>
    /// <param name="points"> A points instance, should be the one in GameRunning. </param>
    public static string IterateBalls(Level currentLevel, Player player, EntityContainer<Ball> balls, Points points) {
        int ballCount = balls.CountEntities();

        balls.Iterate(ball => {
            ball.Move();
            CollisionData colCheckPlayer = CollisionDetection.Aabb(
                ball.Dynamic, 
                player.Shape.AsDynamicShape()
            );

            if (colCheckPlayer.Collision) {
                float ballMiddle = ball.Shape.Position.X + (ball.Shape.Extent.X / 2.0f);
                float playerMiddle = player.Shape.Position.X + (player.Shape.Extent.X / 2.0f);
                float rotation = (playerMiddle - ballMiddle) * 12.0f;

                ball.ChangeDirection(colCheckPlayer.CollisionDir);

                Vec2F newDir = defaultBallDirection.Copy();

                ball.Dynamic.ChangeDirection(new Vec2F(
                    newDir.X * MathF.Cos(rotation) - newDir.Y * MathF.Sin(rotation),
                    newDir.X * MathF.Sin(rotation) + newDir.Y * MathF.Cos(rotation)
                ));
            }

            currentLevel.Blocks.Iterate(block => {
                CollisionData colCheckBlock = CollisionDetection.Aabb(
                    ball.Dynamic, 
                    block.Shape
                );

                if (colCheckBlock.Collision) {
                    block.Hit();
                    if (ball.IsHard) { block.Hit(); } // Double damage for hard balls.
                    ball.ChangeDirection(colCheckBlock.CollisionDir);
                }
                if (block.IsDeleted()) {
                    points.AwardPointsFor(block);
                    currentLevel.BreakableLeft -= 1;
                }
            });
        });

        if (currentLevel.BreakableLeft == 0) {
            return "NO_MORE_BLOCKS";
        }

        bool lostAllBalls = (ballCount != 0 && balls.CountEntities() == 0);
        if (lostAllBalls) {
           TimedEffectsCanceler.BallsLostCancel();
           return "NO_MORE_BALLS";
        }

        return "CONTINUE";
    }
}
