using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Entities;
public class Block : Entity {
    private IBaseImage damagedImage;
    private int maxHitpoints;
    private int hitpoints;
    public int HitPoints {
        get => hitpoints;
        set {
            if (value == maxHitpoints/2) { Image = damagedImage; } 
            if (value > 0) { hitpoints = value; }
            else { this.DeleteEntity(); }
        }
    }
    public Block(int startHitpoints, IBaseImage normal, IBaseImage damaged, StationaryShape shape) : base(shape, normal) {
        damagedImage = damaged;
        maxHitpoints = startHitpoints;
        hitpoints = maxHitpoints;
    }
}