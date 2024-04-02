using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Entities;
public class Block : Entity {
    private IBaseImage damagedImage;
    private bool unbreakable;
    private int maxHitpoints;
    private int hitpoints;
    public int HitPoints {
        get => hitpoints;
        set {
            if (unbreakable) { return; }
            if (value == maxHitpoints/2) { Image = damagedImage; } 
            if (value > 0) { hitpoints = value; }
            else { this.DeleteEntity(); }
        }
    }
    public Block(int maxHp, IBaseImage normal, IBaseImage damaged, StationaryShape shape, bool unbreakable=false) : base(shape, normal) {
        damagedImage = damaged;
        unbreakable = unbreakable;
        maxHitpoints = maxHp;
        hitpoints = maxHp;
    }
}