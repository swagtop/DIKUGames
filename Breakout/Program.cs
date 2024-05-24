﻿namespace Breakout; 

using DIKUArcade.GUI;

public class Program {
    static void Main(string[] args) {
        var windowArgs = new WindowArgs() { Title = "Breakout v3" };
        var game = new Game(windowArgs);
        game.Run();
    }
}
