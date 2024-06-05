namespace Breakout.Entities.Blocks;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;

/// <summary>
/// The HardenedBlock is almost identical to the block, except it has double its health, and
/// doesn't have a chance to spawn hazard effect when broken.
/// </summary>
public class HardenedBlock : Block {
    public HardenedBlock(IBaseImage image, IBaseImage damagedImage, Shape shape) : base(image, damagedImage, shape, 2) {}
}
