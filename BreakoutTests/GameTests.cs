namespace BreakoutTests;

using System;
using NUnit.Framework;
using DIKUArcade;
using DIKUArcade.Input;
using DIKUArcade.GUI;
using DIKUArcade.State;
using DIKUArcade.Math;
using DIKUArcade.Events;
using Breakout;
using Breakout.GameStates;

public class GameTests {
    private WindowArgs windowArgs = new WindowArgs() { Title = "Breakout v3" };
    private Game game;

    [OneTimeSetUp]
    public void Init() {
        DIKUArcade.GUI.Window.CreateOpenGLContext(); 
        game = new Game(windowArgs);
    }

    [Test]
    public void MethodsDontThrowExceptionTest() {
        game.Render();
        game.Update();
        game.ProcessEvent(new GameEvent());
        
        Assert.Pass();
    }

    [Test]
    public void UpdateStateManyTimesTest() {
        for (int i = 0; i < 1000; i++) {
            game.Update();
        }
        Assert.Pass();
    }
}
