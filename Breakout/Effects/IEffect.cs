namespace Breakout.Effects;

using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using Breakout.Entities;

public interface IEffect {
    void EngageEffect(EntityContainer<Ball> balls, Player player);
    void DisengageEffect(EntityContainer<Ball> balls, Player player);
}
