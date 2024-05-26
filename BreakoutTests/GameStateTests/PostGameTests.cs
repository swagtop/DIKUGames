namespace BreakoutTests;

using System;
using NUnit.Framework;
using DIKUArcade.Input;
using DIKUArcade.State;
using DIKUArcade.Math;
using DIKUArcade.Events;
using Breakout.GameStates;

public class PostGameTests {
    private PostGame postGame;

    [OneTimeSetUp]
    public void Init() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        postGame = PostGame.GetInstance();
    }

    [Test]
    public void MethodsDontThrowExceptionTest() {
        PostGame.GetInstance();

        postGame.ResetState();
        postGame.UpdateState();
        postGame.RenderState();
        postGame.SelectMenuItem("MAIN_MENU");
        postGame.SelectMenuItem("QUIT_GAME");
        postGame.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
        postGame.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        postGame.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
        postGame.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Escape);
        postGame.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.R);
        postGame.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.R);
        Assert.Pass();
    }

    [Test]
    public void UpdateStateManyTimesTest() {
        for (int i = 0; i < 1000; i++) {
            postGame.UpdateState();
        }
        Assert.Pass();
    }
}