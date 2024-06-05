namespace Breakout.Entities.Blocks;

using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Utilities;

/// <summary>
/// This is the Base Block class. It should be the default block that is created when a level
/// doesn't specify a specific type of block to be created. This class is made and meant to be
/// extended by the other special blocks. 
/// The Hit() method should be called when a ball collides with the block, which decrements its 
/// Health value. Changing the Health value should only be done through the Hit() method.
/// The block deletes itself should its Health field be set to zero or below.
/// </summary>
public class Block : Entity {
    private IBaseImage damagedImage;
    private int maxHealth;
    private int health;
    public int Health {
        get => health;
        set {
            if (value <= maxHealth / 2 && health > maxHealth / 2) { Image = damagedImage; }
            if (value > 0) { health = value; }
            else this.DeleteEntity();
        }
    }

    public Block(IBaseImage image, IBaseImage damagedImage, Shape shape, int maxHealth = 1) : base(shape, image) {
        this.damagedImage = damagedImage;
        this.maxHealth = maxHealth;
        this.health = this.maxHealth;
    }

    public virtual bool Hit() {
        Health -= 1;

        if (this.GetType().IsSubclassOf(typeof(Block))) {
            return this.IsDeleted();
        }

        if (this.IsDeleted()) {
            int randomInt = RandomGenerator.Generator.Next(0, 100);
            
            if (randomInt < 15) {
            BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.StatusEvent,
                Message = "SPAWN_HAZARD",
                ObjectArg1 = (object)Shape.Position
            });}
        }

        return this.IsDeleted();
    }
}
