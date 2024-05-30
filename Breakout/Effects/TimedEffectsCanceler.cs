namespace Breakout.Effects;

using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using Breakout.Entities;

public static class TimedEffectsCanceler {
    public static void LevelEndCancel() {
        GameEventBus eventBus = BreakoutBus.GetBus();
        
        eventBus.CancelTimedEvent(101);
        eventBus.CancelTimedEvent(201);
        eventBus.CancelTimedEvent(301);
        eventBus.CancelTimedEvent(701);
    }

    public static void BallsLostCancel() {
        BreakoutBus.GetBus().CancelTimedEvent(201);
        BreakoutBus.GetBus().CancelTimedEvent(301);
    }
}
