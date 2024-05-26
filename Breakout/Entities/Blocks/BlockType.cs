namespace Breakout.Entities.Blocks;

public enum BlockType {
    Block,
    HardenedBlock,
    UnbreakableBlock
}

public static class BlockTypeTransformer {
    public static BlockType TransformStringToType(string type) {
        switch (type) {
            case "BLOCK":
                return BlockType.Block;
            case "UNBREAKABLE":
                return BlockType.UnbreakableBlock;
            case "HARDENED":
                return BlockType.HardenedBlock;
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
            default:
                throw new ArgumentException($"Unrecognized BlockType: {type}");
        }
    }
}
