namespace Breakout.GameStates;

/// <summary>
/// This Enum has an entry for each game state. Should a new game state be implemented, an entry
/// representing it should be added here.
/// </summary>
public enum GameStateType {
    GameRunning,
    GamePaused,
    MainMenu,
    ChooseLevel,
    PostGame
}

/// <summary>
/// This class is made such that easy conversion between strings and enum values representing the
/// same game state can be easily converted to eachother.
/// </summary>
public static class GameStateTransformer {
    public static GameStateType TransformStringToState(string state) {
        switch (state) {
            case "GAME_RUNNING":
                return GameStateType.GameRunning;
            case "GAME_PAUSED":
                return GameStateType.GamePaused;
            case "MAIN_MENU":
                return GameStateType.MainMenu;
            case "CHOOSE_LEVEL":
                return GameStateType.ChooseLevel;
            case "POST_GAME":
                return GameStateType.PostGame;
            default:
                throw new ArgumentException($"Unrecognized GameStateType: {state}");
        }
    }

    public static string TransformStateToString(GameStateType state) {
        switch (state) {
            case GameStateType.GameRunning:
                return "GAME_RUNNING";
            case GameStateType.GamePaused:
                return "GAME_PAUSED";
            case GameStateType.MainMenu:
                return "MAIN_MENU";
            case GameStateType.ChooseLevel:
                return "CHOOSE_LEVEL";
            case GameStateType.PostGame:
                return "POST_GAME";
            default:
                throw new ArgumentException($"Unrecognized GameStateType: {state}");
        }
    }
}
