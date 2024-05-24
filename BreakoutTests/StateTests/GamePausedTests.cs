namespace BreakoutTests;

using System;
using NUnit.Framework;
using DIKUArcade.Input;
using DIKUArcade.State;
using DIKUArcade.Math;
using DIKUArcade.Events;
using Breakout.GameStates;

public class GamePausedTests {
    private GamePaused gamePaused;

    [OneTimeSetUp]
    public void Init() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        gamePaused = GamePaused.GetInstance();
    }

    [Test]
    public void MethodsDontThrowExceptionTest() {
        GamePaused.GetInstance();

        gamePaused.ResetState();
        gamePaused.UpdateState();
        gamePaused.RenderState();
        gamePaused.SelectMenuItem("RESUME_GAME");
        gamePaused.SelectMenuItem("MAIN_MENU");
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.A);
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Left);
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.D);
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Right);
        gamePaused.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.R);
        gamePaused.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Up);
        gamePaused.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Down);
        gamePaused.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Enter);
        gamePaused.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.A);
        gamePaused.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Left);
        gamePaused.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.D);
        gamePaused.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Right);
        gamePaused.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.R);
        
        Assert.Pass();
    }

    [Test]
    public void UpdateStateManyTimesTest() {
        for (int i = 0; i < 1000; i++) {
            gamePaused.UpdateState();
        }
        Assert.Pass();
    }

    [Test]
    public void CorrectExceptionForInvalidMenuItem() {
        try {
            gamePaused.SelectMenuItem("RANDOM_NONEXISTANT_STATE");
        } catch (Exception e) {
            Assert.AreEqual(e.Message.Split(':')[0], "Button number not implemented");
        }
    }
}