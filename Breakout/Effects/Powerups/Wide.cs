namespace Breakout.Effects.Powerups;

using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using Breakout;
using Breakout.Entities;

/// <summary>
/// This powerup makes the player fat when engaged, and skinny again when disengaged.
/// </summary>
public class Wide : PowerupEffect, IEffect {
    private static GameEvent disengageEvent = new GameEvent {
        EventType = GameEventType.TimedEvent,
        Message = "DISENGAGE_EFFECT",
        Id = 101,
        ObjectArg1 = new Wide(),
    };

    /// <summary> Calls the GetFat() method of Player class. </param>
    /// <param name="balls"=> Not used here. </param>
    /// <param name="player"=> The Player we wish to get fat </param>
    public override void EngageEffect(EntityContainer<Ball> balls, Player player) {
        player.GetFat();
        BreakoutBus.GetBus().AddOrResetTimedEvent(disengageEvent, TimePeriod.NewSeconds(5));
    }

    /// <summary> Calls the GetSkinny() method of Player class. </param>
    /// <param name="balls"=> Not used here. </param>
    /// <param name="player"=> The Player we wish to get skinny </param>
    public override void DisengageEffect(EntityContainer<Ball> balls, Player player) {
        player.GetSkinny();
    }
}
