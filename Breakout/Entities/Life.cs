using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Entities;
public class Life : Entity
{
    private bool value;
    private int counter;
    private IBaseImage emptyImage;

    public virtual bool Value
    {
        get => value;
        set
        {
            this.value = value;
            if (!value)
            {
                Image = emptyImage;
            }
        }
    }



    public Life(Shape shape, IBaseImage image, IBaseImage emptyImage, bool value = true)
        : base(shape, image)
    {
        this.emptyImage = emptyImage;
        this.value = value;
    }
    // public void LoseLife()
    // {
    //     value = false;
    //     emptyImage

    // }
}

