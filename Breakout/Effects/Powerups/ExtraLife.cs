namespace Breakout.Effects.Powerups;

using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using Breakout;
using Breakout.Entities;

/// <summary>
/// This powerup tells the GameRunning state to heal the player for one health point when engaged.
/// </summary>
public class ExtraLife : PowerupEffect, IEffect {
    private static GameEvent engageEvent = new GameEvent {
        EventType = GameEventType.StatusEvent,
        Message = "GAIN_LIFE",
        Id = 400,
    };

    public override void EngageEffect(EntityContainer<Ball> balls, Player player) {
        BreakoutBus.GetBus().RegisterEvent(engageEvent);
    }
    
    public override void DisengageEffect(EntityContainer<Ball> balls, Player player) {}
}
