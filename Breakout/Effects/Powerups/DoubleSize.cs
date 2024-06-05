namespace Breakout.Effects.Powerups;

using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Timers;
using Breakout.Entities;

/// <summary>
/// This powerup doubles the size of the balls when engaged, and halves them when disengaged.
/// </summary>
public class DoubleSize : PowerupEffect, IEffect {
    private static GameEvent disengageEvent = new GameEvent {
        EventType = GameEventType.TimedEvent,
        Message = "DISENGAGE_EFFECT",
        Id = 201,
        ObjectArg1 = new DoubleSize(),
    };

    /// <summary> Doubles all balls sizes, adds timed event to halve their size. </param>
    /// <param name="balls"=> The ball container of balls we wish to make hard </param>
    /// <param name="player"=> Not used here. </param>
    public override void EngageEffect(EntityContainer<Ball> balls, Player player) {
        balls.Iterate(ball => { ball.Shape.Extent *= 2; });
        BreakoutBus.GetBus().RegisterTimedEvent(disengageEvent, TimePeriod.NewSeconds(5));
    }
    
    /// <summary> Halves size of all balls. </param>
    /// <param name="balls"=> The ball container of balls we wish to make hard </param>
    /// <param name="player"=> Not used here. </param>
    public override void DisengageEffect(EntityContainer<Ball> balls, Player player) {
        balls.Iterate(ball => { ball.Shape.Extent /= 2; });
    }
}
