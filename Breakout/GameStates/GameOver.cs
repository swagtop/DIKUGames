namespace Breakout.GameStates;

using System;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;
using Breakout.GUI;

public class GameOver : IGameState {
    private static GameOver instance = new GameOver();
    private GameEventBus eventBus = BreakoutBus.GetBus();
    private Text gameOverText = new Text("Game Over!", new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f));
    private int points;

    private Menu menu = new Menu(
        0.4f,
        ("Main Menu", "MAIN_MENU"),
        ("Quit Game", "QUIT_GAME")
    );

    private GameOver() {
        gameOverText.SetColor(new Vec3F(1.0f, 0.0f, 0.0f));
    }
    
    public static GameOver GetInstance() {
        return GameOver.instance;
    }
    public void ResetState() {
        menu.Reset();
        eventBus.RegisterEvent(new GameEvent {
            EventType = GameEventType.StatusEvent,
            Message = "DUMP_QUEUE",
        });
    }
    
    public void UpdateState() {
    }
    
    public void RenderState() {
        GameRunning.GetInstance().RenderState();
        gameOverText.RenderText();
        menu.RenderMenu();
    }
    public void SelectMenuItem(string value) {
        switch (value) {
            case "MAIN_MENU":
                ResetState();
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "MAIN_MENU"
                });
                break;
            case "QUIT_GAME":
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.WindowEvent,
                    Message = "QUIT_GAME",
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
            default:
                break;
        }
    }

    private void KeyRelease(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Left:
            case KeyboardKey.A:
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE",
                    StringArg1 = "LEFT",
                    StringArg2 = "STOP"
                });
                break;
            case KeyboardKey.Right:
            case KeyboardKey.D:
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
