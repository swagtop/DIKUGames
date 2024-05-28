namespace Breakout.PowerupEffects;

using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Utilities;
using Breakout.Entities;

public class Split : IPowerupEffect {
    private static int multiplier = 3;
    
    public void EngagePowerup(EntityContainer<Ball> balls, Player player) {
        if (balls.CountEntities() > 500) return;
        List<Ball> newBalls = new List<Ball>();
        balls.Iterate(ball => {
            for (int i = 0; i < multiplier; i++) {
                Ball newBall = ball.Clone();
                Vec2F newDir = newBall.Dynamic.Direction;
                float randomRotationAmount = RandomGenerator.Generator.NextSingle();
                randomRotationAmount *= MathF.PI;

                newBall.Dynamic.ChangeDirection(new Vec2F(
                    newDir.X * MathF.Cos(randomRotationAmount) - newDir.Y * MathF.Sin(randomRotationAmount),
                    newDir.X * MathF.Sin(randomRotationAmount) + newDir.Y * MathF.Cos(randomRotationAmount)
                ));

                newBalls.Add(newBall);
            }
            ball.DeleteEntity();
        });

        foreach (Ball ball in newBalls) {
            balls.AddEntity(ball);
        }
    }

    public void DisengagePowerup(EntityContainer<Ball> balls, Player player) {}
}
