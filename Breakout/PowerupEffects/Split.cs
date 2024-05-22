namespace Breakout.PowerUpEffects;

public class Split : PowerUpEffects {
    public void ActivateSplit(List<Ball> balls) {
        var newBalls = new List<Ball>();
        foreach (var ball in balls) {
            newBalls.Add(CreateNewBall(ball, new Vec2F(1, 1)));
            newBalls.Add(CreateNewBall(ball, new Vec2F(-1, 1)));
            newBalls.Add(CreateNewBall(ball, new Vec2F(0, 1)));
        }
        balls.AddRange(newBalls);  
    }
    private Ball CreateNewBall(Ball originalBall, Vec2F direction) {
        var newBall = new Ball(new DynamicShape(originalBall.Shape.Position, originalBall.Shape.Extent, direction), Image);
    }
    return newBall;
}
