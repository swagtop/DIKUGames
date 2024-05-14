namespace Breakout.GUI;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class Hearts {
    private uint startAmount;
    private IBaseImage image;
    private IBaseImage emptyImage;
    public uint Amount;

    public Hearts(uint startAmount, IBaseImage image, IBaseImage emptyImage) {
        this.startAmount = startAmount;
        this.Amount = this.startAmount;
        this.image = image;
        this.emptyImage = emptyImage;
    }

    public bool BreakHeart() {
        Amount -= 1;
        return (Amount > 0);
    }

    public void RenderHearts() {
        for (int i = 0; i < startAmount; i++) {
            if (i < Amount) {
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

