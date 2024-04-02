using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Entities;
public class Powerup : Entity {
    public Powerup(IBaseImage image, DynamicShape shape) : base(shape, image) {
        Image = image;
    }

    public void Move() {
    }
}