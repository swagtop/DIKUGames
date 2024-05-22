using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Entities;
public class PowerUpBlock : Block{
    public PowerUpBlock(IBaseImage image, IBaseImage damagedImage, Shape shape) : base(image, damagedImage, shape){
    }

    public void ActivatePowerUp (PowerUpType powerUpType) {
        switch (powerUpType) {
            case PowerUpType.PlayerSpeed:
                break;
            case PowerUpType.Wide:
                break;
            case PowerUpType.DoubleSize:
                break;
            case PowerUpType.Split:
                break;
            case PowerUpType.HardBall:
                break;
        }
    }
}
