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
public class GamePaused : IGameState
{
    private static GamePaused instance = new GamePaused();
    private GameEventBus eventBus = BreakoutBus.GetBus();
    private Entity backGroundImage = new Entity(
        new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
        new Image(Path.Combine("Assets", "Images", "BreakoutTitleScreen.png"))
    );
    private Vec3F redColor = new Vec3F(1.0f, 0.0f, 0.0f);
    private Text gamePausedText = new Text("GAME PAUSED!", new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f));
    private Menu menu = new Menu(
        0.4f,
        ("Resume Game", "RESUME_GAME"),
        ("Main Menu", "MAIN_MENU")
    );

    public static GamePaused GetInstance()
    {
        return GamePaused.instance;
    }

    public void RenderState()
    {
        GameRunning.GetInstance().RenderState();
        gamePausedText.RenderText();
        menu.RenderButtons();
    }

    public void ResetState()
    {
        gamePausedText.SetColor(redColor);
        menu.Reset();
    }

    public void UpdateState()
    {
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key)
    {
        if (action != KeyboardAction.KeyPress) return;

        switch ((key, menu.GetValue()))
        {
            case (KeyboardKey.Up, _):
                menu.GoUp();
                break;
            case (KeyboardKey.Down, _):
                menu.GoDown();
                break;
            case (KeyboardKey.Escape, _):
                ResetState();
                eventBus.RegisterEvent(new GameEvent
                {
                    EventType = GameEventType.GameStateEvent,
                    To = StateMachine.GetInstance(),
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_RUNNING"
                });
                break;
            case (KeyboardKey.Enter, "RESUME_GAME"):
                ResetState();
                eventBus.RegisterEvent(new GameEvent
                {
                    EventType = GameEventType.GameStateEvent,
                    To = StateMachine.GetInstance(),
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_RUNNING"
                });
                break;
            case (KeyboardKey.Enter, "MAIN_MENU"):
                ResetState();
                eventBus.RegisterEvent(new GameEvent
                {
                    EventType = GameEventType.GameStateEvent,
                    To = StateMachine.GetInstance(),
                    Message = "CHANGE_STATE",
                    StringArg1 = "MAIN_MENU"
                });
                break;
            case (KeyboardKey.Enter, _):
                throw new ArgumentException($"Button number not implemented: {menu.GetText()}");
            default:
                break;
        }
    }
}