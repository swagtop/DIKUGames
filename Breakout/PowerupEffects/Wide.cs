namespace Breakout.PowerupEffects;

using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using Breakout;
using Breakout.Entities;

public class Wide : IPowerupEffect {
    private static GameEvent skinnyEvent = new GameEvent {
        EventType = GameEventType.PlayerEvent,
        Message = "GET_SKINNY",
        Id = 100,
    };

    public void EngagePowerup(EntityContainer<Ball> balls, Player player) {
        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "GET_FAT",
        });
        BreakoutBus.GetBus().AddOrResetTimedEvent(
            skinnyEvent,
            TimePeriod.NewSeconds(5)
        );
    }
}
