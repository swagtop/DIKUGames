using DIKUArcade.Entities;
using Breakout.Entities;

namespace Breakout.LevelHandling;
public class LevelData {
    public string LevelName;
    public int TimeLimit;
    public EntityContainer<Block> Blocks;

    public LevelData() {
        LevelName = "";
        TimeLimit = -1;
        Blocks = new EntityContainer<Block>();
    }
}