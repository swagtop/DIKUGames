using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;

namespace Breakout.Entities;
public class Ball : Entity
{
    public DynamicShape Dynamic;
    public Ball(IBaseImage image, DynamicShape shape) : base(shape, image)
    {
        Dynamic = shape.AsDynamicShape();
    }

    public void Move()
    {
        if (!(Shape.Position.X + Dynamic.Direction.X < 0.0f) && !(Shape.Position.X + Dynamic.Direction.X > 1.0f - Shape.Extent.X))
        {
            Shape.Position.X += Dynamic.Direction.X;
        }
        else
        {
            Dynamic.ChangeDirection(new Vec2F(-Dynamic.Direction.X, Dynamic.Direction.Y));
        }
        if (!(Shape.Position.Y + Dynamic.Direction.Y > 1.0f - Shape.Extent.Y))
        {
            Shape.Position.Y += Dynamic.Direction.Y;
        }
        else
        {
            Dynamic.ChangeDirection(new Vec2F(Dynamic.Direction.X, -Dynamic.Direction.Y));
        }
        if (Shape.Position.Y + Dynamic.Direction.Y < 0.0f - Shape.Extent.Y) this.DeleteEntity();
    }
    public void ChangeDirection(CollisionDirection direction)
    {
        switch (direction)
        {
            case CollisionDirection.CollisionDirUp:
            case CollisionDirection.CollisionDirDown:
                Dynamic.ChangeDirection(new Vec2F(Dynamic.Direction.X, -Dynamic.Direction.Y));
                break;
            case CollisionDirection.CollisionDirLeft:
            case CollisionDirection.CollisionDirRight:
                Dynamic.ChangeDirection(new Vec2F(-Dynamic.Direction.X, Dynamic.Direction.Y));
                break;
            default:
                break;
        }
    }

}