namespace Breakout.Effects;

using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using Breakout.Entities;

/// <summary>
/// An abstract class, made such that 'effect is HazardEffect' behaviour can exist.
/// </summary>
public abstract class HazardEffect : IEffect {
    public abstract void EngageEffect(EntityContainer<Ball> balls, Player player);
    public abstract void DisengageEffect(EntityContainer<Ball> balls, Player player);
}
