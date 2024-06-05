namespace Breakout.GUI;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

/// <summary>
/// The Background class simply takes in an image and renders it such that it fills the entire
/// screen. Should be rendered first, such that everything else is rendered on top of it.
/// </summary>
public class Background {
    private IBaseImage image;
    private Shape shape;

    public Background(IBaseImage image) {
        this.image = image;
        this.shape = new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f));
    }

    /// <summary> Renders image of background, filling the whole screen </summary>
    public void RenderBackground() {
        image.Render(shape);
    }
}
