namespace Breakout.LevelHandling;

using DIKUArcade.Entities;
using Breakout.Entities;

public class Level {
    public LevelMeta Meta;
    public EntityContainer<Block> Blocks;

    public Level() {
        Meta = new LevelMeta(); 
        Blocks = new EntityContainer<Block>();
    }

    public Level(LevelMeta meta, EntityContainer<Block> blocks) {
        Meta = meta; 
        Blocks = blocks;
    }
}