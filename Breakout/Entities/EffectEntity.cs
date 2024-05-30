namespace Breakout.Entities;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Effects;

public class EffectEntity : Entity {
    private IEffect effect;

    public EffectEntity(IBaseImage image, DynamicShape shape, IEffect effect) : base(shape, image) {
        this.effect = effect;
    }

    public void Move() {
        Shape.Move();

        bool belowLowerBorder = (Shape.Position.X < 0.0f - Shape.Extent.X);
        if (belowLowerBorder) { DeleteEntity(); }
    }

    public IEffect Pop() {
        DeleteEntity();
        return effect;
    }
}
