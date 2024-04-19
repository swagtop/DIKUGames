using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Entities;
public class Block : Entity {
    private IBaseImage damagedImage;
    private bool unbreakable;
    private int maxHitpoints = 2;
    private int hitpoints;
    public int HitPoints {
        get => hitpoints;
        set {
            if (unbreakable) { return; } // Hitpoints cannot be set if block is unbreakable.
            if (value <= maxHitpoints/2 && hitpoints > maxHitpoints/2) { Image = damagedImage; }
            if (value > 0) { hitpoints = value; }
            else { this.DeleteEntity(); }
        }
    }
    public Block(Image image, Image damagedImage, Shape shape, bool hardened,
                bool unbreakable) : base(shape, image) {
        this.damagedImage = damagedImage;
        this.unbreakable = unbreakable;
        if (hardened) { this.maxHitpoints *= 2; }
        this.hitpoints = this.maxHitpoints;
    }
}