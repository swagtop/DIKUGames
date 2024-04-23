using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Entities;
public class Block : Entity {
    private IBaseImage damagedImage;
    private int value;
    private int maxHealth = 2;
    private int health;
    private bool hardened;
    private bool unbreakable;
    
    public int Health {
        get => health;
        set {
            if (unbreakable) { return; } // Hitpoints cannot be set if block is unbreakable.
            if (value <= maxHealth/2 && health > maxHealth/2) { Image = damagedImage; }
            if (value > 0) { health = value; }
            else { this.DeleteEntity(); }
        }
    }

    public Block(Image image, Image damagedImage, Shape shape, bool hardened, bool unbreakable, int value=0) : base(shape, image) {
        this.damagedImage = damagedImage;
        this.value = value;
        this.unbreakable = unbreakable;
        if (hardened) { this.maxHealth *= 2; }
        this.health = this.maxHealth;
    }
}