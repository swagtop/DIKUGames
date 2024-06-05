namespace Breakout.Entities.Blocks;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

/// <summary>
///
/// </summary>
public static class BlockFactory {
    public static Block CreateBlock(IBaseImage normalImage, IBaseImage damagedImage, BlockType blockType, int i, int j) {
        int maxNumberOfBlocksInRow = 12;
        float xRatio = 1.0f / maxNumberOfBlocksInRow;
        float yRatio = xRatio / 3.0f;
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
