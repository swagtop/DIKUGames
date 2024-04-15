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
        float newX = Shape.Position.X + Shape.AsDynamicShape().Direction.X;
        float newY = Shape.Position.Y + Shape.AsDynamicShape().Direction.Y;
        if (newX > 1.0f) {
            Shape.Position.X = 1.0f - (newX - 1.0f);
            Shape.AsDynamicShape().ChangeDirection(
                new Vec2F(
                    Shape.AsDynamicShape().Direction.X * -1.0f,
                    Shape.AsDynamicShape().Direction.Y
            ));
        } else if (newX < 0.0f) {
            Shape.Position.X = -newX;
            Shape.AsDynamicShape().ChangeDirection(
                new Vec2F(
                    Shape.AsDynamicShape().Direction.X * -1.0f,
                    Shape.AsDynamicShape().Direction.Y
            ));
        } else {
            Shape.Position.X = newX;
        }
        if (newY > 1.0f) {
            Shape.Position.Y = 1.0f - (newY - 1.0f);
            Shape.AsDynamicShape().ChangeDirection(
                new Vec2F(
                    Shape.AsDynamicShape().Direction.X,
                    Shape.AsDynamicShape().Direction.Y * -1.0f
            ));
        } else if (newY < 0.0f) {
            Shape.Position.Y = -newY;
            Shape.AsDynamicShape().ChangeDirection(
                new Vec2F(
                    Shape.AsDynamicShape().Direction.X,
                    Shape.AsDynamicShape().Direction.Y * -1.0f
            ));
        } else {
            Shape.Position.Y = newY;
        }
    }
}