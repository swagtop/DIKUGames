using System;
using System.IO;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.State;
using Breakout;
using Breakout.LevelHandling;
using Breakout.Menus;

namespace Breakout.GameStates;
public class ChooseLevel : IGameState {
    private static ChooseLevel instance = new ChooseLevel();
    private GameEventBus eventBus = BreakoutBus.GetBus();
    private Entity backGroundImage = new Entity(
        new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
        new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"))
    );
    private MenuButtonContainer menuButtons = new MenuButtonContainer(0.6f);
    public static ChooseLevel GetInstance() {
        return ChooseLevel.instance;
    }
    
    public void RenderState() {
        backGroundImage.RenderEntity();
        menuButtons.RenderButtons();
    }

    public void ResetState() {
        menuButtons.Clear();
        menuButtons.AddButton("Main Menu", "MAIN_MENU");

        string[] levelAssets = Directory.GetFiles(Path.Combine("Assets", "Levels"));
        float buttonDistance = 0.5f / levelAssets.Length;

        for (int i = 0; i < levelAssets.Length; i++) {
            string fileName = levelAssets[levelAssets.Length - 1 - i].Remove(0, 14);
            menuButtons.AddButton(fileName, fileName);
        }

        menuButtons.Reset();
    }

    public void UpdateState() {
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (action != KeyboardAction.KeyPress) return;

        switch ((key, menuButtons.GetValue())) {
            case (KeyboardKey.Up, _):
                menuButtons.GoUp();
                break;
            case (KeyboardKey.Down, _):
                menuButtons.GoDown();
                break;
            case (KeyboardKey.Enter, "MAIN_MENU"):
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    To = StateMachine.GetInstance(),
                    Message = "CHANGE_STATE",
                    StringArg1 = "MAIN_MENU"
                });
                break;
            case (KeyboardKey.Enter, _):
                try {
                    Level level = LevelFactory.LoadFromFile(
                        Path.Combine("Assets", "Levels", menuButtons.GetValue())
                    );
                    eventBus.RegisterEvent(new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        To = GameRunning.GetInstance(),
                        Message = "LOAD_LEVEL",
                        ObjectArg1 = (object)level
                    });
                    eventBus.RegisterEvent(new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        To = StateMachine.GetInstance(),
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_RUNNING"
                    });
                } catch (Exception e) {
                    Console.WriteLine("Cannot load level: " + e.ToString().Split('\n')[0]);
                }
                break;
            case (KeyboardKey.Escape, _):
                ResetState();
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    To = StateMachine.GetInstance(),
                    Message = "CHANGE_STATE",
                    StringArg1 = "MAIN_MENU"
                });
                break;
            default:
                break;
        }
    }
}