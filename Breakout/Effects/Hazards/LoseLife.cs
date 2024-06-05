namespace Breakout.Effects.Hazards;

using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using Breakout.Entities;

/// <summary>
/// This hazard tells the GameRunning game state to decrement the players health when engaged.
/// </summary>
public class LoseLife : HazardEffect, IEffect {
    private static GameEvent engageEffect = new GameEvent {
        EventType = GameEventType.StatusEvent,
        Message = "LOSE_LIFE",
        Id = 600,
    };

    /// <summary> Sends off game event to GameRunning, asking for player to lose a life </param>
    public override void EngageEffect(EntityContainer<Ball> balls, Player player) {
        BreakoutBus.GetBus().RegisterEvent(engageEffect);
    }

    /// <summary> Nothing to disengage. </param>
    public override void DisengageEffect(EntityContainer<Ball> balls, Player player) {}
}

