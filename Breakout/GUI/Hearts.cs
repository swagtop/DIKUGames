namespace Breakout.GUI;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class Hearts {
    private uint startAmount;
    private IBaseImage fullContainerImage;
    private IBaseImage emptyContainerImage;
    private Shape heartShape = new StationaryShape(
        new Vec2F(0.777f, 0.3f),
        new Vec2F(0.05f, 0.05f)
    );
    public uint Amount;

    public Hearts(uint startAmount) {
        this.startAmount = startAmount;
        this.Amount = this.startAmount;
        this.fullContainerImage = new Image(Path.Combine("Assets", "Images", "heart_filled.png"));
        this.emptyContainerImage = new Image(Path.Combine("Assets", "Images", "heart_empty.png"));
    }

    public bool BreakHeart() {
        Amount -= 1;
        return (Amount > 0);
    }

    public void RenderHearts() {
        float originalX = heartShape.Position.X;
        for (int i = 0; i < startAmount; i++) {
            heartShape.Position.X += i * 0.05f;
            if (i < Amount) {
                fullContainerImage.Render(heartShape);
            } else {
                emptyContainerImage.Render(heartShape);
            }
        }
        heartShape.Position.X = originalX;
    }
}
