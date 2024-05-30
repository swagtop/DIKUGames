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

public class GameRunning : IGameState, IGameEventProcessor {
    private static GameRunning instance = new GameRunning();
    private GameEventBus eventBus = BreakoutBus.GetBus();

    private Player player = new Player();
    private Queue<Level> levelQueue = new Queue<Level>();
    private Level currentLevel = new Level();
    private EntityContainer<Ball> balls = new EntityContainer<Ball>();
    private BallLauncher ballLauncher;
    private EntityContainer<EffectEntity> effects = new EntityContainer<EffectEntity>();

    private Hearts hearts= new Hearts();
    private Timer timer = new Timer();
    private Points points = new Points();
    private Background background = new Background(
        new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"))
    );

    private GameRunning() {
        eventBus.Subscribe(GameEventType.PlayerEvent, player);
        eventBus.Subscribe(GameEventType.StatusEvent, this);
        eventBus.Subscribe(GameEventType.GameStateEvent, this);
        eventBus.Subscribe(GameEventType.TimedEvent, this);

        ballLauncher = new BallLauncher(balls, player);

        ResetState();
    }

    public static GameRunning GetInstance() {
        return GameRunning.instance;
    }

    public void ResetState() {
        eventBus.CancelTimedEvent(101);
        eventBus.CancelTimedEvent(201);
        eventBus.CancelTimedEvent(301);

        player.Reset();
        balls.ClearContainer();
        ballLauncher.AddNewBall();       
        effects.ClearContainer();

        hearts.SetHearts(3);

        StaticTimer.RestartTimer();
        timer.Reset();
    }

    public void UpdateState() {
        timer.UpdateTimer();
        if (timer.TimeLimitExceeded()) { EndGame("LOST"); }
        else {
            player.Move();
            IterateBalls();
            IterateEffects();
        }
    }

    public void RenderState() {
        background.RenderBackground();

        currentLevel.Blocks.RenderEntities();
        player.RenderEntity();
        balls.RenderEntities();
        effects.RenderEntities();

        hearts.RenderHearts();
        timer.RenderTimer();
        points.RenderPoints();
    }

    public void IterateBalls() {
        string status = BallIterator.IterateBalls(currentLevel, player, balls, points, hearts);
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

    public void IterateEffects() {
        effects.Iterate(effect => {
            if (effect.CollidesWith(player)) {
                effect.Pop().EngageEffect(balls, player);
            } else {
                effect.Move();
            }
        });
    }

    public void FlushQueue() {
        levelQueue.Clear();
    }

    public void EndLevel() {
        if (levelQueue.Any()) {
            ResetState();
            currentLevel = levelQueue.Dequeue();
            timer.SetTimeLimit(currentLevel.Meta.TimeLimit);
        } else {
            EndGame("WON");
        }
    }

    public void EndGame(string result) {
        eventBus.RegisterEvent(new GameEvent {
            EventType = GameEventType.GraphicsEvent,
            Message = "DISPLAY_STATS",
            StringArg1 = result,
            IntArg1 = (int)points.GetPoints()
        });
        eventBus.RegisterEvent(new GameEvent {
            EventType = GameEventType.GameStateEvent,
            Message = "CHANGE_STATE",
            StringArg1 = "POST_GAME"
        });
    }

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

    public void ProcessEvent(GameEvent gameEvent) {
        switch (gameEvent.Message) {
            case "SPAWN_POWERUP":
                effects.AddEntity(EffectEntityFactory.CreateRandomPowerup((Vec2F)gameEvent.ObjectArg1));
                break;
            case "SPAWN_HAZARD":
                effects.AddEntity(EffectEntityFactory.CreateRandomHazard((Vec2F)gameEvent.ObjectArg1));
                break;
            case "DISENGAGE_EFFECT":
                ((IEffect)gameEvent.ObjectArg1).DisengageEffect(balls, player);
                break;
            case "GAIN_LIFE":
                hearts.MendHeart();
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
                if (gameEvent.StringArg1 == "GAME_RUNNING") return;
                if (gameEvent.StringArg1 == "GAME_PAUSED") return;
                if (levelQueue.Count > 0) { FlushQueue(); }
                points.ResetPoints();
                break;
            default:
                break;
        }
    }
}
