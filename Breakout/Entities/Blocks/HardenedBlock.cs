namespace Breakout.Entities.Blocks;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;

/// <summary>
/// The HardenedBlock is almost identical to the block, except it has double its health, and
/// doesn't have a chance to spawn hazard effect when broken.
/// </summary>
public class HardenedBlock : Block {
    /// <summary> Constructor that asks to have a maxHealth of 2 of base Block class </param>
    public HardenedBlock(IBaseImage image, IBaseImage damagedImage, Shape shape) : base(image, damagedImage, shape, 2) {}
}
