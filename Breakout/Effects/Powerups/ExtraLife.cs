namespace Breakout.Effects.Powerups;

using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using Breakout;
using Breakout.Entities;

public class ExtraLife : PowerupEffect, IEffect {
    public override void EngageEffect(EntityContainer<Ball> balls, Player player) {
        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "GAIN_LIFE",
            Id = 400,
        });
    }
    
    public override void DisengageEffect(EntityContainer<Ball> balls, Player player) {}
}
