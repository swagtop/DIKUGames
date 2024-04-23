using DIKUArcade.Entities;
using Breakout.Entities;

namespace Breakout.LevelHandling;
public class Level {
    public LevelMetadata Metadata;
    public EntityContainer<Block> Blocks;

    public Level() {
        Metadata = new LevelMetadata(); 
        Blocks = new EntityContainer<Block>();
    }

    public Level(LevelMetadata metadata, EntityContainer<Block> blocks) {
        Metadata = metadata; 
        Blocks = blocks;
    }
}