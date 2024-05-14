namespace Breakout.GUI;

using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

public class Heart {
    private bool value;
    private IBaseImage image;
    private IBaseImage emptyImage;
    private Shape shape;

    public bool Value {
        get => value;
        set {
            this.value = value;
            if (!value) {
                image = emptyImage;
            }
        }
    }

    public Heart(Shape shape, IBaseImage image, IBaseImage emptyImage) {
        this.image = image;
        this.emptyImage = emptyImage;
        this.value = value;
    }

    public void RenderHeart() {
        image.Render(shape);
    }
}

