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
        gameRunning.EndGame(true);
        gameRunning.EndGame(false);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Space);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.P);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.R);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Up);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Down);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Enter);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Space);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.P);
        gameRunning.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.R);
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