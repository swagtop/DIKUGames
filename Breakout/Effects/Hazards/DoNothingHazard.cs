namespace Breakout.Effects.Hazards;

using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using Breakout.Entities;

public class DoNothingHazard : HazardEffect, IEffect {
    public void EngageEffect(EntityContainer<Ball> balls, Player player) {}
    public void DisengageEffect(EntityContainer<Ball> balls, Player player) {}
}

