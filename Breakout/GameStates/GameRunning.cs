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
using Breakout.GUI;
using Breakout.LevelHandling;
using Breakout.PowerupEffects;

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

    private static readonly Vec2F defaultBallExtent = new Vec2F(0.025f, 0.025f);
    private static readonly Vec2F defaultBallDirection = new Vec2F(0.0f, 0.0150f);

    private GameRunning() {
        eventBus.Subscribe(GameEventType.PlayerEvent, player);
        eventBus.Subscribe(GameEventType.StatusEvent, this);
        eventBus.Subscribe(GameEventType.GameStateEvent, this);

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
        int ballCount = balls.CountEntities();

        balls.Iterate(ball => {
            ball.Move();
            CollisionData colCheckPlayer = CollisionDetection.Aabb(
                ball.Dynamic, 
                player.Shape.AsDynamicShape()
            );

            if (colCheckPlayer.Collision) {
                float ballMiddle = ball.Shape.Position.X + (ball.Shape.Extent.X / 2.0f);
                float playerMiddle = player.Shape.Position.X + (player.Shape.Extent.X / 2.0f);
                float relativeRotation = (playerMiddle - ballMiddle) * 12.0f;

                ball.ChangeDirection(colCheckPlayer.CollisionDir);

                Vec2F newDir = defaultBallDirection.Copy();

                ball.Dynamic.ChangeDirection(new Vec2F(
                    newDir.X * MathF.Cos(relativeRotation) - newDir.Y * MathF.Sin(relativeRotation),
                    newDir.X * MathF.Sin(relativeRotation) + newDir.Y * MathF.Cos(relativeRotation)
                ));
            }

            currentLevel.Blocks.Iterate(block => {
                CollisionData colCheckBlock = CollisionDetection.Aabb(
                    ball.Dynamic, 
                    block.Shape
                );
                if (colCheckBlock.Collision) {
                    block.Hit();
                    ball.ChangeDirection(colCheckBlock.CollisionDir);
                }
                if (block.IsDeleted()) {
                    powerups.AddEntity(PowerupFactory.CreateRandomPowerup(block.Shape.Position));
                    points.AwardPointsFor(block);
                    currentLevel.BreakableLeft -= 1;
                }
            });
        });
        
        if (currentLevel.BreakableLeft == 0) {
            EndLevel();
            return;
        }

        bool lostAllBalls = (ballCount != 0 && balls.CountEntities() == 0);
        if (lostAllBalls) {
            bool playerLost = hearts.BreakHeart();
            if (playerLost) { EndGame("LOST"); }
            else { ballLauncher.AddNewBall(); }
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
