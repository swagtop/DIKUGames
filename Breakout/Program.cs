using System;
using DIKUArcade.GUI;

namespace Breakout; 
public class Program {
    static void Main(string[] args) {
        var windowArgs = new WindowArgs() { Title = "Breakout v1" };
        var game = new Game(windowArgs);
        game.Run();
    }
}
