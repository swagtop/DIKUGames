namespace Breakout.Entities;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;

public class Block : Entity {
    private IBaseImage damagedImage;
    private int maxHealth;
    private int health;
    public int Health {
        get => health;
        set {
            if (value <= maxHealth / 2 && health > maxHealth / 2) Image = damagedImage;
            if (value > 0) health = value;
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
        return this.IsDeleted();
    }
}
