using NUnit.Framework;
using DIKUArcade.Math;
using DIKUArcade.Events;
using Breakout.GameStates;

namespace BreakoutTests;
public class StateTransformerTests {
    GameStateType mainMenu = GameStateType.MainMenu;
    GameStateType chooseLevel = GameStateType.ChooseLevel;
    GameStateType gameRunning = GameStateType.GameRunning;
    GameStateType gamePaused = GameStateType.GamePaused;
    string stringMainMenu = "MAIN_MENU";
    string stringChooseLevel = "CHOOSE_LEVEL";
    string stringGameRunning = "GAME_RUNNING";
    string stringGamePaused = "GAME_PAUSED";

    [SetUp]
    public void Setup() {
    }

    [Test]
    public void TestStringToGameState() {
        Assert.AreEqual(mainMenu, StateTransformer.TransformStringToState(stringMainMenu));
        Assert.AreEqual(chooseLevel, StateTransformer.TransformStringToState(stringChooseLevel));
        Assert.AreEqual(gameRunning, StateTransformer.TransformStringToState(stringGameRunning));
        Assert.AreEqual(gamePaused, StateTransformer.TransformStringToState(stringGamePaused));
    }

    [Test]
    public void TestStateToString() {
        Assert.AreEqual(stringMainMenu, StateTransformer.TransformStateToString(mainMenu));
        Assert.AreEqual(stringChooseLevel, StateTransformer.TransformStateToString(chooseLevel));
        Assert.AreEqual(stringGameRunning, StateTransformer.TransformStateToString(gameRunning));
        Assert.AreEqual(stringGamePaused, StateTransformer.TransformStateToString(gamePaused));
    }
}