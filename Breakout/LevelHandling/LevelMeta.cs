namespace Breakout.LevelHandling;
public class LevelMeta {
    public string LevelName;
    public int TimeLimit;
    public char HardenedChar;
    public char UnbreakableChar;
    public char PowerUpChar;

    public LevelMeta() {
        LevelName = "";
        TimeLimit = -1;
        HardenedChar = '-';
        UnbreakableChar = '-';
        PowerUpChar = '-';
    }

    public LevelMeta(string levelName, int timeLimit, char hardenedChar, char unbreakableChar, char powerUpChar) {
        LevelName = levelName;
        TimeLimit = timeLimit;
        HardenedChar = hardenedChar;
        UnbreakableChar = unbreakableChar;
        PowerUpChar = powerUpChar;
    }
}