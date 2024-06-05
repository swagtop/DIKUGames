namespace Breakout.GameStates;

using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.State;
using DIKUArcade.Timers;
using DIKUArcade.Utilities;
using Breakout.Entities;
using Breakout.Entities.Blocks;
using Breakout.GUI;
using Breakout.LevelHandling;
using Breakout.Effects;
using Breakout.Effects.Powerups;
using Breakout.Effects.Hazards;

/// <summary> 
/// The GameRunning class is responsible for managing and running the 'Breakout' game itself.
/// It contains and manages the in-game objects, such as the player, balls, blocks, and effects.
/// When updating this game state, this class makes these objects interact, and makes descides if
/// the player has won, lost, or completed a level based off of these interactions.
/// </summary>
public class GameRunning : IGameState, IGameEventProcessor {
    private static GameRunning instance = new GameRunning();
    private GameEventBus eventBus = BreakoutBus.GetBus();

    private Player player = new Player();
    private Queue<Level> levelQueue = new Queue<Level>();
    private Level currentLevel = new Level();
    private EntityContainer<Ball> balls = new EntityContainer<Ball>();
    private BallLauncher ballLauncher;
    private EntityContainer<EffectEntity> effects = new EntityContainer<EffectEntity>();
    private bool fogOfWarActive = false;

    private Hearts hearts= new Hearts();
    private Timer timer = new Timer();
    private Points points = new Points();
    private Background background = new Background(
        new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"))
    );

    /// <summary> Private constructor that sets up base conditions for class. </summary>
    private GameRunning() {
        eventBus.Subscribe(GameEventType.PlayerEvent, player);
        eventBus.Subscribe(GameEventType.StatusEvent, this);
        eventBus.Subscribe(GameEventType.GameStateEvent, this);
        eventBus.Subscribe(GameEventType.TimedEvent, this);

        ballLauncher = new BallLauncher(balls, player);
        hearts.SetHearts(3);

        ResetState();
    }

    /// <summary> GetInstance method for Singleton purposes. </summary>
    public static GameRunning GetInstance() {
        return GameRunning.instance;
    }

    /// <summary> Resets everything except points, health and levelQueue </summary>
    public void ResetState() {
        player.Reset();
        balls.ClearContainer();
        ballLauncher.AddNewBall();       
        effects.ClearContainer();
        fogOfWarActive = false;

        StaticTimer.RestartTimer();
        timer.Reset();
    }

    /// <summary> Updates state, if time limit exceeded, ends the game as a loss. </summary>
    public void UpdateState() {
        timer.UpdateTimer();
        if (timer.TimeLimitExceeded()) { 
            EndGame("LOST");
        } else {
            player.Move();
            IterateBalls();
            IterateEffects();
        }
    }

    /// <summary> Renders all game objects. </summary>
    public void RenderState() {
        background.RenderBackground();

        if (!fogOfWarActive) { currentLevel.Blocks.RenderEntities(); }
        player.RenderEntity();
        balls.RenderEntities();
        effects.RenderEntities();

        hearts.RenderHearts();
        timer.RenderTimer();
        points.RenderPoints();
    }

    /// <summary> Moves all balls and makes them interact with player and blocks. </summary>
    public void IterateBalls() {
        string status = BallIterator.IterateBalls(currentLevel, player, balls, points);
        switch (status) {
            case "CONTINUE":
                break;
            case "NO_MORE_BALLS":
                bool playerLost = hearts.BreakHeart();
                if (playerLost) { EndGame("LOST"); }
                else { ballLauncher.AddNewBall(); }
                break;
            case "NO_MORE_BLOCKS":
                EndLevel();
                break;
        }
    }

    /// <summary> Moves all effects and engages them if they collide with the player. </summary>
    public void IterateEffects() {
        effects.Iterate(effect => {
            if (effect.CollidesWith(player)) { 
                effect.Pop().EngageEffect(balls, player);
            } else { 
                effect.Move();
            }
        });
    }

    /// <summary> Empties the levelQueue. </summary>
    public void FlushQueue() {
        levelQueue.Clear();
    }
    
    /// <summary> Ends level and switches to the next if there is one, ends game if not. </summary>
    public void EndLevel() {
        TimedEffectsCanceler.LevelEndCancel();

        if (levelQueue.Any()) {
            ResetState();
            currentLevel = levelQueue.Dequeue();
            timer.SetTimeLimit(currentLevel.Meta.TimeLimit);
        } else {
            EndGame("WON");
        }
    }

