namespace BreakoutTests;

using NUnit.Framework;
using DIKUArcade.Input;
using DIKUArcade.State;
using DIKUArcade.Math;
using DIKUArcade.Events;
using Breakout.GameStates;

public class MainMenuTests {
    private MainMenu mainMenu = MainMenu.GetInstance();

    [OneTimeSetUp]
    public void Setup() {}

    [Test]
    public void MethodsDontThrowExceptionTest() {
        MainMenu.GetInstance();

        mainMenu.ResetState();
        mainMenu.UpdateState();
        mainMenu.RenderState();
        mainMenu.SelectMenuItem("CHOOSE_LEVEL");
        mainMenu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
        mainMenu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        mainMenu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
        Assert.Pass();
    }
}
