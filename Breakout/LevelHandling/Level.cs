namespace Breakout.LevelHandling;

using DIKUArcade.Entities;
using Breakout.Entities;
using Breakout.Entities.Blocks;

public class Level {
    public LevelMeta Meta;
    public EntityContainer<Block> Blocks;
    public int BreakableLeft;

    public Level() {
        Meta = new LevelMeta(); 
        Blocks = new EntityContainer<Block>();
        BreakableLeft = 0;
    }

    public Level(LevelMeta meta, EntityContainer<Block> blocks, int breakableLeft) {
        Meta = meta; 
        Blocks = blocks;
        BreakableLeft = breakableLeft;
    }
}