    /// <summary> Collects stats of game, sends them to and switches to PostGame. </summary>
    public void EndGame(string result) {
        int finalPoints = (int)points.GetPoints();

        eventBus.RegisterEvent(new GameEvent {
            EventType = GameEventType.GraphicsEvent,
            Message = "DISPLAY_STATS",
            StringArg1 = result,
            IntArg1 = finalPoints
        });
        eventBus.RegisterEvent(new GameEvent {
            EventType = GameEventType.GameStateEvent,
            Message = "CHANGE_STATE",
            StringArg1 = "POST_GAME"
        });
    }

    /// <summary> Interprets behaviour from keypress. </summary>
    /// <param name="key"> The keyboard key pressed. </param>
    private void KeyPress(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Escape:
                StaticTimer.PauseTimer();
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_PAUSED"
                });
                break;
            case KeyboardKey.Left: 
            case KeyboardKey.A:
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE",
                    StringArg1 = "LEFT",
                    StringArg2 = "START"
                });
                break;
            case KeyboardKey.Right: 
            case KeyboardKey.D:
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE",
                    StringArg1 = "RIGHT",
                    StringArg2 = "START"
                });
                break;
            case KeyboardKey.Space:
                ballLauncher.LaunchBall();
                break;
            case KeyboardKey.B:
                Console.WriteLine("DEBUG: All blocks take one hit.");
                int blockAmount = currentLevel.Blocks.CountEntities();
                currentLevel.Blocks.Iterate(block => block.Hit());
                currentLevel.BreakableLeft -= blockAmount - currentLevel.Blocks.CountEntities();
                break;
            case KeyboardKey.Tab:
                if (levelQueue.Any()) {
                    Console.WriteLine("DEBUG: Skipping to next level in queue.");
                } else {
                    Console.WriteLine("DEBUG: No more levels in queue, switching to PostGame!");
                }
                EndLevel();
                break;
        }
    }

    /// <summary> Interprets behaviour from key release. </summary>
    /// <param name="key"> The keyboard key released. </param>
    private void KeyRelease(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Left:
            case KeyboardKey.A:
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE",
                    StringArg1 = "LEFT",
                    StringArg2 = "STOP"
                });
                break;
            case KeyboardKey.Right:
            case KeyboardKey.D:
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE",
                    StringArg1 = "RIGHT",
                    StringArg2 = "STOP"
                });
                break;
        }
    }

    /// <summary> Handles key events by sending them off to respective methods. </summary>
    /// <param name="action"> Has the key been pressed or released?. </param>
    /// <param name="key"> The keyboard key in question. </param>
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        switch (action) {
            case KeyboardAction.KeyPress:
                KeyPress(key);
                break;
            case KeyboardAction.KeyRelease:
                KeyRelease(key);
                break;
        }
    }

    /// <summary> Interprets behaviour from game events. </summary>
    /// <param name="gameEvent"> The game event received from event bus. </param>
    public void ProcessEvent(GameEvent gameEvent) {
        switch (gameEvent.Message) {
            case "SPAWN_POWERUP":
                effects.AddEntity(EffectEntityFactory.CreateRandomPowerup((Vec2F)gameEvent.ObjectArg1));
                break;
            case "SPAWN_HAZARD":
                effects.AddEntity(EffectEntityFactory.CreateRandomHazard((Vec2F)gameEvent.ObjectArg1));
                break;
            case "ENGAGE_EFFECT":
                ((IEffect)gameEvent.ObjectArg1).EngageEffect(balls, player);
                break;
            case "DISENGAGE_EFFECT":
                ((IEffect)gameEvent.ObjectArg1).DisengageEffect(balls, player);
                break;
            case "GAIN_LIFE":
                hearts.MendHeart();
                break;
            case "LOSE_LIFE":
                bool playerLost = hearts.BreakHeart();
                if (playerLost) { EndGame("LOST"); }
                break;
            case "SET_FOG_OF_WAR":
                fogOfWarActive = (gameEvent.StringArg1 == "ENGAGE");
                break;

            case "LOAD_LEVEL":
                ResetState();
                currentLevel = (Level)gameEvent.ObjectArg1;
                timer.SetTimeLimit(currentLevel.Meta.TimeLimit);
                break;
            case "QUEUE_LEVELS":
                ResetState();
                levelQueue = (Queue<Level>)gameEvent.ObjectArg1;
                currentLevel = levelQueue.Dequeue();
                timer.SetTimeLimit(currentLevel.Meta.TimeLimit);
                break;
            case "FLUSH_QUEUE":
                FlushQueue();
                break;

            case "CHANGE_STATE":
                if (gameEvent.StringArg1 == "GAME_RUNNING" ||
                    gameEvent.StringArg1 == "GAME_PAUSED"  ||
                    gameEvent.StringArg1 == "POST_GAME") return;
                if (levelQueue.Count > 0) { FlushQueue(); }
                hearts.ResetHearts();
                points.ResetPoints();
                break;
            default:
                break;
        }
    }
}
