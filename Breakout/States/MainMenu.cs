using System;
using System.IO;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.State;
using Breakout.States;

namespace Breakout.States;
public class MainMenu : IGameState {
    private static MainMenu instance = null;
    private Entity backGroundImage = new Entity(
        new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
        new Image(Path.Combine("Assets", "Images", "BreakoutTitleScreen.png"))
    );
    private Text[] menuButtons = {
        new Text("New Game", new Vec2F(0.5f, 0.5f), new Vec2F(0.3f, 0.3f)),
        new Text("Quit", new Vec2F(0.5f, 0.3f), new Vec2F(0.3f, 0.3f)),
    };
    private int activeMenuButton = 0;
    private int maxMenuButtons = 2;
    public static MainMenu GetInstance() {
        if (MainMenu.instance == null) {
            MainMenu.instance = new MainMenu();
            MainMenu.instance.ResetState();
        }
        return MainMenu.instance;
    }
    
    public void RenderState() {
        backGroundImage.RenderEntity();
        foreach (Text button in menuButtons) {
            button.RenderText();
        }
    }
    public void ResetState() {
    }
    public void UpdateState() {
    }
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        switch ((action, key)) {
            case (KeyboardAction.KeyPress, KeyboardKey.Up):
                if (activeMenuButton > 0) { activeMenuButton -= 1; }
                break;
            case (KeyboardAction.KeyPress, KeyboardKey.Down):
                if (activeMenuButton < maxMenuButtons - 1) { activeMenuButton += 1; }
                break;
            case (KeyboardAction.KeyPress, KeyboardKey.Enter):
                switch (activeMenuButton) {
                    case 0:
                        BreakoutBus.GetBus().RegisterEvent(
                            new GameEvent {
                                EventType = GameEventType.GameStateEvent,
                                Message = "CHANGE_STATE",
                                StringArg1 = "GAME_RUNNING"
                        });
                        break;
                    case 1:
                        BreakoutBus.GetBus().RegisterEvent(
                            new GameEvent {
                                EventType = GameEventType.WindowEvent,
                                Message = "CLOSE_WINDOW",
                        });
                        break;
                }
                break;
            default:
                break;
        }
    }
}