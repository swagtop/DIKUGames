using Breakout.Entities;

namespace Breakout.MovementStrategies;
public class StandardMove : IMovementStrategy {
    public void Move(Ball ball) {
        ball.Move();
    }
}