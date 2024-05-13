namespace BreakoutTests;

using NUnit.Framework;
using DIKUArcade.Math;
using DIKUArcade.Events;
using Breakout.GameStates;

public class GameStateTransformerTests {
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
        Assert.AreEqual(mainMenu, GameStateTransformer.TransformStringToState(stringMainMenu));
        Assert.AreEqual(chooseLevel, GameStateTransformer.TransformStringToState(stringChooseLevel));
        Assert.AreEqual(gameRunning, GameStateTransformer.TransformStringToState(stringGameRunning));
        Assert.AreEqual(gamePaused, GameStateTransformer.TransformStringToState(stringGamePaused));
    }

    [Test]
    public void TestStateToString() {
        Assert.AreEqual(stringMainMenu, GameStateTransformer.TransformStateToString(mainMenu));
        Assert.AreEqual(stringChooseLevel, GameStateTransformer.TransformStateToString(chooseLevel));
        Assert.AreEqual(stringGameRunning, GameStateTransformer.TransformStateToString(gameRunning));
        Assert.AreEqual(stringGamePaused, GameStateTransformer.TransformStateToString(gamePaused));
    }
}
