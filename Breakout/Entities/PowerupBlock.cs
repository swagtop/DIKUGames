using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Entities;
public class PowerUpBlock : Block{
    public PowerUpBlock(IBaseImage image, IBaseImage damagedImage, Shape shape) : base(image, damagedImage, shape){
    }
}