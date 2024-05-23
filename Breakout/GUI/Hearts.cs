namespace Breakout.GUI;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class Hearts {
    private uint startAmount;
    private IBaseImage fullContainerImage;
    private IBaseImage emptyContainerImage;
    private Shape renderingShape;
    public uint Amount { get; private set; }

    public Hearts(uint startAmount) {
        this.startAmount = startAmount;
        this.Amount = this.startAmount;
        this.fullContainerImage = new Image(Path.Combine("Assets", "Images", "heart_filled.png"));
        this.emptyContainerImage = new Image(Path.Combine("Assets", "Images", "heart_empty.png"));
        this.renderingShape = new StationaryShape(new Vec2F(0.777f, 0.3f), new Vec2F(0.05f, 0.05f));
    }

    public void SetHearts(uint amount) {
        startAmount = amount;
        Amount = amount;
    }

    public bool BreakHeart() {
        if (Amount == 0) return true;

        Amount -= 1;
        return false;
    }

    public void RenderHearts() {
        float originalX = renderingShape.Position.X;
        for (int i = 0; i < startAmount; i++) {
            renderingShape.Position.X += i * 0.05f;
            if (i < Amount) {
                fullContainerImage.Render(renderingShape);
            } else {
                emptyContainerImage.Render(renderingShape);
            }
        }
        renderingShape.Position.X = originalX;
    }
}
