namespace Breakout.Entities;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;

/// <summary>
/// The Ball class represents the 'ball' in the Breakout game. This ball can move, and change 
/// direction. This behaviour should however be handled by the BallIterator.
/// </summary>
public class Ball : Entity {
    public DynamicShape Dynamic;
    public bool IsHard = false;

    public Ball(IBaseImage image, DynamicShape shape) : base(shape, image){
        Dynamic = shape.AsDynamicShape();
    }

    /// <summary> 
    /// Moves ball, flips direction if it is going out of bounds. Deletes if out of bounds below.
    ///</summary>
    public void Move() {
        bool belowLowerBound = (Shape.Position.Y + Dynamic.Direction.Y < 0.0f - Shape.Extent.Y);
        
        if (belowLowerBound) { this.DeleteEntity(); return; } // Early return for optimization

        bool withinLeftBound = !(Shape.Position.X + Dynamic.Direction.X < 0.0f);
        bool withinRightBound = !(Shape.Position.X + Dynamic.Direction.X > 1.0f - Shape.Extent.X);
        bool withinUpperBound = !(Shape.Position.Y + Dynamic.Direction.Y > 1.0f - Shape.Extent.Y);

        if (withinLeftBound && withinRightBound) { Shape.Position.X += Dynamic.Direction.X; } 
        else { Dynamic.ChangeDirection(new Vec2F(-Dynamic.Direction.X, Dynamic.Direction.Y)); }

        if (withinUpperBound) { Shape.Position.Y += Dynamic.Direction.Y; } 
        else { Dynamic.ChangeDirection(new Vec2F(Dynamic.Direction.X, -Dynamic.Direction.Y)); }
    }

    /// <summary> Flips direction based on input direction </summary>
    /// <param name="direction"> Direction given by collision detection data. </param>
    public void ChangeDirection(CollisionDirection direction) {
        switch (direction) {
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

    /// <summary> Creates exact copy of ball in a different instance. </summary>
    public Ball Clone() {
        return new Ball(
            this.Image, 
            new DynamicShape(
                this.Shape.Position.Copy(), 
                this.Shape.Extent, 
                this.Dynamic.Direction.Copy()
        ));
    }
}
