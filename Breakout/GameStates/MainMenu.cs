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
    /*
    private int activeMenuButton = 0;
    private Vec3F whiteColor = new Vec3F(1.0f, 1.0f, 1.0f);
    private Vec3F greyColor = new Vec3F(0.4f, 0.4f, 0.4f);
    private Text[] menuButtons = {
        new Text("Choose Level", new Vec2F(0.12f, 0.43f), new Vec2F(0.3f, 0.3f)),
        new Text("Quit", new Vec2F(0.12f, 0.3f), new Vec2F(0.3f, 0.3f)),
    };
    */
    private MenuButtonContainer menuButtons = new MenuButtonContainer(
        0.4f,
        ("Choose Level", "CHOOSE_LEVEL"),
        ("Quit", "CLOSE_WINDOW")
    );

    public static MainMenu GetInstance() {
        return MainMenu.instance;
    }
    
    public void RenderState() {
        backGroundImage.RenderEntity();
        /*
        foreach (Text button in menuButtons) {
            button.RenderText();
        }
        */
        menuButtons.RenderButtons();
    }

    public void ResetState() {
        /*
        activeMenuButton = 0; 
        foreach (Text button in instance.menuButtons) { 
            button.SetColor(instance.greyColor); 
        }
        menuButtons[activeMenuButton].SetColor(whiteColor);
        */
    }

    public void UpdateState() {
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        switch ((action, key)) {
            case (KeyboardAction.KeyPress, KeyboardKey.Up):
                menuButtons.GoUp();
                break;

            case (KeyboardAction.KeyPress, KeyboardKey.Down):
                menuButtons.GoDown();
                break;

            case (KeyboardAction.KeyPress, KeyboardKey.Enter):
                switch(menuButtons.GetValue()) {
                    case "CHOOSE_LEVEL":
                        eventBus.RegisterEvent(new GameEvent {
                            EventType = GameEventType.GameStateEvent,
                            To = StateMachine.GetInstance(),
                            Message = "CHANGE_STATE",
                            StringArg1 = "CHOOSE_LEVEL"
                        });
                        break;
                    case "CLOSE_WINDOW":
                        eventBus.RegisterEvent(new GameEvent {
                            EventType = GameEventType.WindowEvent,
                            Message = "CLOSE_WINDOW",
                        });
                        break;
                    default:
                        throw new ArgumentException($"Button not implemented: {menuButtons.GetText()}");
                        break;
                }
                break;
            default:
                break;
        }
    }
}