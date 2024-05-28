namespace Breakout.PowerupEffects;

using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Timers;
using Breakout.Entities;

public class DoubleSize : IPowerupEffect {
    private static GameEvent disengageEvent = new GameEvent {
        EventType = GameEventType.TimedEvent,
        Message = "DISENGAGE_POWERUP",
        Id = 201,
        ObjectArg1 = new DoubleSize(),
    };

    public void EngagePowerup(EntityContainer<Ball> balls, Player player) {
        balls.Iterate(ball => {
        ball.Shape.Extent *= 2;
        });
        BreakoutBus.GetBus().RegisterTimedEvent(disengageEvent, TimePeriod.NewSeconds(5));
    }
    
    public void DisengagePowerup(EntityContainer<Ball> balls, Player player) {
        balls.Iterate(ball => {
        ball.Shape.Extent /= 2;
        });
    }
}
