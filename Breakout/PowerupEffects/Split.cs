namespace Breakout.PowerupEffects;

using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Utilities;
using Breakout.Entities;

public class Split : IPowerupEffect {
    public int multiplier;

    public Split() {
        multiplier = 3;
    }
    
    public void EngagePowerup(EntityContainer<Ball> balls, Player player) {
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
        });
        
        if (newBalls.Count == 0) return;

        foreach (Ball ball in newBalls) {
            balls.AddEntity(ball);
        }
    }
}
