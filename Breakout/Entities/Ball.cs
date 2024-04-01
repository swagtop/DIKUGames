using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Entities;
public class Ball : Entity {
    public Ball(IBaseImage ballImage, DynamicShape shape) : base(shape, ballImage) {
        Image = ballImage;
    }

    public void Move() {
    }
}