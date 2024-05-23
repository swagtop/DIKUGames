namespace BreakoutTests;

using NUnit.Framework;
using DIKUArcade.Input;
using DIKUArcade.State;
using DIKUArcade.Math;
using DIKUArcade.Events;
using Breakout.GameStates;

public class GameRunningTests {
    private GameRunning gameRunning;

    [OneTimeSetUp]
    public void Init() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        gameRunning = GameRunning.GetInstance();
    }

    [Test]
    public void MethodsDontThrowExceptionTest() {
        GameRunning.GetInstance();

        gameRunning.ResetState();
        gameRunning.UpdateState();
        gameRunning.RenderState();
        gameRunning.IterateBalls();
        gameRunning.FlushQueue();
        gameRunning.EndLevel();
        gameRunning.EndGame();
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
        Assert.Pass();
    }

    [Test]
    public void UpdateStateManyTimesTest() {
        for (int i = 0; i < 1000; i++) {
            gameRunning.UpdateState();
        }
        Assert.Pass();
    }
}