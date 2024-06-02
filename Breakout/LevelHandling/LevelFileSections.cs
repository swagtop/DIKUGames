namespace Breakout.LevelHandling;

public struct LevelFileSections {
    public string[] Map;
    public string[] Meta;
    public string[] Legend;

    public LevelFileSections(string[] map, string[] meta, string[] legend) {
        Map = map;
        Meta = meta;
        Legend = legend;
    }
}
