namespace Breakout.Effects;

using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using Breakout.Entities;

public static class TimedEffectsCanceler {
    public static void LevelEndCancel() {
        BreakoutBus.GetBus().CancelTimedEvent(101);
        BreakoutBus.GetBus().CancelTimedEvent(201);
        BreakoutBus.GetBus().CancelTimedEvent(301);
        BreakoutBus.GetBus().CancelTimedEvent(701);
    }

    public static void BallsLostCancel() {
        BreakoutBus.GetBus().CancelTimedEvent(201);
        BreakoutBus.GetBus().CancelTimedEvent(301);
    }
}
