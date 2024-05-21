using System;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;
using Breakout.Menus;

namespace Breakout.GameStates;

public class GameOver : IGameState
{
    private static GameOver instance = new GameOver();
    private GameEventBus eventBus = BreakoutBus.GetBus();
    private Entity backGroundImage = new Entity(
    new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
    new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));

    private Vec3F redColor = new Vec3F(1.0f, 0.0f, 0.0f);
    private Text gameOverText = new Text("Game Over!", new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f));
    private int points;

    private Menu menu = new Menu(
        0.4f,
        ("Main Menu", "MAIN_MENU"),
        ("Quit Game", "QUIT_GAME")
    );
    public static GameOver GetInstance()
    {
        return GameOver.instance;
    }
    public void ResetState()
    {
        gameOverText.SetColor(redColor);
        menu.Reset();

    }
    public void UpdateState()
    {

    }
    public void RenderState()
    {
        GameRunning.GetInstance().RenderState();
        gameOverText.RenderText();
        menu.RenderButtons();
    }
    public void SelectMenuItem(string value)
    {
        switch (value)
        {
            case "QUIT_GAME":
                eventBus.RegisterEvent(new GameEvent
                {
                    EventType = GameEventType.WindowEvent,
                    Message = "QUIT_GAME",
                });
                break;
            case "MAIN_MENU":
                ResetState();
                eventBus.RegisterEvent(new GameEvent
                {
                    EventType = GameEventType.GameStateEvent,
                    To = StateMachine.GetInstance(),
                    Message = "CHANGE_STATE",
                    StringArg1 = "MAIN_MENU"
                });
                break;
            default:
                throw new ArgumentException($"Button number not implemented: {menu.GetText()}");
        }
    }



    private void KeyPress(KeyboardKey key)
    {
        switch (key)
        {
            case (KeyboardKey.Up):
                menu.GoUp();
                break;
            case (KeyboardKey.Down):
                menu.GoDown();
                break;
            case (KeyboardKey.Enter):
                SelectMenuItem(menu.GetValue());
                break;
            case KeyboardKey.Left:
            case KeyboardKey.A:
                eventBus.RegisterEvent(new GameEvent
                {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE",
                    StringArg1 = "LEFT",
                    StringArg2 = "START"
                });
                break;
            case KeyboardKey.Right:
            case KeyboardKey.D:
                eventBus.RegisterEvent(new GameEvent
                {
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

    private void KeyRelease(KeyboardKey key)
    {
        switch (key)
        {
            case KeyboardKey.Left:
            case KeyboardKey.A:
                eventBus.RegisterEvent(new GameEvent
                {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE",
                    StringArg1 = "LEFT",
                    StringArg2 = "STOP"
                });
                break;
            case KeyboardKey.Right:
            case KeyboardKey.D:
                eventBus.RegisterEvent(new GameEvent
                {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE",
                    StringArg1 = "RIGHT",
                    StringArg2 = "STOP"
                });
                break;
        }
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key)
    {
        switch (action)
        {
            case KeyboardAction.KeyPress:
                KeyPress(key);
                break;
            case KeyboardAction.KeyRelease:
                KeyRelease(key);
                break;
        }
    }

}