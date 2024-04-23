using DIKUArcade.Entities;
using Breakout.Entities;

namespace Breakout.LevelHandling;
public class LevelMetadata {
    public string LevelName;
    public int TimeLimit;
    public char HardenedChar;
    public char UnbreakableChar;
    public char PowerupChar;

    public LevelMetadata() {
        LevelName = "";
        TimeLimit = -1;
        HardenedChar = '-';
        UnbreakableChar = '-';
        PowerupChar = '-';
    }
}