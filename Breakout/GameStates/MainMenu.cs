using System;
using System.IO;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.State;
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
        ("Choose Level", "CHOOSE_LEVEL"),
        ("Quit", "CLOSE_WINDOW")
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

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (action != KeyboardAction.KeyPress) return;

        switch ((key, menu.GetValue())) {
            case (KeyboardKey.Up, _):
                menu.GoUp();
                break;
            case (KeyboardKey.Down, _):
                menu.GoDown();
                break;
            case (KeyboardKey.Enter, "CHOOSE_LEVEL"):
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    To = StateMachine.GetInstance(),
                    Message = "CHANGE_STATE",
                    StringArg1 = "CHOOSE_LEVEL"
                });
                break;
            case (KeyboardKey.Enter, "CLOSE_WINDOW"):
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.WindowEvent,
                    Message = "CLOSE_WINDOW",
                });
                break;
            case (KeyboardKey.Enter, _):
                throw new ArgumentException($"Button not implemented: {menu.GetText()}");
            default:
                break;
        }
    }
}