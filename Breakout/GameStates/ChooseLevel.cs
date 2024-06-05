namespace Breakout.GameStates;

using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;
using DIKUArcade.Utilities;
using Breakout.LevelHandling;
using Breakout.GUI;

/// <summary> 
/// The ChooseLevel class is one of the game states that can be reached from the main menu.
/// This class loads all the files inside the /Assets/Levels folder, and makes them available for
/// individual loading into the GameRunning state.
/// </summary>
public class ChooseLevel : IGameState {
    private static ChooseLevel instance = new ChooseLevel();
    private GameEventBus eventBus = BreakoutBus.GetBus();
    private Background background = new Background(new Image(
        Path.Combine("Assets", "Images", "SpaceBackground.png"))
    );
    private Menu menu = new Menu(0.6f);

    /// <summary> Private constructor that sets up base conditions for class. </summary>
    private ChooseLevel() {
        ResetState();
    }

    /// <summary> GetInstance method for Singleton purposes. </summary>
    public static ChooseLevel GetInstance() {
        return ChooseLevel.instance;
    }
    
    /// <summary> Clears the menu, and loads all levelfiles into it </summary>
    public void ResetState() {
        menu.Clear();
        menu.AddButton("Main Menu", "MAIN_MENU");

        string fullPath = FileIO.GetProjectPath();
        string[] levelAssets = Directory.GetFiles(Path.Combine(fullPath, "Assets", "Levels"));
        Array.Sort(levelAssets);

        for (int i = levelAssets.Length-1; i > -1; i--) {
            string[] directoryParts = levelAssets[levelAssets.Length - 1 - i].Split(Path.DirectorySeparatorChar);
            string fileName = directoryParts[directoryParts.Length - 1];
            menu.AddButton(fileName, fileName);
        }

        menu.Reset();
    }
    
    /// <summary> Nothing to update, does nothing. </summary>
    public void UpdateState() {}

    /// <summary> Renders background and menu </summary>
    public void RenderState() {
        background.RenderBackground();
        menu.RenderMenu();
    }
    
    /// <summary> Acts on value taken from active menu button </summary>
    /// <param name="value"=> Value string representing button selected </param>
    public void SelectMenuItem(string value) {
        switch (value) {
            case ("MAIN_MENU"):
                ResetState();
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "MAIN_MENU"
                });
                break;
            default:
                try {
                    Level level = LevelFactory.LoadFromFile(
                        Path.Combine("Assets", "Levels", menu.GetValue())
                    );
                    eventBus.RegisterEvent(new GameEvent {
                        EventType = GameEventType.StatusEvent,
                        Message = "LOAD_LEVEL",
                        ObjectArg1 = (object)level
                    });
                    eventBus.RegisterEvent(new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_RUNNING"
                    });
                } catch (Exception e) {
                    Console.WriteLine("Cannot load level: " + e.ToString().Split('\n')[0]);
                }
                break;
        }
    }

    /// <summary> Interprets behaviour based on keyboard inputs. </summary>
    /// <param name="action"> Has the key been pressed or released?. </param>
    /// <param name="key"> The keyboard key in question. </param>
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (action != KeyboardAction.KeyPress) return;

        switch (key) {
            case (KeyboardKey.Up):
                menu.GoUp();
                break;
            case (KeyboardKey.Down):
                menu.GoDown();
                break;
            case (KeyboardKey.Enter):
                SelectMenuItem(menu.GetValue());
                break;
            case (KeyboardKey.Escape):
                ResetState();
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "MAIN_MENU"
                });
                break;
            default:
                break;
        }
    }
}
