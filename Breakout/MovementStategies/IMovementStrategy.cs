namespace Breakout.MovementStrategies;

using Breakout.Entities;

public interface IMovementStrategy {
    void Move(Ball ball);
}
