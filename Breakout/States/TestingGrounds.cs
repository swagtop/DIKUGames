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
    private EntityContainer<Block> blocks;
    private EntityContainer<Ball> balls;
    private EntityContainer<Powerup> powerups;

    public static TestingGrounds GetInstance() {
        if (TestingGrounds.instance == null) {
            TestingGrounds.instance = new TestingGrounds();
            TestingGrounds.instance.ResetState();
        }
        return TestingGrounds.instance;
    }

    public void ResetState() {
        // BLOCKS
        Image blueBlock = new Image(Path.Combine("Assets", "Images", "blue-block.png"));
        Image blueBlockDamaged = new Image(Path.Combine("Assets", "Images", "blue-block-damaged.png"));
        
        blocks = new EntityContainer<Block>(12);
        for (int i = 0; i < 12; i++) {
            blocks.AddEntity(new Block(
                200, 
                blueBlock, 
                blueBlockDamaged, 
                new StationaryShape(new Vec2F(0.0f + i*0.06f, 0.0f), new Vec2F(0.06f, 0.02f))
            ));
        }

        // BALLS
        Image normalBall = new Image(Path.Combine("Assets", "Images", "ball.png"));

        balls = new EntityContainer<Ball>(1);
        balls.AddEntity(new Ball(
            normalBall,
            new DynamicShape(
                new Vec2F(0.2f, 0.2f), 
                new Vec2F(0.02f, 0.02f),
                new Vec2F(0.02f, 0.01f)
            )
        ));

        // EVENT BUS
        eventBus = BreakoutBus.GetBus();
    }

    public void RenderState() {
        blocks.RenderEntities();
        balls.RenderEntities();
    }

    public void UpdateState() {
        if (blocks.CountEntities() > 0) {
            blocks.Iterate(block => block.HitPoints -= 1);
        }
        balls.Iterate(ball => ball.Move());
    }

    private void KeyPress(KeyboardKey key) {
    }

    private void KeyRelease(KeyboardKey key) {
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