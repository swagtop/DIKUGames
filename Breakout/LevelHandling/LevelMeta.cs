using System.Collections.Generic;
using Breakout.Entities;

namespace Breakout.LevelHandling;
public class LevelMeta {
    public string LevelName;
    public int TimeLimit;
    public char HardenedChar;
    public char UnbreakableChar;
    public char PowerUpChar;
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