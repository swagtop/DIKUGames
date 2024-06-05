namespace Breakout.Effects;

using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using Breakout.Entities;

/// <summary>
/// Some powerups and hazards have timed events, that when triggered revert effects. When levels
/// end or new ball instances are introduced, these effects could trigger, causing unwanted
/// effects. This class is the tool that should be used to cancel these events.
/// </summary>
public static class TimedEffectsCanceler {
    private static readonly GameEventBus eventBus = BreakoutBus.GetBus();
    private static void CancelEvents(params uint[] eventIds) {
        foreach (uint eventId in eventIds) {
            if (eventBus.HasTimedEvent(eventId)) { eventBus.CancelTimedEvent(eventId); }
        }
    }

    public static void LevelEndCancel() {
        CancelEvents(
            101, // (Powerup) Wide
            201, // (Powerup) DoubleSize
            301, // (Powerup) HardBall
            701  // (Hazard) Fog of War
        );
    }

    public static void BallsLostCancel() {
        CancelEvents(
            201, // (Powerup) Wide
            301  // (Powerup) DoubleSize
        );
    }
}
