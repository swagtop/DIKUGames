namespace Breakout.Entities;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Effects;

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

    public bool CollidesWith(Entity entity) {
        float effectLeftSide = Shape.Position.X;
        float effectRightSide = Shape.Position.X + Shape.Extent.X;
        float effectUpperSide = Shape.Position.Y + Shape.Extent.Y;
        float effectLowerSide = Shape.Position.Y;

        float entityLeftSide = entity.Shape.Position.X;
        float entityRightSide = entity.Shape.Position.X + Shape.Extent.X;
        float entityUpperSide = entity.Shape.Position.Y + Shape.Extent.Y;
        float entityLowerSide = entity.Shape.Position.Y;

        return (
            effectLeftSide < entityRightSide &&
            effectRightSide > entityLeftSide &&
            effectLowerSide < entityUpperSide &&
            effectUpperSide > entityLowerSide 
        );
    }
}
