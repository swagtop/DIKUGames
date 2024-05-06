using System;
using System.IO;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.State;
using Breakout.LevelHandling;
using Breakout.Menus;


namespace Breakout.GameStates;
public class MainMenu : IGameState {
    private static MainMenu instance = new MainMenu();
    private GameEventBus eventBus = BreakoutBus.GetBus();
    private Entity backGroundImage = new Entity(
        new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
        new Image(Path.Combine("Assets", "Images", "BreakoutTitleScreen.png"))
    );
    private Menu menu = new Menu(
        0.4f,
        ("Play Campaign", "PLAY_CAMPAIGN"),
        ("Choose Level", "CHOOSE_LEVEL"),
        ("Quit", "QUIT_GAME")
    );

    public static MainMenu GetInstance() {
        return MainMenu.instance;
    }
    
    public void RenderState() {
        backGroundImage.RenderEntity();
        menu.RenderButtons();
    }

    public void ResetState() {
        menu.Reset();
    }

    public void UpdateState() {
    }

    public void SelectMenuItem(string value) {
        switch (value) {
            case ("PLAY_CAMPAIGN"):
                Queue<Level> levelQueue = new Queue<Level>();
                string[] levelFilenames = Directory.GetFiles(Path.Combine("Assets", "Levels"));

                for (int i = 0; i < levelFilenames.Length; i++) {
                    levelFilenames[i] = levelFilenames[i].Remove(0, 14);
                }

                foreach (string filename in levelFilenames) {
                    try {
                        levelQueue.Enqueue(LevelFactory.LoadFromFile(
                            Path.Combine("Assets", "Levels", filename)
                        ));
                    } catch (Exception e) {
                        Console.WriteLine("Cannot load level: " + e.ToString().Split('\n')[0]);
                    }
                }

                if (levelQueue.Count > 0) {
                    eventBus.RegisterEvent(new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        To = GameRunning.GetInstance(),
                        Message = "QUEUE_LEVELS",
                        ObjectArg1 = (object)levelQueue
                    });
                    eventBus.RegisterEvent(new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        To = StateMachine.GetInstance(),
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_RUNNING"
                    });
                }
                break;
            case "CHOOSE_LEVEL":
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    To = StateMachine.GetInstance(),
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
                break;
        }
    }

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