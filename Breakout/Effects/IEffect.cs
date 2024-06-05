namespace Breakout.Effects;

using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using Breakout.Entities;

/// <summary>
/// The basic interface for effects, such that PowerupEffect and HazardEffects can be stored in
/// the same places.
/// </summary>
public interface IEffect {
    void EngageEffect(EntityContainer<Ball> balls, Player player);
    void DisengageEffect(EntityContainer<Ball> balls, Player player);
}
