namespace Breakout.Entities.Blocks;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;

/// <summary>
///
/// </summary>
public class UnbreakableBlock : Block {
    public UnbreakableBlock(IBaseImage image, IBaseImage damagedImage, Shape shape) : base(image, damagedImage, shape) {}

    public override bool Hit() {
        return false;
    }
}
