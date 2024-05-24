namespace Breakout.PowerupEffects;

using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using Breakout.Entities;

public interface IPowerupEffect {
    void EngagePowerup(EntityContainer<Ball> balls, Player player);
}
