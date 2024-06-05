namespace Breakout.LevelHandling;

using DIKUArcade.Entities;
using Breakout.Entities;
using Breakout.Entities.Blocks;

/// <summary>
/// The Level class contains all information and objects needed to start a level.
/// Instances of these are used by the GameRunning game state, to make the game playable.
/// These instances are easily hot-swappable, making switching of levels quick and uncomplicated.
/// </summary>
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
