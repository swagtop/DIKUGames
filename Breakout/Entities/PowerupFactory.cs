namespace Breakout.Entities;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.PowerupEffects;

public static class PowerupFactory {
    public static Powerup CreatePowerup(Vec2F poisition, PowerupEffectType effectType) {
        switch (effectType) {
            case PowerupEffectType.Split:
                return new Powerup(
                    new Image(Path.Combine("Assets", "Images", "SplitPowerUp.png")),
                    new DynamicShape(poisition, new Vec2F(0.05f, 0.05f), new Vec2F(0.0f, -0.002f)),
                    new Split()
                );
            case PowerupEffectType.Wide:
                return new Powerup(
                    new Image(Path.Combine("Assets", "Images", "WidePowerUp.png")),
                    new DynamicShape(poisition, new Vec2F(0.05f, 0.05f), new Vec2F(0.0f, -0.002f)),
                    new Wide()
                );
            default:
                throw new Exception("Powerup type not implemented.");
        }
    }
}
