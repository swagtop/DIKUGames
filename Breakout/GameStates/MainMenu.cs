namespace Breakout.GameStates;

using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;
using DIKUArcade.Utilities;
using Breakout.GUI;
using Breakout.LevelHandling;

/// <summary> 
/// The MainMenu class, should be the first game state the player meets. It is the main entry for
/// the player to choose what they would like to do inside the program.
/// </summary>
public class MainMenu : IGameState {
    private static MainMenu instance = new MainMenu();
    private GameEventBus eventBus = BreakoutBus.GetBus();
    private Background background = new Background(
        new Image(Path.Combine("Assets", "Images", "BreakoutTitleScreen.png"))
    );
    private Menu menu = new Menu(
        0.4f,
        ("Play Campaign", "PLAY_CAMPAIGN"),
        ("Choose Level", "CHOOSE_LEVEL"),
        ("Quit Game", "QUIT_GAME")
    );

    /// <summary> GetInstance method for Singleton purposes. </summary>
    public static MainMenu GetInstance() {
        return MainMenu.instance;
    }

    /// <summary> Resets the menu, and tells GameRunning to flush queue. </summary>
    public void ResetState() {
        menu.Reset();
        eventBus.RegisterEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "FLUSH_QUEUE",
        });
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
            case ("PLAY_CAMPAIGN"):
                Queue<Level> levelQueue = new Queue<Level>();
                
                string fullPath = FileIO.GetProjectPath();
                string[] levelFilenames = Directory.GetFiles(Path.Combine("Assets", "Levels"));
                Array.Sort(levelFilenames);

                for (int i = 0; i < levelFilenames.Length; i++) {
                    levelFilenames[i] = levelFilenames[i].Remove(0, 14);
                }

                foreach (string filename in levelFilenames) {
                    try {
                        levelQueue.Enqueue(LevelFactory.LoadFromFile(
                            Path.Combine(fullPath, "Assets", "Levels", filename)
                        ));
                    } catch (Exception e) {
                        Console.WriteLine("Cannot load level: " + e.ToString().Split('\n')[0]);
                    }
                }

                if (levelQueue.Count > 0) {
                    eventBus.RegisterEvent(new GameEvent {
                        EventType = GameEventType.StatusEvent,
                        Message = "QUEUE_LEVELS",
                        ObjectArg1 = (object)levelQueue
                    });
                    eventBus.RegisterEvent(new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_RUNNING"
                    });
                }
                break;
            case "CHOOSE_LEVEL":
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "CHOOSE_LEVEL"
                });
                break;
            case "QUIT_GAME":
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.WindowEvent,
                    Message = "QUIT_GAME",
                });
                break;
            default:
                throw new ArgumentException($"Button not implemented: {menu.GetText()}");
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
            default:
                break;
        }
    }
}
