namespace Breakout.PowerupEffects;

using DIKUArcade.Entities;
using Breakout.Entities;

public class DoubleSize : IPowerupEffect {
    public void EngagePowerup(EntityContainer<Ball> balls, Player player) {
        balls.Iterate(ball => {
        ball.Shape.Extent *= 2;
        return;
        });
    }
}
