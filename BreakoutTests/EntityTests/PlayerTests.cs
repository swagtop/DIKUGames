namespace BreakoutTests;

using NUnit.Framework;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout;
using Breakout.Entities;

public class PlayerTests {
    private Player player;
    private IBaseImage noImage = new NoImage();
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
            noImage
        );
    }
    
    [Test]
    public void OutOfBoundsLeftTest() {
        player.ProcessEvent(moveLeftStart);
        for (int i = 0; i > 900; i++) { player.Move(); }
        Assert.True(player.Shape.Position.X >= 0.0f);
    }

    [Test]
    public void OutOfBoundsRightTest() {
        player.ProcessEvent(moveRightStart);
        for (int i = 0; i > 900; i++) { player.Move(); }
        Assert.True(player.Shape.Position.X + player.Shape.Extent.X  <= 1.0f);
    }
    
    [Test]
    public void OnlyMovesByXAxis() {
        var originalY = player.Shape.Position.Y;

        player.ProcessEvent(moveLeftStart);
        for (int i = 0; i > 70; i++) { player.Move(); }
        player.ProcessEvent(moveLeftStop);
        
        player.ProcessEvent(moveRightStart);
        for (int i = 0; i > 20; i++) { player.Move(); }
        player.ProcessEvent(moveLeftStart);
        for (int i = 0; i > 5; i++) player.Move();
        player.ProcessEvent(moveRightStop);
        for (int i = 0; i > 70; i++) player.Move();
        player.ProcessEvent(moveLeftStop);

        Assert.AreEqual(player.Shape.Position.Y, originalY);
    }

    [Test]
    public void IsEntityTest() {
        Assert.That(player, Is.InstanceOf<Entity>());
    }
    
    [Test]
    public void IsRectangularTest() {
        Assert.IsTrue(player.Shape.Extent.X > player.Shape.Extent.Y);
    }
    
    [Test]
    public void InBottomHalfTest() {
        Assert.IsTrue(player.Shape.Position.Y < 0.5f);
    }

    [Test]
    public void ResetNoExceptionTest() {
        player.Reset();
    }

    [Test]
    public void GetFatTest() {
        Vec2F beforeExtent = player.Shape.Extent.Copy();

        player.GetFat();

        Assert.That(beforeExtent.X < player.Shape.Extent.X);
    }
    
    [Test]
    public void GetSkinnyTest() {
        player.GetFat();

        Vec2F fatExtent = player.Shape.Extent.Copy();

        player.GetSkinny();

        Assert.That(fatExtent.X > player.Shape.Extent.X);
    }

    [Test]
    public void GetFatTwiceTest() {
        player.GetFat();

        Vec2F fatExtent = player.Shape.Extent.Copy();

        player.GetFat();

        Assert.That(fatExtent.X == player.Shape.Extent.X);
    }

    [Test]
    public void GetSkinnyTwiceTest() {
        player.GetSkinny();

        Vec2F skinnyExtent = player.Shape.Extent.Copy();

        player.GetSkinny();

        Assert.That(skinnyExtent.X == player.Shape.Extent.X);
    }
}
