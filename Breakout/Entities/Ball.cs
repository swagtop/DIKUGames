using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Entities;
public class Ball : Entity {
    public Ball(IBaseImage image, DynamicShape shape) : base(shape, image) {
    }

    public void Move() {
        Shape.Move();
    }
}