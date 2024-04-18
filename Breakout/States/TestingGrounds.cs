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
using Breakout.States;

namespace Breakout.States;
public class TestingGrounds : IGameState {
    private static TestingGrounds instance = null;
    private GameEventBus eventBus;
    private Player player;
    private EntityContainer<Block> blocks;

    public static TestingGrounds GetInstance() {
        if (TestingGrounds.instance == null) {
            TestingGrounds.instance = new TestingGrounds();
            TestingGrounds.instance.ResetState();
        }
        return TestingGrounds.instance;
    }

    public void ResetState() {
        // PLAYER
        Image playerImage = new Image(Path.Combine("Assets", "Images", "player.png"));

        player = new Player(
            new DynamicShape(new Vec2F(0.0f, 0.0f), new Vec2F(0.07f, 0.01375f)),
            playerImage
        );

        // BLOCKS
        Image blueBlock = new Image(Path.Combine("Assets", "Images", "blue-block.png"));
        Image blueBlockDamaged = new Image(Path.Combine("Assets", "Images", "blue-block-damaged.png"));
        
        blocks = new EntityContainer<Block>();
        for (int i = 0; i < 12; i++) {
            blocks.AddEntity(new Block(
                blueBlock, 
                blueBlockDamaged, 
                new StationaryShape(new Vec2F(0.0f + i*0.06f, 0.0f), new Vec2F(0.06f, 0.02f)),
                true, // Hardened?
                true, // Unbreakable?
                true  // Drops powerup?
            ));
        }

        // EVENT BUS
        eventBus = BreakoutBus.GetBus();
        eventBus.Subscribe(GameEventType.PlayerEvent, player);
    }

    public void RenderState() {
        blocks.RenderEntities();
        player.RenderEntity();
    }

    public void UpdateState() {
        blocks.Iterate(block => block.HitPoints -= 1);
        player.Move();
    }

    private void KeyPress(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Escape:
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_PAUSED"
                });
                break;
            case KeyboardKey.Left:
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    To = player,
                    Message = "MOVE",
                    StringArg1 = "LEFT",
                    StringArg2 = "START"
                });
                break;
            case KeyboardKey.Right:
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    To = player,
                    Message = "MOVE",
                    StringArg1 = "RIGHT",
                    StringArg2 = "START"
                });
                break;
        }
    }

    private void KeyRelease(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Left:
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    To = player,
                    Message = "MOVE",
                    StringArg1 = "LEFT",
                    StringArg2 = "STOP"
                });
                break;
            case KeyboardKey.Right:
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

}