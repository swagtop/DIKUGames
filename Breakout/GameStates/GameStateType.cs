using System;

namespace Breakout.GameStates;
public enum GameStateType
{
    GameRunning,
    GamePaused,
    MainMenu,
    ChooseLevel,
    GameOver,
    GameWon
}
public static class StateTransformer
{
    public static GameStateType TransformStringToState(string state)
    {
        switch (state)
        {
            case "GAME_RUNNING":
                return GameStateType.GameRunning;
            case "GAME_PAUSED":
                return GameStateType.GamePaused;
            case "MAIN_MENU":
                return GameStateType.MainMenu;
            case "CHOOSE_LEVEL":
                return GameStateType.ChooseLevel;
            case "GAME_OVER":
                return GameStateType.GameOver;
            case "GAME_WON":
                return GameStateType.GameWon;
            default:
                throw new ArgumentException($"Unrecognized GameStateType: {state}");
        }
    }

    public static string TransformStateToString(GameStateType state)
    {
        switch (state)
        {
            case GameStateType.GameRunning:
                return "GAME_RUNNING";
            case GameStateType.GamePaused:
                return "GAME_PAUSED";
            case GameStateType.MainMenu:
                return "MAIN_MENU";
            case GameStateType.ChooseLevel:
                return "CHOOSE_LEVEL";
            case GameStateType.GameOver:
                return "GAME_OVER";
            case GameStateType.GameWon:
                return "GAME_WON";
            default:
                throw new ArgumentException($"Unrecognized GameStateType: {state}");
        }
    }
}