using System;
using DIKUArcade.GUI;

namespace Breakout; 
public class Program {
    static void Main(string[] args) {
        var windowArgs = new WindowArgs() { Title = "Breakout v0.1" };
        var game = new Game(windowArgs);
        game.Run();
        Console.WriteLine("Hello World!"); // TODO: Delete this line!
    }
}
