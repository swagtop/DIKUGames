namespace Breakout.GameStates;

using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;
using Breakout.GUI;

public class GamePaused : IGameState {
    private static GamePaused instance = new GamePaused();
    private GameEventBus eventBus = BreakoutBus.GetBus();
    private Text gamePausedText = new Text("GAME PAUSED!", new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f));
    private Menu menu = new Menu(
        0.4f,
        ("Resume Game", "RESUME_GAME"),
        ("Main Menu", "MAIN_MENU")
    );

    private GamePaused() {
        gamePausedText.SetColor(new Vec3F(1.0f, 0.0f, 0.0f));
    }

    public static GamePaused GetInstance() {
        return GamePaused.instance;
    }
    
    public void ResetState() {
        menu.Reset();
    }

    public void UpdateState() {}

    public void RenderState() {
        GameRunning.GetInstance().RenderState();
        gamePausedText.RenderText();
        menu.RenderMenu();
    }

    public void SelectMenuItem(string value) {
        switch (value) {
            case "RESUME_GAME":
                ResetState();
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_RUNNING"
                });
                break;
            case "MAIN_MENU":
                ResetState();
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "MAIN_MENU"
                });
                break;
            default:
                throw new ArgumentException($"Button number not implemented: {menu.GetText()}");
        }
    }

    private void KeyPress(KeyboardKey key) {
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
                    StringArg1 = "GAME_RUNNING"
                });
                break;
            case KeyboardKey.Left: case KeyboardKey.A:
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE",
                    StringArg1 = "LEFT",
                    StringArg2 = "START"
                });
                break;
            case KeyboardKey.Right: case KeyboardKey.D:
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE",
                    StringArg1 = "RIGHT",
                    StringArg2 = "START"
                });
                break;
            default:
                break;
        }
    }

    private void KeyRelease(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Left: case KeyboardKey.A:
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE",
                    StringArg1 = "LEFT",
                    StringArg2 = "STOP"
                });
                break;
            case KeyboardKey.Right: case KeyboardKey.D:
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE",
                    StringArg1 = "RIGHT",
                    StringArg2 = "STOP"
                });
                break;
        }
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        switch (action) {
            case KeyboardAction.KeyPress:
                KeyPress(key);
                break;
            case KeyboardAction.KeyRelease:
                KeyRelease(key);
                break;
        }
    }
}
