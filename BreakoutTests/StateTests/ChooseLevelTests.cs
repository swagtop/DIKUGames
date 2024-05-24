namespace BreakoutTests;

using System;
using NUnit.Framework;
using DIKUArcade.Input;
using DIKUArcade.State;
using DIKUArcade.Math;
using DIKUArcade.Events;
using Breakout.GameStates;

public class ChooseLevelTests {
    private ChooseLevel chooseLevel;

    [OneTimeSetUp]
    public void Init() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        chooseLevel = ChooseLevel.GetInstance();
    }

    [Test]
    public void MethodsDontThrowExceptionTest() {
        ChooseLevel.GetInstance();

        chooseLevel.ResetState();
        chooseLevel.UpdateState();
        chooseLevel.RenderState();
        chooseLevel.SelectMenuItem("MAIN_MENU");
        chooseLevel.SelectMenuItem("empty.txt");
        chooseLevel.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
        chooseLevel.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        chooseLevel.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
        chooseLevel.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Escape);
        chooseLevel.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.R);
        chooseLevel.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.R);
        Assert.Pass();
    }

    [Test]
    public void UpdateStateManyTimesTest() {
        for (int i = 0; i < 1000; i++) {
            chooseLevel.UpdateState();
        }
        Assert.Pass();
    }
    
    [Test]
    public void CorrectExceptionForInvalidLevelNameTest() {
        try {
            chooseLevel.SelectMenuItem("no file with this name exists . t x t");
        } catch (Exception e) {
            Assert.AreEqual(e.Message.Split(':')[0], "Cannot load level");
        }
    }
}