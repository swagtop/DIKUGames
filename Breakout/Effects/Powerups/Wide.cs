namespace Breakout.Effects.Powerups;

using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using Breakout;
using Breakout.Entities;

public class Wide : PowerupEffect, IEffect {
    private static GameEvent disengageEvent = new GameEvent {
        EventType = GameEventType.TimedEvent,
        Message = "DISENGAGE_EFFECT",
        Id = 101,
        ObjectArg1 = new Wide(),
    };

    public void EngageEffect(EntityContainer<Ball> balls, Player player) {
        player.GetFat();
        BreakoutBus.GetBus().AddOrResetTimedEvent(disengageEvent, TimePeriod.NewSeconds(5));
    }

    public void DisengageEffect(EntityContainer<Ball> balls, Player player) {
        player.GetSkinny();
    }
}
