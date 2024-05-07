using Breakout.Entities;

namespace Breakout.MovementStrategies;
public interface IMovementStrategy
{
    void Move(Ball ball);
}