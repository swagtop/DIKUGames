namespace Breakout.GUI;

using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class Hearts {
    private uint startAmount;
    private uint amount;
    private IBaseImage image;
    private IBaseImage emptyImage;

    public Hearts(uint startAmount, IBaseImage image, IBaseImage emptyImage) {
        this.startAmount = startAmount;
        this.amount = this.startAmount;
        this.image = image;
        this.emptyImage = emptyImage;
    }

    public void SetHearts(uint amount) {
        this.amount = amount;
    }

    public uint GetHearts() {
        return amount;   
    }

    public bool BreakHeart() {
        amount -= 1;
        return (amount > 0);
    }

    public void RenderHearts() {
        for (int i = 0; i < startAmount; i++) {
            if (i < amount) {
                image.Render(new StationaryShape(
                    new Vec2F(0.777f + (float)i * 0.05f, 0.3f), 
                    new Vec2F(0.05f, 0.05f)
                ));
            } else {
                emptyImage.Render(new StationaryShape(
                    new Vec2F(0.777f + (float)i * 0.05f, 0.3f), 
                    new Vec2F(0.05f, 0.05f)
                ));
            }
        }
    }
}

