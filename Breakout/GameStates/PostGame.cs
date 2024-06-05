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

/// <summary> 
/// The PostGame class, is the game state that is switched to when the player has either won or
/// lost in-game. It receives the final stats of the run through the event bus.
/// </summary>
public class PostGame : IGameState, IGameEventProcessor {
    private static PostGame instance = new PostGame();
    private GameEventBus eventBus = BreakoutBus.GetBus();
    private Background background = new Background(
        new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"))
    );
    private Text gameLostText = new Text("YOU LOST!", new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f));
    private Text gameWonText = new Text("YOU WON!", new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f));
    private Text finalPointsText = new Text("No points received!", new Vec2F(0.5f, 0.5f), new Vec2F(0.3f, 0.3f));
    private bool playerHasWon = false;

    private Menu menu = new Menu(
        0.4f,
        ("Main Menu", "MAIN_MENU"),
        ("Quit Game", "QUIT_GAME")
    );

    /// <summary> Private constructor that sets up base conditions for class. </summary>
    private PostGame() {
        eventBus.Subscribe(GameEventType.GraphicsEvent, this);

        gameWonText.SetColor(new Vec3F(1.0f, 0.0f, 0.0f));
        gameLostText.SetColor(new Vec3F(1.0f, 0.0f, 0.0f));
        finalPointsText.SetColor(new Vec3F(1.0f, 0.0f, 0.0f));
    }
    
    /// <summary> GetInstance method for Singleton purposes. </summary>
    public static PostGame GetInstance() {
        return PostGame.instance;
    }
    
    /// <summary> Resets the menu. </summary>
    public void ResetState() {
        menu.Reset();
    }
    
    /// <summary> Nothing to update, does nothing. </summary>
    public void UpdateState() {}

    /// <summary> Renders background, game result text, points, and menu. </summary>
    public void RenderState() {
        background.RenderBackground();
        if (playerHasWon) {
            gameWonText.RenderText();
        } else {
            gameLostText.RenderText();
        }
        finalPointsText.RenderText();
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
    
    /// <summary> Interprets behaviour based on keyboard inputs. </summary>
    /// <param name="action"> Has the key been pressed or released?. </param>
    /// <param name="key"> The keyboard key in question. </param>
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
    
    /// <summary> Receives final game stats from event bus here </summary>
    /// <param name="gameEvent"> The game event received from event bus. </param>
    public void ProcessEvent(GameEvent gameEvent) {
        switch (gameEvent.Message) {
            case "DISPLAY_STATS":
                playerHasWon = (gameEvent.StringArg1 == "WON");
                finalPointsText.SetText($"Total Points: {gameEvent.IntArg1}");
                break;
            default:
                break;
        }
    }
}
