namespace Breakout.HazardEffects;

using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using Breakout.Entities;

public interface IHazardEffect {
    void EngageHazard(EntityContainer<Ball> balls, Player player);
}
