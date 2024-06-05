namespace Breakout.LevelHandling;

/// <summary>
/// This is a simple struct used in the construction of Levels loaded from level files.
/// Should only be created and used within the LevelFactory.
/// </summary>
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
