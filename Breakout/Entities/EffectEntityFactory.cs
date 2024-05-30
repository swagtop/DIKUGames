namespace Breakout.Entities;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Utilities;
using Breakout.Effects;
using Breakout.Effects.Powerups;
using Breakout.Effects.Hazards;

public static class EffectEntityFactory {
    private static readonly Vec2F defaultExtent = new Vec2F(0.05f, 0.05f);
    private static readonly Vec2F defaultDirection = new Vec2F(0.0f, -0.002f);

    public static EffectEntity CreatePowerup(Vec2F position, PowerupEffectType effectType) {
        Vec2F spawnPosition = position;
        spawnPosition.X += RandomGenerator.Generator.NextSingle() * 0.05f;

        Vec2F extent = defaultExtent.Copy();
        Vec2F direction = defaultDirection.Copy();

        switch (effectType) {
            case PowerupEffectType.Split:
                return new EffectEntity(
                    new Image(Path.Combine("Assets", "Images", "SplitPowerUp.png")),
                    new DynamicShape(spawnPosition, extent, direction),
                    new Split()
                );
            case PowerupEffectType.Wide:
                return new EffectEntity(
                    new Image(Path.Combine("Assets", "Images", "WidePowerUp.png")),
                    new DynamicShape(spawnPosition, extent, direction),
                    new Wide()
                );
            case PowerupEffectType.DoubleSize:
                return new EffectEntity(
                    new Image(Path.Combine("Assets", "Images", "BigPowerUp.png")),
                    new DynamicShape(spawnPosition, extent, direction),
                    new DoubleSize()
                );
            case PowerupEffectType.HardBall:
                return new EffectEntity(
                    new Image(Path.Combine("Assets", "Images", "ToughenUp.png")),
                    new DynamicShape(spawnPosition, extent, direction),
                    new HardBall()
                );
            case PowerupEffectType.ExtraLife:
                return new EffectEntity(
                    new Image(Path.Combine("Assets", "Images", "LifePickUp.png")),
                    new DynamicShape(spawnPosition, extent, direction),
                    new ExtraLife()
                );
            default:
                throw new Exception("Powerup type not implemented.");
        }
    }

    public static EffectEntity CreateRandomPowerup(Vec2F position) {
        PowerupEffectType[] powerups = (PowerupEffectType[])Enum.GetValues(typeof(PowerupEffectType));
        int randomIndex = RandomGenerator.Generator.Next(powerups.Length);
        
        EffectEntity randomPowerup = CreatePowerup(position, powerups[randomIndex]);
        
        return randomPowerup;
    }

    public static EffectEntity CreateHazard(Vec2F position, HazardEffectType effectType) {
        Vec2F spawnPosition = position;
        spawnPosition.X += RandomGenerator.Generator.NextSingle() * 0.05f;

        Vec2F extent = defaultExtent.Copy();
        Vec2F direction = defaultDirection.Copy();

        switch (effectType) {
            case HazardEffectType.DoNothingHazard:
                return new EffectEntity(
                    new Image(Path.Combine("Assets", "Images", "Ghost.png")),
                    new DynamicShape(spawnPosition, extent, direction),
                    new DoNothingHazard()
                );
            default:
                throw new Exception("Hazard type not implemented.");
        }
    }

    public static EffectEntity CreateRandomHazard(Vec2F position) {
        HazardEffectType[] hazards = (HazardEffectType[])Enum.GetValues(typeof(HazardEffectType));
        int randomIndex = RandomGenerator.Generator.Next(hazards.Length);
        
        EffectEntity randomHazard = CreateHazard(position, hazards[randomIndex]);
        
        return randomHazard;
    }
}
