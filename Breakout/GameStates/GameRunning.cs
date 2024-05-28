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
using Breakout.PowerupEffects;
using Breakout.HazardEffects;

public class GameRunning : IGameState, IGameEventProcessor {
    private static GameRunning instance = new GameRunning();
    private GameEventBus eventBus = BreakoutBus.GetBus();
    private Background background = new Background(
        new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"))
    );
    
    private Player player = new Player();
    private Queue<Level> levelQueue = new Queue<Level>();
    private Level currentLevel = new Level();
    private EntityContainer<Ball> balls = new EntityContainer<Ball>();
    private BallLauncher ballLauncher;
    private EntityContainer<Powerup> powerups = new EntityContainer<Powerup>();

    private Hearts hearts= new Hearts();
    private Timer timer = new Timer();
    private Points points = new Points();

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
        player.Reset();
        balls.ClearContainer();
        ballLauncher.AddNewBall();       
        powerups.ClearContainer();

        hearts.SetHearts(3);

        StaticTimer.RestartTimer();
        timer.Reset();
    }

    public void UpdateState() {
        timer.UpdateTimer(StaticTimer.GetElapsedSeconds());
        player.Move();
        IterateBalls();
        powerups.Iterate(powerup => {
            powerup.Move();
            CollisionData colCheckPlayer = CollisionDetection.Aabb(
                powerup.Shape.AsDynamicShape(), 
                player.Shape.AsStationaryShape()
            );
            if (colCheckPlayer.Collision) {
                powerup.Pop().EngagePowerup(balls, player);
            }
        });
        
        if (timer.TimeIsUp(StaticTimer.GetElapsedSeconds())) {
            EndGame("LOST");
        }
    }

    public void RenderState() {
        background.RenderBackground();

        currentLevel.Blocks.RenderEntities();
        player.RenderEntity();
        balls.RenderEntities();
        powerups.RenderEntities();

        hearts.RenderHearts();
        timer.RenderTimer();
        points.RenderPoints();
    }

    public void IterateBalls() {
        string status = BallIterator.IterateBalls(currentLevel, player, balls, points, hearts);
        switch (status) {
            case "CONTINUE":
                break;
            case "LOAD_BALL":
                ballLauncher.AddNewBall();
                break;
            case "END_LEVEL":
                EndLevel();
                break;
            case "GAME_WON":
                EndGame("WON");
                break;
            case "GAME_LOST":
                EndGame("LOST");
                break;
        }
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
                currentLevel.Blocks.Iterate(block => block.Hit());
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
                powerups.AddEntity(PowerupFactory.CreateRandomPowerup((Vec2F)gameEvent.ObjectArg1));
                break;
            case "SPAWN_HAZARD":
                //
                break;
            case "DISENGAGE_POWERUP":
                ((IPowerupEffect)gameEvent.ObjectArg1).DisengagePowerup(balls, player);
                break;
            case "DISENGAGE_HAZARD":
                ((IHazardEffect)gameEvent.ObjectArg1).DisengageHazard(balls, player);
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
