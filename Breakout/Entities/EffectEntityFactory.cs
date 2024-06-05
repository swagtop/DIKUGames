namespace Breakout.Entities;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Utilities;
using Breakout.Effects;
using Breakout.Effects.Powerups;
using Breakout.Effects.Hazards;

/// <summary>
/// This is where the EffectEntities should be created. The EffectEntityFactory creation methods
/// take in a position and an effect type, which in turn creates an EffectEntity with the correct
/// image corresponding to the effect it contains.
/// This class can also create random instances of effects, based on the entries of effects in
/// their respective enums.
/// </summary>
public static class EffectEntityFactory {
    private static readonly Vec2F defaultExtent = new Vec2F(0.05f, 0.05f);
    private static readonly Vec2F defaultDirection = new Vec2F(0.0f, -0.002f);

    /// <summary> Creates EffectEntity around position given, containing effect of effectType. </summary>
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

    /// <summary> Creates EffectEntity around position given, containing random powerup </summary>
    public static EffectEntity CreateRandomPowerup(Vec2F position) {
        PowerupEffectType[] powerups = (PowerupEffectType[])Enum.GetValues(typeof(PowerupEffectType));
        int randomIndex = RandomGenerator.Generator.Next(powerups.Length);
        
        EffectEntity randomPowerup = CreatePowerup(position, powerups[randomIndex]);
        
        return randomPowerup;
    }

    /// <summary> Creates EffectEntity around position given, containing effect of effectType. </summary>
    public static EffectEntity CreateHazard(Vec2F position, HazardEffectType effectType) {
        Vec2F spawnPosition = position;
        spawnPosition.X += RandomGenerator.Generator.NextSingle() * 0.05f;

        Vec2F extent = defaultExtent.Copy();
        Vec2F direction = defaultDirection.Copy();

        switch (effectType) {
            case HazardEffectType.FogOfWar:
                return new EffectEntity(
                    new Image(Path.Combine("Assets", "Images", "Ghost.png")),
                    new DynamicShape(spawnPosition, extent, direction),
                    new FogOfWar()
                );
            case HazardEffectType.LoseLife:
                return new EffectEntity(
                    new Image(Path.Combine("Assets", "Images", "LoseLife.png")),
                    new DynamicShape(spawnPosition, extent, direction),
                    new LoseLife()
                );
            default:
                throw new Exception("Hazard type not implemented.");
        }
    }

    /// <summary> Creates EffectEntity around position given, containing random hazard </summary>
    public static EffectEntity CreateRandomHazard(Vec2F position) {
        HazardEffectType[] hazards = (HazardEffectType[])Enum.GetValues(typeof(HazardEffectType));
        int randomIndex = RandomGenerator.Generator.Next(hazards.Length);
        
        EffectEntity randomHazard = CreateHazard(position, hazards[randomIndex]);
        
        return randomHazard;
    }
}
