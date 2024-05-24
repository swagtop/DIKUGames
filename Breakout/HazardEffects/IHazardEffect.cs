namespace Breakout.HazardEffects;

using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using Breakout.Entities;

public interface IHazardEffect {
    void EngagePowerup(EntityContainer<Ball> balls, Player player);
}
