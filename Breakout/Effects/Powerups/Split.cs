namespace Breakout.Effects.Powerups;

using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Utilities;
using Breakout.Entities;

public class Split : PowerupEffect, IEffect {
    private static Random rnd = RandomGenerator.Generator;
    private static int multiplier = 3;

    private void Rotate(Ball ball, float amount) {
        Vec2F dir = ball.Dynamic.Direction;

        ball.Dynamic.ChangeDirection(new Vec2F(
            dir.X * MathF.Cos(amount) - dir.Y * MathF.Sin(amount),
            dir.X * MathF.Sin(amount) + dir.Y * MathF.Cos(amount)
        ));
    }

    private void GiveRandomDirection(Ball ball) {
        float randomRotationAmount = rnd.NextSingle();
        randomRotationAmount *= (MathF.PI * 8);

        Rotate(ball, randomRotationAmount);
    }
    
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

    public override void DisengageEffect(EntityContainer<Ball> balls, Player player) {}
}
