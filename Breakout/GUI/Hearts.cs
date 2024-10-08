namespace Breakout.GUI;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

/// <summary>
/// The Hearts class is both responsible for rendering the player lives, and keeping track of the
/// amount of lives the player has left.
/// </summary>
public class Hearts {
    private uint startAmount;
    private IBaseImage fullContainerImage = new Image(
        Path.Combine("Assets", "Images", "heart_filled.png")
    );
    private IBaseImage emptyContainerImage = new Image(
        Path.Combine("Assets", "Images", "heart_empty.png")
    );
    private Shape renderingShape = new StationaryShape(
        new Vec2F(0.95f, 0.3f), new Vec2F(0.05f, 0.05f)
    );
    public uint Amount { get; private set; }

    public Hearts() {
        this.startAmount = 0;
        this.Amount = this.startAmount;
    }

    public Hearts(uint startAmount) {
        this.startAmount = startAmount;
        this.Amount = this.startAmount;
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
    
    public void MendHeart() {
        if (Amount + 1 > startAmount) return;
        
        Amount += 1;
    }

    public void ResetHearts() {
        Amount = startAmount;
    }

    /// <summary> Renders each heart, based on maximum and current values of lives. </summary>
    public void RenderHearts() {
        float originalX = renderingShape.Position.X;
        for (int i = 0; i < startAmount; i++) {
            renderingShape.Position.X -= 0.05f;
            if (i < Amount) {
                fullContainerImage.Render(renderingShape);
            } else {
                emptyContainerImage.Render(renderingShape);
            }
        }
        renderingShape.Position.X = originalX;
    }
}
