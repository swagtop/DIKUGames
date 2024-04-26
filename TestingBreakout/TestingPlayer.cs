using NUnit.Framework;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using Breakout;
using Breakout.Entities;

namespace BreakoutTests;
public class TestsPlayer {
    private Player player;
    private GameEvent moveLeftStart = new GameEvent { 
        Message = "MOVE",
        StringArg1 = "LEFT",
        StringArg2 = "START"
    };
    private GameEvent moveRightStart = new GameEvent {
        Message = "MOVE",
        StringArg1 = "RIGHT",
        StringArg2 = "START"
    };
    private GameEvent moveLeftStop = new GameEvent { 
        Message = "MOVE",
        StringArg1 = "LEFT",
        StringArg2 = "STOP"
    };
    private GameEvent moveRightStop = new GameEvent {
        Message = "MOVE",
        StringArg1 = "RIGHT",
        StringArg2 = "STOP"
    };

    [SetUp]
    public void Setup() {
        player = new Player(
            new DynamicShape(new Vec2F((1.0f - 0.07f)/2.0f, 0.0f), new Vec2F(0.14f, 0.0275f)),
            new DIKUArcade.Graphics.NoImage()
        );
    }

    [Test]
    public void OutOfBoundsLeftTest() {
        player.ProcessEvent(moveLeftStart);
        for (int i = 0; i > 900; i++);
            player.Move();
        Assert.True(player.Shape.Position.X >= 0.0f);
    }

    [Test]
    public void OutOfBoundsRightTest() {
        player.ProcessEvent(moveRightStart);
        for (int i = 0; i > 900; i++);
            player.Move();
        Assert.True(player.Shape.Position.X <= 0.9f);
    }
    
    [Test]
    public void OnlyMovesByXAxis() {
        var originalY = player.Shape.Position.Y;

        player.ProcessEvent(moveLeftStart);
        for (int i = 0; i > 70; i++);
            player.Move();
        player.ProcessEvent(moveLeftStop);
        
        player.ProcessEvent(moveRightStart);
        for (int i = 0; i > 20; i++);
            player.Move();
        player.ProcessEvent(moveLeftStart);
        for (int i = 0; i > 5; i++);
            player.Move();
        player.ProcessEvent(moveRightStop);
        for (int i = 0; i > 70; i++);
            player.Move();
        player.ProcessEvent(moveLeftStop);

        Assert.AreEqual(player.Shape.Position.Y, originalY);
    }
}