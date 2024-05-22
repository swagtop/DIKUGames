namespace Breakout.GUI;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class Background {
    private IBaseImage image;
    private Shape shape;

    public Background(IBaseImage image) {
        this.image = image;
        this.shape = new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f));
    }

    public void RenderBackground() {
        image.Render(shape);
    }
}
