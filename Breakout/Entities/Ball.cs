using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Entities;
public class Ball : Entity {
    public Ball(IBaseImage image, DynamicShape shape) : base(shape, image) {
    }

    public void Move() {
        if (!(Shape.Position.X + Shape.AsDynamicShape().Direction.X < 0.0f) && !(Shape.Position.X + Shape.AsDynamicShape().Direction.X > 1.0f - Shape.Extent.X)) {
            Shape.Position.X += Shape.AsDynamicShape().Direction.X;
        } else {
            Shape.AsDynamicShape().ChangeDirection(new Vec2F(-Shape.AsDynamicShape().Direction.X, Shape.AsDynamicShape().Direction.Y));
        }
        if (!(Shape.Position.Y + Shape.AsDynamicShape().Direction.Y > 1.0f - Shape.Extent.Y)) {
            Shape.Position.Y += Shape.AsDynamicShape().Direction.Y;
        } else {
            Shape.AsDynamicShape().ChangeDirection(new Vec2F(Shape.AsDynamicShape().Direction.X, -Shape.AsDynamicShape().Direction.Y));
        }
        if (Shape.Position.Y + Shape.AsDynamicShape().Direction.Y < 0.0f - Shape.Extent.Y) this.DeleteEntity();
    }
}