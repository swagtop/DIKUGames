namespace Breakout.Effects.Powerups;

using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Utilities;
using Breakout.Entities;

/// <summary>
/// This powerup splits each existing ball into three new balls, traveling in random directions.
/// </summary>
public class Split : PowerupEffect, IEffect {
    private static Random rnd = RandomGenerator.Generator;
    private static int multiplier = 3;

    /// <summary> Rotates ball with vector rotation formula </param>
    /// <param name="ball"=> The ball we wish to rotate </param>
    /// <param name="amount"=> The amount in radians we wish to rotate by </param>
    private void Rotate(Ball ball, float amount) {
        Vec2F dir = ball.Dynamic.Direction;

        ball.Dynamic.ChangeDirection(new Vec2F(
            dir.X * MathF.Cos(amount) - dir.Y * MathF.Sin(amount),
            dir.X * MathF.Sin(amount) + dir.Y * MathF.Cos(amount)
        ));
    }

    /// <summary> Rotates ball a random amount </param>
    /// <param name="ball"=> The ball we wish to rotate </param>
    private void GiveRandomDirection(Ball ball) {
        float randomRotationAmount = rnd.NextSingle();
        randomRotationAmount *= (MathF.PI * 8);

        Rotate(ball, randomRotationAmount);
    }
    
    /// <summary> Rotates ball a random amount </param>
    /// <param name="balls"=> The container of the balls we wish to split </param>
    /// <param name="player"=> Not used here. </param>
    public override void EngageEffect(EntityContainer<Ball> balls, Player player) {
        if (balls.CountEntities() > 500) return; // Avoiding crazy amounts of balls.

        List<Ball> newBalls = new List<Ball>();
        balls.Iterate(ball => {
            for (int i = 0; i < multiplier; i++) {
                Ball newBall = ball.Clone();
                
                GiveRandomDirection(newBall);

                newBalls.Add(newBall);
            }
            ball.DeleteEntity();
        });

        foreach (Ball ball in newBalls) {
            balls.AddEntity(ball);
        }
    }

    /// <summary> Nothing to disengage. </param>
    public override void DisengageEffect(EntityContainer<Ball> balls, Player player) {}
}
