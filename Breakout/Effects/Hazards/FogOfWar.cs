namespace Breakout.Effects.Hazards;

using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Timers;
using Breakout.Entities;

/// <summary>
///
/// </summary>
public class FogOfWar : HazardEffect, IEffect {
    private static GameEvent engageEvent = new GameEvent {
        EventType = GameEventType.StatusEvent,
        Message = "SET_FOG_OF_WAR",
        StringArg1 = "ENGAGE",
        Id = 700,
    };
    
    private static GameEvent disengageEvent = new GameEvent {
        EventType = GameEventType.TimedEvent,
        Message = "SET_FOG_OF_WAR",
        StringArg1 = "DISENGAGE",
        Id = 701,
    };

    public override void EngageEffect(EntityContainer<Ball> balls, Player player) {
        BreakoutBus.GetBus().RegisterEvent(engageEvent);
        BreakoutBus.GetBus().AddOrResetTimedEvent(disengageEvent, TimePeriod.NewSeconds(5));
    }

    public override void DisengageEffect(EntityContainer<Ball> balls, Player player) {}
}

