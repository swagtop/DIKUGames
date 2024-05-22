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

public class GameWon : IGameState {
    private static GameWon instance = new GameWon();
    private GameEventBus eventBus = BreakoutBus.GetBus();
    private Text gameWonText = new Text("Game Won!", new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f));
    private int points;
    private Menu menu = new Menu(
        0.4f,
        ("Main Menu", "MAIN_MENU"),
        ("Quit Game", "QUIT_GAME")
    );
    
    private GameWon() {
        gameWonText.SetColor(new Vec3F(1.0f, 0.0f, 0.0f));
    }
    
    public static GameWon GetInstance() {
        return GameWon.instance;
    }

    public void ResetState() {
        menu.Reset();
    }

    public void UpdateState() {}

    public void RenderState() {
        GameRunning.GetInstance().RenderState();
        gameWonText.RenderText();
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
