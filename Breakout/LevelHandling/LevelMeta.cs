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

    public LevelMeta(string levelName, int timeLimit, char hardenedChar, char unbreakableChar, char powerupChar) {
        LevelName = levelName;
        TimeLimit = timeLimit;
        HardenedChar = hardenedChar;
        UnbreakableChar = unbreakableChar;
        PowerupChar = powerupChar;
    }
}