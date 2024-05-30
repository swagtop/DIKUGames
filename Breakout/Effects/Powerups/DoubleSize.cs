namespace Breakout.Effects.Powerups;

using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Timers;
using Breakout.Entities;

public class DoubleSize : IEffect {
    private static GameEvent disengageEvent = new GameEvent {
        EventType = GameEventType.TimedEvent,
        Message = "DISENGAGE_EFFECT",
        Id = 201,
        ObjectArg1 = new DoubleSize(),
    };

    public void EngageEffect(EntityContainer<Ball> balls, Player player) {
        balls.Iterate(ball => { ball.Shape.Extent *= 2; });
        BreakoutBus.GetBus().RegisterTimedEvent(disengageEvent, TimePeriod.NewSeconds(5));
    }
    
    public void DisengageEffect(EntityContainer<Ball> balls, Player player) {
        balls.Iterate(ball => { ball.Shape.Extent /= 2; });
    }
}
