namespace Breakout.Entities.Blocks;

/// <summary>
/// Each of the implemented Block types. If a new block is implemented, it should have an entry in
/// this enum.
/// </summary>
public enum BlockType {
    Block,
    HardenedBlock,
    UnbreakableBlock,
    PowerupBlock
}

/// <summary>
/// This is used for transforming between the string representation and enum representation of a
/// blocktype. Should be used when parsing level meta information.
/// </summary>
public static class BlockTypeTransformer {
    public static BlockType TransformStringToType(string type) {
        switch (type) {
            case "BLOCK":
                return BlockType.Block;
            case "UNBREAKABLE":
                return BlockType.UnbreakableBlock;
            case "HARDENED":
                return BlockType.HardenedBlock;
            case "POWERUP":
                return BlockType.PowerupBlock;
            default:
                throw new ArgumentException($"Unrecognized BlockType: {type}");
        }
    }

    public static string TransformTypeToString(BlockType type) {
        switch (type) {
            case BlockType.Block:
                return "BLOCK";
            case BlockType.UnbreakableBlock:
                return "UNBREAKABLE";
            case BlockType.HardenedBlock:
                return "HARDENED";
            case BlockType.PowerupBlock:
                return "POWERUP";
            default:
                throw new ArgumentException($"Unrecognized BlockType: {type}");
        }
    }
}
