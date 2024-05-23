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
using Breakout.Entities;
using Breakout.GUI;
using Breakout.LevelHandling;
using Breakout.MovementStrategies;

public class GameRunning : IGameState, IGameEventProcessor {
    private static GameRunning instance = new GameRunning();
    private GameEventBus eventBus = BreakoutBus.GetBus();
    private Background background = new Background(
        new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"))
    );
    private Random rnd = new Random();
    private Player player = new Player(
        new DynamicShape(new Vec2F((1.0f - 0.07f)/2.0f, 0.0f), new Vec2F(0.14f, 0.0275f)),
        new Image(Path.Combine("Assets", "Images", "player.png"))
    );
    private Level currentLevel = new Level();
    private Queue<Level> levelQueue = new Queue<Level>();
    private EntityContainer<Ball> balls = new EntityContainer<Ball>();
    private IMovementStrategy movementStrategy= new StandardMove();
    private Hearts hearts= new Hearts(3);
    private Timer timer = new Timer();
    private Points points = new Points();

    private GameRunning() {
        eventBus.Subscribe(GameEventType.PlayerEvent, player);
        eventBus.Subscribe(GameEventType.StatusEvent, this);
        eventBus.Subscribe(GameEventType.GameStateEvent, this);
    }

    public static GameRunning GetInstance() {
        return GameRunning.instance;
    }

    public void ResetState() {
        player.Reset();
        balls.ClearContainer();
        
        float rotation = rnd.NextSingle() * 0.75f - rnd.NextSingle() * 0.75f;
        
        Vec2F ballExtent = new Vec2F(0.025f, 0.025f);
        Vec2F ballPosition = new Vec2F(
            player.Shape.Position.X + (player.Shape.Extent.X/2) - ballExtent.X /2, 
            player.Shape.Extent.Y
        );
        Vec2F ballDirection = new Vec2F(0.0f, 0.0150f);
        balls.AddEntity(new Ball(
            new Image(Path.Combine("Assets", "Images", "ball.png")),
            new DynamicShape(ballPosition, ballExtent, ballDirection)
        ));

        hearts.Amount = 3;

        StaticTimer.RestartTimer();
        timer.Reset();
    }

    public void UpdateState() {
        timer.UpdateTimer(StaticTimer.GetElapsedSeconds());
        player.Move();
        IterateBalls();
        if (hearts.Amount < 0 || timer.TimeIsUp(StaticTimer.GetElapsedSeconds())) {
            ResetState();
            eventBus.RegisterEvent(new GameEvent{
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                StringArg1 = "GAME_OVER"
            });
        }
    }

    public void RenderState() {
        background.RenderBackground();
        currentLevel.Blocks.RenderEntities();
        player.RenderEntity();
        balls.RenderEntities();
        hearts.RenderHearts();
        timer.Render();
        points.RenderPoints();
    }

    public void IterateBalls() {
        int ballCount = balls.CountEntities();

        balls.Iterate(ball => {
            movementStrategy.Move(ball);
            CollisionData colCheckPlayer = CollisionDetection.Aabb(
                ball.Dynamic, 
                player.Shape.AsDynamicShape()
            );

            if (colCheckPlayer.Collision) {
                float ballMiddle = ball.Shape.Position.X - (ball.Shape.Extent.X / 2.0f);
                float playerMiddle = player.Shape.Position.X + (player.Shape.Extent.X / 2.0f);
                float relativeRotation = (ballMiddle - playerMiddle) * -12.0f;

                ball.ChangeDirection(colCheckPlayer.CollisionDir);

                float ballDirX = ball.Dynamic.Direction.X;
                float ballDirY = ball.Dynamic.Direction.Y;

                ball.Dynamic.ChangeDirection(new Vec2F(
                    ballDirX * MathF.Cos(relativeRotation) - ballDirY * MathF.Sin(relativeRotation),
                    ballDirX * MathF.Sin(relativeRotation) + ballDirY * MathF.Cos(relativeRotation)
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
                    points.AwardPointsFor(block);
                }
            });

        });

        if (ballCount != 0 && balls.CountEntities() == 0) {
            hearts.BreakHeart();
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
            EndGame();
        }
    }

    public void EndGame() {
        ResetState();
        eventBus.RegisterEvent(new GameEvent {
            EventType = GameEventType.GraphicsEvent,
            Message = "DISPLAY_STATS",
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
            case KeyboardKey.Left: case KeyboardKey.A:
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE",
                    StringArg1 = "LEFT",
                    StringArg2 = "STOP"
                });
                break;
            case KeyboardKey.Right: case KeyboardKey.D:
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
