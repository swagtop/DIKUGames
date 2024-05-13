﻿namespace Breakout; 

using System;
using DIKUArcade.GUI;

public class Program {
    static void Main(string[] args) {
        var windowArgs = new WindowArgs() { Title = "Breakout v2" };
        var game = new Game(windowArgs);
        game.Run();
    }
}
