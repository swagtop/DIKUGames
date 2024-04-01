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
        
        blocks = new EntityContainer<Block>(1);
        blocks.AddEntity(new Block(
            4, 
            blueBlock, 
            blueBlockDamaged, 
            new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(0.09f, 0.03f))
        ));

        // BALLS
        Image normalBall = new Image(Path.Combine("Assets", "Images", "ball.png"));

        balls = new EntityContainer<Ball>(1);
        balls.AddEntity(new Ball(
            normalBall,
            new DynamicShape(new Vec2F(0.2f, 0.2f), new Vec2F(0.03f, 0.03f))
        ));

        // EVENT BUS
        eventBus = BreakoutBus.GetBus();
    }

    public void RenderState() {
        blocks.RenderEntities();
        balls.RenderEntities();
    }

    public void UpdateState() {
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