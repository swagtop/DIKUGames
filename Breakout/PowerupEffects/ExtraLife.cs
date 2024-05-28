namespace Breakout.PowerupEffects;

using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using Breakout;
using Breakout.Entities;

public class ExtraLife : IPowerupEffect {
    public void EngagePowerup(EntityContainer<Ball> balls, Player player) {
        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "GAIN_LIFE",
            Id = 400,
        });
    }
    
    public void DisengagePowerup(EntityContainer<Ball> balls, Player player) {}
}
