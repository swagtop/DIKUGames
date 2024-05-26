namespace Breakout.Entities;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Utilities;
using Breakout.PowerupEffects;

public static class PowerupFactory {
    public static Powerup CreatePowerup(Vec2F position, PowerupEffectType effectType) {
        Vec2F spawnPosition = position;
        spawnPosition.X += RandomGenerator.Generator.NextSingle() * 0.05f;

        Vec2F extent = new Vec2F(0.05f, 0.05f);

        Vec2F direction = new Vec2F(0.0f, -0.002f);

        switch (effectType) {
            case PowerupEffectType.Split:
                return new Powerup(
                    new Image(Path.Combine("Assets", "Images", "SplitPowerUp.png")),
                    new DynamicShape(spawnPosition, extent, direction),
                    new Split()
                );
            case PowerupEffectType.Wide:
                return new Powerup(
                    new Image(Path.Combine("Assets", "Images", "WidePowerUp.png")),
                    new DynamicShape(spawnPosition, extent, direction),
                    new Wide()
                );
            case PowerupEffectType.DoubleSize:
                return new Powerup(
                    new Image(Path.Combine("Assets", "Images", "BigPowerUp.png")),
                    new DynamicShape(spawnPosition, extent, direction),
                    new DoubleSize()
                );
            default:
                throw new Exception("Powerup type not implemented.");
        }
    }

    public static Powerup CreateRandomPowerup(Vec2F position) {
        PowerupEffectType[] powerups = (PowerupEffectType[])Enum.GetValues(typeof(PowerupEffectType));
        int randomIndex = RandomGenerator.Generator.Next(powerups.Length);
        
        Powerup randomPowerup = CreatePowerup(position, powerups[randomIndex]);
        
        return randomPowerup;
    }
}
