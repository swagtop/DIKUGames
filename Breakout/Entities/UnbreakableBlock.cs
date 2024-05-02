using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Entities;
public class UnbreakableBlock : Block {

    public UnbreakableBlock(IBaseImage image, IBaseImage damagedImage, Shape shape) : base(image, damagedImage, shape) {
    }

    public override bool Hit() {
        return false;
    }
}