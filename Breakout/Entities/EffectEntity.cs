namespace Breakout.Entities;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Effects;

/// <summary>
/// The EffectEntity should contain an IEffect instance. This entity should have a visual
/// representation of which effect it contains, and whould be popped when colliding with the
/// player entity. When popped it returns the IEffect it contains, which should then be activated.
/// </summary>
public class EffectEntity : Entity {
    public readonly IEffect Effect;

    public EffectEntity(IBaseImage image, DynamicShape shape, IEffect effect) : base(shape, image) {
        Effect = effect;
    }

    public void Move() {
        Shape.Move();

        bool belowLowerBorder = (Shape.Position.X < 0.0f - Shape.Extent.X);
        if (belowLowerBorder) { DeleteEntity(); }
    }

    public IEffect Pop() {
        DeleteEntity();
        return Effect;
    }

    /// <summary> Checks if there is a collision with an entity </summary>
    /// <param name="entity"> Any entity, but usually the player </param>
    public bool CollidesWith(Entity entity) {
        float effectLeftSide = Shape.Position.X;
        float effectRightSide = Shape.Position.X + Shape.Extent.X;
        float effectLowerSide = Shape.Position.Y;

        float entityRightSide = entity.Shape.Position.X + Shape.Extent.X;
        float entityLeftSide = entity.Shape.Position.X;
        float entityUpperSide = entity.Shape.Position.Y + Shape.Extent.Y;

        return (
            effectLeftSide < entityRightSide &&
            effectRightSide > entityLeftSide &&
            effectLowerSide < entityUpperSide
        );
    }
}
