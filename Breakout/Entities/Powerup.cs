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
        Shape.Position.Y += Shape.AsDynamicShape().Direction.Y;
    }

    public IPowerupEffect Pop() {
        DeleteEntity();
        return effect;
    }
}
