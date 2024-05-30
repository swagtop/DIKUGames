namespace Breakout.Effects.Hazards;

using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using Breakout.Entities;

public class LoseLife : HazardEffect, IEffect {
    public override void EngageEffect(EntityContainer<Ball> balls, Player player) {
        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "LOSE_LIFE",
            Id = 600,
        });
    }

    public override void DisengageEffect(EntityContainer<Ball> balls, Player player) {}
}

