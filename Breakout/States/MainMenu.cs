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
    private GameEventBus eventBus = BreakoutBus.GetBus();
    private Entity backGroundImage = new Entity(
        new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
        new Image(Path.Combine("Assets", "Images", "BreakoutTitleScreen.png"))
    );
    private int activeMenuButton = 0;
    private Vec3F whiteColor = new Vec3F(1.0f, 1.0f, 1.0f);
    private Vec3F greyColor = new Vec3F(0.4f, 0.4f, 0.4f);
    private Text[] menuButtons = {
        new Text("Choose Level", new Vec2F(0.1f, 0.5f), new Vec2F(0.3f, 0.3f)),
        new Text("Quit", new Vec2F(0.1f, 0.3f), new Vec2F(0.3f, 0.3f)),
    };

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
        activeMenuButton = 0; 
        foreach (Text button in instance.menuButtons) { 
            button.SetColor(instance.greyColor); 
        }
        menuButtons[activeMenuButton].SetColor(whiteColor);
    }

    public void UpdateState() {
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        switch ((action, key)) {
            case (KeyboardAction.KeyPress, KeyboardKey.Up):
                if (activeMenuButton > 0) { 
                    menuButtons[activeMenuButton].SetColor(greyColor);
                    activeMenuButton -= 1; 
                    menuButtons[activeMenuButton].SetColor(whiteColor);
                }
                break;

            case (KeyboardAction.KeyPress, KeyboardKey.Down):
                if (activeMenuButton < menuButtons.Length - 1) { 
                    menuButtons[activeMenuButton].SetColor(greyColor);
                    activeMenuButton += 1; 
                    menuButtons[activeMenuButton].SetColor(whiteColor);
                }
                break;

            case (KeyboardAction.KeyPress, KeyboardKey.Enter):
                switch (activeMenuButton) {
                    case 0:
                        eventBus.RegisterEvent(
                            new GameEvent {
                                EventType = GameEventType.GameStateEvent,
                                To = StateMachine.GetInstance(),
                                Message = "CHANGE_STATE",
                                StringArg1 = "CHOOSE_LEVEL"
                        });
                        break;
                    case 1:
                        eventBus.RegisterEvent(
                            new GameEvent {
                                EventType = GameEventType.WindowEvent,
                                Message = "CLOSE_WINDOW",
                        });
                        break;
                default:
                    throw new ArgumentException($"Button number not implemented: {activeMenuButton}");
                }
                break;

            default:
                break;
        }
    }
}