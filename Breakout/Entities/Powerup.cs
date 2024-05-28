namespace Breakout.Entities;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.PowerupEffects;

public class Powerup : Entity {
    private IPowerupEffect effect;

    public Powerup(IBaseImage image, DynamicShape shape, IPowerupEffect effect) : base(shape, image) {
        this.effect = effect;
    }

    public void Move() {
        Shape.Move();

        bool belowLowerBorder = (Shape.Position.X < 0.0f - Shape.Extent.X);
        if (belowLowerBorder) { DeleteEntity(); }
    }

    public IPowerupEffect Pop() {
        DeleteEntity();
        return effect;
    }
}
