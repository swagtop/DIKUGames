namespace Breakout.MovementStrategies;

using Breakout.Entities;

public class StandardMove : IMovementStrategy {
    public void Move(Ball ball) {
        ball.Move();
    }
}
