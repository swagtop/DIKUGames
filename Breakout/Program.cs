namespace Breakout; 

using DIKUArcade.GUI;

/// <summary>
/// The Program class. Starts and runs the game, by intializing the game class.
/// </summary>
public class Program {
    static void Main(string[] args) {
        var windowArgs = new WindowArgs() { Title = "Breakout v4" };
        var game = new Game(windowArgs);
        game.Run();
    }
}
