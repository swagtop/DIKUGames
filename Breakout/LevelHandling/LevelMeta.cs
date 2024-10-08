namespace Breakout.LevelHandling;

using Breakout.Entities;
using Breakout.Entities.Blocks;

/// <summary>
/// This is a simple struct, and should containin the meta info found in level files.
/// Instances of this is used in the creation of Level class instances.
/// </summary>
public struct LevelMeta {
    public string LevelName;
    public int TimeLimit;
    public Dictionary<char, BlockType> CharDictionary;

    public LevelMeta() {
        LevelName = "";
        TimeLimit = -1;
        CharDictionary = new Dictionary<char, BlockType>();
    }

    public LevelMeta(string levelName, int timeLimit, Dictionary<char, BlockType> charDictionary) {
        LevelName = levelName;
        TimeLimit = timeLimit;
        CharDictionary = charDictionary;
    }
}
