using System;
using System.IO;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.State;
using Breakout;
using Breakout.States;
using Breakout.LevelHandling;

namespace Breakout.States;
public class ChooseLevel : IGameState {
    private static ChooseLevel instance = null;
    private GameEventBus eventBus = BreakoutBus.GetBus();
    private Entity backGroundImage = new Entity(
        new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
        new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"))
    );
    private int activeMenuButton = 0;
    private Vec3F whiteColor = new Vec3F(1.0f, 1.0f, 1.0f);
    private Vec3F greyColor = new Vec3F(0.4f, 0.4f, 0.4f);
    private List<Text> menuButtons = new List<Text>();
    private List<string> levelFiles = new List<string>();
    
    public static ChooseLevel GetInstance() {
        if (ChooseLevel.instance == null) {
            ChooseLevel.instance = new ChooseLevel();
            ChooseLevel.instance.ResetState();
        }
        return ChooseLevel.instance;
    }
    
    public void RenderState() {
        backGroundImage.RenderEntity();
        foreach (Text button in menuButtons) {
            button.RenderText();
        }
    }

    public void ResetState() {
        menuButtons.Clear();
        levelFiles.Clear();
        
        string[] levelAssets = Directory.GetFiles(Path.Combine("Assets", "Levels"));
        float buttonDistance = 0.5f / levelAssets.Length;
        menuButtons.Add(new Text(
            "Main Menu", 
            new Vec2F(0.1f, 0.5f), 
            new Vec2F(0.3f, 0.3f)
        ));

        for (int i = 0; i < levelAssets.Length; i++) {
            string fileName = levelAssets[levelAssets.Length - 1 - i].Remove(0, 14);
            menuButtons.Add(new Text(
                fileName, 
                new Vec2F(0.1f, 0.5f - ((i + 1) * buttonDistance)), 
                new Vec2F(0.3f, 0.3f)
            ));
            levelFiles.Add(fileName);
        }
        activeMenuButton = 0;
        foreach (Text button in menuButtons) { 
            button.SetColor(greyColor); 
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
                if (activeMenuButton < menuButtons.Count - 1) { 
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
                                StringArg1 = "MAIN_MENU"
                        });
                        break;
                    default:
                        try {
                            Level level = LevelFactory.LoadFromFile(
                                Path.Combine("Assets", "Levels", levelFiles[activeMenuButton - 1])
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
                }
                break;

            default:
                break;
        }
    }
}