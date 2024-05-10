using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Events;
using DIKUArcade.Math;
using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using Breakout;
using Breakout.Entities;
using Breakout.LevelHandling;
using Breakout.MovementStrategies;

namespace Breakout.GameStates;
public class GameRunning : IGameState, IGameEventProcessor
{
    private static GameRunning instance = new GameRunning();
    private GameEventBus eventBus = BreakoutBus.GetBus();
    private Player player = new Player(
        new DynamicShape(new Vec2F((1.0f - 0.07f) / 2.0f, 0.0f), new Vec2F(0.14f, 0.0275f)),
        new Image(Path.Combine("Assets", "Images", "player.png"))
    );
    private Level level = new Level();
    private Entity backGroundImage = new Entity(
        new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
        new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"))
    );
    private EntityContainer<Ball> balls = new EntityContainer<Ball>();
    private IMovementStrategy movementStrategy = new StandardMove();

    public static GameRunning GetInstance()
    {
        return GameRunning.instance;
    }

    public void ResetState()
    {
        player.Reset();
        eventBus.Subscribe(GameEventType.PlayerEvent, player);
        balls.ClearContainer();
        balls.AddEntity(new Ball(
            new Image(Path.Combine("Assets", "Images", "ball.png")),
            new DynamicShape(new Vec2F(0.4875f, 0.0f), new Vec2F(0.025f, 0.025f), new Vec2F(0.0f, 0.0175f))
        ));

    }

    public void RenderState()
    {
        backGroundImage.RenderEntity();
        level.Blocks.RenderEntities();
        player.RenderEntity();
        balls.RenderEntities();
    }

    public void UpdateState()
    {
        player.Move();
        IterateBalls();
    }
    public void IterateBalls()
    {
        balls.Iterate(ball =>
        {
            movementStrategy.Move(ball);
            CollisionData colCheck1 = CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), player.Shape.AsDynamicShape());
            if (colCheck1.Collision)
            {
                float rot = -(ball.Shape.Position.X - (player.Shape.Position.X + (player.Shape.Extent.X / 2.0f)));
                Console.WriteLine(rot);
                rot *= 10;
                Console.WriteLine(rot);
                ball.ChangeDirection(colCheck1.CollisionDir);
                ball.Shape.AsDynamicShape().ChangeDirection(new Vec2F(ball.Shape.AsDynamicShape().Direction.X * (float)Math.Cos(rot) - ball.Shape.AsDynamicShape().Direction.Y * (float)Math.Sin(rot), ball.Shape.AsDynamicShape().Direction.X * (float)Math.Sin(rot) + ball.Shape.AsDynamicShape().Direction.Y * (float)Math.Cos(rot)));

            }

            level.Blocks.Iterate(block =>
            {
                CollisionData colCheck2 = CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape);
                if (colCheck2.Collision)
                {
                    block.Hit();
                    ball.ChangeDirection(colCheck2.CollisionDir);
                }

            });

        });

    }

    private void KeyPress(KeyboardKey key)
    {
        switch (key)
        {
            case KeyboardKey.Escape:
                eventBus.RegisterEvent(new GameEvent
                {
                    EventType = GameEventType.PlayerEvent,
                    To = player,
                    Message = "STOP",
                });
                eventBus.RegisterEvent(new GameEvent
                {
                    EventType = GameEventType.GameStateEvent,
                    To = StateMachine.GetInstance(),
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_PAUSED"
                });
                break;
            case KeyboardKey.Left:
            case KeyboardKey.A:
                eventBus.RegisterEvent(new GameEvent
                {
                    EventType = GameEventType.PlayerEvent,
                    To = player,
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
                    To = player,
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
                    To = player,
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

    public void ProcessEvent(GameEvent gameEvent)
    {
        if (gameEvent.EventType != GameEventType.GameStateEvent) return;

        if (gameEvent.Message == "LOAD_LEVEL")
        {
            this.ResetState();
            level = (Level)gameEvent.ObjectArg1;
        }
    }
}