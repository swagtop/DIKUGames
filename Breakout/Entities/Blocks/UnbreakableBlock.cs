namespace Breakout.Entities.Blocks;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;

/// <summary>
/// The UnbreakableBlock is unbreakable, and thus overrides the Hit() method to do nothing.
/// </summary>
public class UnbreakableBlock : Block {
    public UnbreakableBlock(IBaseImage image, IBaseImage damagedImage, Shape shape) : base(image, damagedImage, shape) {}

    /// <summary> Does nothing and returns false always, signifying the block survived. </summary>
    public override bool Hit() {
        return false;
    }
}
