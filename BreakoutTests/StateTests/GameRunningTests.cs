// Issues with running tests on this class.

/*
namespace BreakoutTests;

using NUnit.Framework;
using DIKUArcade.Input;
using DIKUArcade.State;
using DIKUArcade.Math;
using DIKUArcade.Events;
using Breakout.GameStates;

public class GameRunningTests {
    private GameRunning gameRunning = GameRunning.GetInstance();

    [Test]
    public void MethodsDontThrowExceptionTest() {
        GameRunning.GetInstance();

        gameRunning.ResetState();
        gameRunning.UpdateState();
        gameRunning.RenderState();
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
        Assert.Pass();
    }
}
*/