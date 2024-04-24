namespace Breakout.LevelHandling;
public class LevelMeta {
    public string LevelName;
    public int TimeLimit;
    public char HardenedChar;
    public char UnbreakableChar;
    public char PowerupChar;

    public LevelMeta() {
        LevelName = "";
        TimeLimit = -1;
        HardenedChar = '-';
        UnbreakableChar = '-';
        PowerupChar = '-';
    }
}