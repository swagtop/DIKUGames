using System;
using System.IO;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.State;

namespace Breakout.GameStates;
public class GamePaused : IGameState {
    private static GamePaused instance = new GamePaused();
    private GameEventBus eventBus = BreakoutBus.GetBus();
    private Entity backGroundImage = new Entity(
        new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
        new Image(Path.Combine("Assets", "Images", "BreakoutTitleScreen.png"))
    );
    private int activeMenuButton = 0;
    private Text gamePausedText = new Text("GAME PAUSED!", new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f));
    private Vec3F redColor = new Vec3F(1.0f, 0.0f, 0.0f);
    private Vec3F whiteColor = new Vec3F(1.0f, 1.0f, 1.0f);
    private Vec3F greyColor = new Vec3F(0.4f, 0.4f, 0.4f);
    private Text[] menuButtons = {
        new Text("Resume", new Vec2F(0.35f, 0.5f), new Vec2F(0.3f, 0.3f)),
        new Text("Main Menu", new Vec2F(0.35f, 0.3f), new Vec2F(0.3f, 0.3f)),
    };

    public static GamePaused GetInstance() {
        return GamePaused.instance;
    }
    
    public void RenderState() {
        GameRunning.GetInstance().RenderState();
        foreach (Text button in menuButtons) {
            button.RenderText();
        }
        gamePausedText.RenderText();
    }

    public void ResetState() {
        gamePausedText.SetColor(redColor);
        activeMenuButton = 0; 
        foreach (Text button in menuButtons) { 
            button.SetColor(greyColor);
        }
        menuButtons[instance.activeMenuButton].SetColor(whiteColor);
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

            case (KeyboardAction.KeyPress, KeyboardKey.Escape):
                ResetState();
                eventBus.RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        To = StateMachine.GetInstance(),
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_RUNNING"
                });
                break;
            
            case (KeyboardAction.KeyPress, KeyboardKey.Enter):
                switch (activeMenuButton) {
                    case 0:
                        ResetState();
                        eventBus.RegisterEvent(
                            new GameEvent {
                                EventType = GameEventType.GameStateEvent,
                                To = StateMachine.GetInstance(),
                                Message = "CHANGE_STATE",
                                StringArg1 = "GAME_RUNNING"
                        });
                        break;
                    case 1:
                        ResetState();
                        eventBus.RegisterEvent(
                            new GameEvent {
                                EventType = GameEventType.GameStateEvent,
                                To = StateMachine.GetInstance(),
                                Message = "CHANGE_STATE",
                                StringArg1 = "MAIN_MENU"
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