namespace Breakout.GameStates;

using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.State;
using Breakout;
using Breakout.Entities;
using Breakout.GUI;
using Breakout.LevelHandling;
using Breakout.MovementStrategies;

public class GameRunning : IGameState, IGameEventProcessor {
    private static GameRunning instance = new GameRunning();
    private GameEventBus eventBus = BreakoutBus.GetBus();
    private Random rnd = new Random();
    private Player player = new Player(
        new DynamicShape(new Vec2F((1.0f - 0.07f)/2.0f, 0.0f), new Vec2F(0.14f, 0.0275f)),
        new Image(Path.Combine("Assets", "Images", "player.png"))
    );
    private Level level = new Level();
    private Queue<Level> levelQueue = new Queue<Level>();
    private Entity backGroundImage = new Entity(
        new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
        new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"))
    );
    private EntityContainer<Ball> balls = new EntityContainer<Ball>();
    private IMovementStrategy movementStrategy= new StandardMove();
    private Hearts hearts= new Hearts(
            3,
            new Image(Path.Combine("Assets", "Images", "heart_filled.png")),
            new Image(Path.Combine("Assets", "Images", "heart_empty.png"))
    );

    public static GameRunning GetInstance() {
        return GameRunning.instance;
    }

    public void ResetState() {
        player.Reset();
        balls.ClearContainer();
        
        eventBus.Subscribe(GameEventType.PlayerEvent, player);
        eventBus.Subscribe(GameEventType.GameStateEvent, this);
        
        float rotation = rnd.NextSingle() * 0.75f - rnd.NextSingle() * 0.75f;
        
        Vec2F ballExtent = new Vec2F(0.025f, 0.025f);
        Vec2F ballPosition = new Vec2F(
            player.Shape.Position.X + (player.Shape.Extent.X/2) - ballExtent.X /2, 
            player.Shape.Extent.Y
        );
        Vec2F ballDirection = new Vec2F(0.0f, 0.0150f);
        ballDirection.X = ballDirection.X * (float)Math.Cos(rotation) - ballDirection.Y * (float)Math.Sin(rotation);
        ballDirection.Y = ballDirection.X * (float)Math.Sin(rotation) + ballDirection.Y * (float)Math.Cos(rotation);
        if (ballDirection.Y < 0) {
            ballDirection.Y *= -1;
        }

        balls.AddEntity(new Ball(
            new Image(Path.Combine("Assets", "Images", "ball.png")),
            new DynamicShape(ballPosition, ballExtent, ballDirection)
        ));

        hearts.Amount = 3;
    }

    public void RenderState() {
        backGroundImage.RenderEntity();
        level.Blocks.RenderEntities();
        player.RenderEntity();
        balls.RenderEntities();
        hearts.RenderHearts();
    }


    public void UpdateState() {
        player.Move();
        IterateBalls();
    }

    public void IterateBalls() {
        balls.Iterate(ball => {
            movementStrategy.Move(ball);
            CollisionData colCheck1 = CollisionDetection.Aabb(ball.Dynamic, player.Shape.AsDynamicShape());

            if (colCheck1.Collision) {
                float rotation = (ball.Shape.Position.X - (player.Shape.Position.X + (player.Shape.Extent.X / 2.0f) - ball.Shape.Extent.X / 2.0f));
                rotation *= -12.0f;
                ball.ChangeDirection(colCheck1.CollisionDir);
                ball.Dynamic.ChangeDirection(new Vec2F(
                    ball.Dynamic.Direction.X * (float)Math.Cos(rotation) - ball.Dynamic.Direction.Y * (float)Math.Sin(rotation), 
                    ball.Dynamic.Direction.X * (float)Math.Sin(rotation) + ball.Dynamic.Direction.Y * (float)Math.Cos(rotation))
                );
            }

            level.Blocks.Iterate(block => {
                CollisionData colCheck2 = CollisionDetection.Aabb(ball.Dynamic, block.Shape);
                if (colCheck2.Collision) {
                    block.Hit();
                    ball.ChangeDirection(colCheck2.CollisionDir);
                }
            });

            if (ball.IsDeleted()) {
                hearts.BreakHeart();
            }
        });
    }

    public void DumpQueue() {
        levelQueue.Clear();
    }

    private void KeyPress(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Escape:
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    To = StateMachine.GetInstance(),
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_PAUSED"
                });
                break;
            case KeyboardKey.Left: 
            case KeyboardKey.A:
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    To = player,
                    Message = "MOVE",
                    StringArg1 = "LEFT",
                    StringArg2 = "START"
                });
                break;
            case KeyboardKey.Right: 
            case KeyboardKey.D:
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    To = player,
                    Message = "MOVE",
                    StringArg1 = "RIGHT",
                    StringArg2 = "START"
                });
                break;
            case KeyboardKey.Space:
                Console.WriteLine("DEBUG: All blocks take one hit.");
                level.Blocks.Iterate(block => block.Hit());
                break;
            case KeyboardKey.Tab:
                if (levelQueue.Any()) {
                    Console.WriteLine("DEBUG: Skipping to next level in queue.");
                    ResetState();
                    level = levelQueue.Dequeue();
                } else {
                    Console.WriteLine("DEBUG: No more levels in queue, returning to main menu.");
                    ResetState();
                    eventBus.RegisterEvent(new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        To = StateMachine.GetInstance(),
                        Message = "CHANGE_STATE",
                        StringArg1 = "MAIN_MENU"
                    });
                }
                break;

        }
    }

    private void KeyRelease(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Left: case KeyboardKey.A:
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    To = player,
                    Message = "MOVE",
                    StringArg1 = "LEFT",
                    StringArg2 = "STOP"
                });
                break;
            case KeyboardKey.Right: case KeyboardKey.D:
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    To = player,
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
        if (gameEvent.EventType != GameEventType.GameStateEvent) return;
        
        switch (gameEvent.Message) {
            case "LOAD_LEVEL":
                ResetState();
                level = (Level)gameEvent.ObjectArg1;
                break;
            case "QUEUE_LEVELS":
                ResetState();
                levelQueue = (Queue<Level>)gameEvent.ObjectArg1;
                level = levelQueue.Dequeue();
                break;
            case "DUMP_QUEUE":
                DumpQueue();
                break;
            default:
                break;
        }
    }
}
