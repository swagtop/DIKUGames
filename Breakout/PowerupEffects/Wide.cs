namespace Breakout.PowerupEffects;

using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using Breakout;
using Breakout.Entities;

public class Wide : IPowerupEffect {
    private static GameEvent disengageEvent = new GameEvent {
        EventType = GameEventType.TimedEvent,
        Message = "GET_SKINNY",
        Id = 101,
        ObjectArg1 = new Wide(),
    };

    public void EngagePowerup(EntityContainer<Ball> balls, Player player) {
        player.GetFat();
        BreakoutBus.GetBus().AddOrResetTimedEvent(disengageEvent, TimePeriod.NewSeconds(5));
    }

    public void DisengagePowerup(EntityContainer<Ball> balls, Player player) {
        player.GetSkinny();
    }
}
