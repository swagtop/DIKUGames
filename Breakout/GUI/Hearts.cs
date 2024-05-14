namespace Breakout.GUI;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class Hearts {
    private uint startAmount;
    private IBaseImage image;
    private IBaseImage emptyImage;
    private Shape renderShape = new StationaryShape(
        new Vec2F(0.777f, 0.3f),
        new Vec2F(0.05f, 0.05f)
    );
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
        float originalX = renderShape.Position.X;
        for (int i = 0; i < startAmount; i++) {
            renderShape.Position.X += (float)i * 0.05f;
            if (i < Amount) {
                image.Render(renderShape);
            } else {
                emptyImage.Render(renderShape);
            }
        }
        renderShape.Position.X = originalX;
    }
}
