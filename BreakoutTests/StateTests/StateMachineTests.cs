namespace BreakoutTests;

using NUnit.Framework;
using DIKUArcade.State;
using DIKUArcade.Math;
using DIKUArcade.Events;
using Breakout.GameStates;

public class StateMachineTests {
    private StateMachine stateMachine = StateMachine.GetInstance();
    private MainMenu mainMenu = MainMenu.GetInstance();
    private ChooseLevel chooseLevel = ChooseLevel.GetInstance();
    private GameRunning gameRunning = GameRunning.GetInstance();
    private GamePaused gamePaused = GamePaused.GetInstance();

    [SetUp]
    public void Setup() {
    }

    [Test]
    public void StateMachineStartsWithMainMenuTest() {
        Assert.AreEqual(stateMachine.ActiveState, mainMenu);
    }

    [Test]
    public void OnlyOneStateActiveTest() {
        Assert.AreEqual(stateMachine.ActiveState, mainMenu);
        Assert.AreNotEqual(stateMachine.ActiveState, chooseLevel);
        Assert.AreNotEqual(stateMachine.ActiveState, gameRunning);
        Assert.AreNotEqual(stateMachine.ActiveState, gamePaused);
    }

    [Test]
    public void AreIGameStateDerivedTest() {
        Assert.That(mainMenu, Is.InstanceOf<IGameState>());
        Assert.That(chooseLevel, Is.InstanceOf<IGameState>());
        Assert.That(gameRunning, Is.InstanceOf<IGameState>());
        Assert.That(gamePaused, Is.InstanceOf<IGameState>());
    }
}