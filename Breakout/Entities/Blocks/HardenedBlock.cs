namespace Breakout.Entities.Blocks;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;

/// <summary>
///
/// </summary>
public class HardenedBlock : Block {
    public HardenedBlock(IBaseImage image, IBaseImage damagedImage, Shape shape) : base(image, damagedImage, shape, 2) {}
}
