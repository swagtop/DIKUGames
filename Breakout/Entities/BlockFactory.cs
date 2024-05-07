using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;
namespace Breakout.Entities;

public static class BlockFactory
{
    public static Block CreateBlock(IBaseImage normalImage, IBaseImage damagedImage, BlockType blockType, int i, int j)
    {
        int maxBlockRows = 30;
        int maxNumberOfBlocksInRow = 12;
        float xRatio = 1.0f / maxNumberOfBlocksInRow;
        float yRatio = xRatio / 3.0f;

        switch (blockType)
        {
            case BlockType.Block:
                return new Block(
                        normalImage,
                        damagedImage,
                        new StationaryShape(
                            new Vec2F(j * xRatio, 1.0f - ((i + 1) * yRatio)),
                            new Vec2F(xRatio, yRatio)
                        )
                    );
                break;
            case BlockType.HardenedBlock:
                return new HardenedBlock(
                        normalImage,
                        damagedImage,
                        new StationaryShape(
                            new Vec2F(j * xRatio, 1.0f - ((i + 1) * yRatio)),
                            new Vec2F(xRatio, yRatio)
                        )
                    );
                break;
            case BlockType.UnbreakableBlock:
                return new UnbreakableBlock(
                        normalImage,
                        damagedImage,
                        new StationaryShape(
                            new Vec2F(j * xRatio, 1.0f - ((i + 1) * yRatio)),
                            new Vec2F(xRatio, yRatio)
                        )
                    );
                break;
            default:
                throw new Exception("BlockType not implementet");
        }
    }

}