namespace Breakout.Entities.Blocks;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

/// <summary>
/// Creates a block based on two coordinates, the i and j integers, and a BlockType. 
/// The integers should be derived from the level files.
/// </summary>
public static class BlockFactory {
    private static int maxNumberOfBlocksInRow = 12;
    private static float xRatio = 1.0f / maxNumberOfBlocksInRow;
    private static float yRatio = xRatio / 3.0f;

    /// <summary> Creates block based on images and position data </summary>
    /// <param name="normalImage"> Image for full health block </param>
    /// <param name="damagedImage"> Image for half health or below block </param>
    /// <param name="blockType"> BlockType enum value </param>
    /// <param name="i"> Block coordinate found in level file </param>
    /// <param name="j"> Block coordinate found in level file </param>
    public static Block CreateBlock(IBaseImage normalImage, IBaseImage damagedImage, BlockType blockType, int i, int j) {
        Shape shape = new StationaryShape(
            new Vec2F(j * xRatio, 1.0f - ((i + 1) * yRatio)),
            new Vec2F(xRatio, yRatio)
        );

        switch (blockType) {
            case BlockType.Block:
                return new Block(normalImage, damagedImage, shape);
            case BlockType.HardenedBlock:
                return new HardenedBlock(normalImage, damagedImage, shape);
            case BlockType.UnbreakableBlock:
                return new UnbreakableBlock(normalImage, damagedImage, shape);
            case BlockType.PowerupBlock:
                return new PowerupBlock(normalImage, damagedImage, shape);
            default:
                throw new Exception("Block type not implemented");
        }
    }
}
