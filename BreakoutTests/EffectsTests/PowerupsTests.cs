namespace BreakoutTests;

using System.IO;
using NUnit.Framework;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using Breakout;
using Breakout.Entities;
using Breakout.Entities.Blocks;
using Breakout.Effects;
using Breakout.Effects.Hazards;
using Breakout.Effects.Powerups;
using Breakout.LevelHandling;

public class PowerupsTests {
    private Player defaultPlayer = new Player();
    private Ball defaultBall = new Ball(
        new NoImage(),
        new DynamicShape(
            new Vec2F(0.5f, 0.5f),
            new Vec2F(0.025f, 0.025f),
            new Vec2F(0.0f, 0.1f)
        )
    );
    private EntityContainer<Ball> balls = new EntityContainer<Ball>();

    [SetUp]
    public void SetUp() {
        defaultPlayer.Reset();
        balls.ClearContainer();
        balls.AddEntity(defaultBall.Clone());
        balls.AddEntity(defaultBall.Clone());
        balls.AddEntity(defaultBall.Clone());
    }

    [Test]
    public void SplitTest() {
        IEffect split = new Split();
        split.EngageEffect(balls, defaultPlayer);

        Assert.AreEqual(balls.CountEntities(), 9);
    }

    [Test]
    public void DoubleSizeEngageTest() {
        IEffect doubleSize = new DoubleSize();

        float sizeSumBefore = 0;
        balls.Iterate(ball => { sizeSumBefore += ball.Shape.Extent.X; });

        doubleSize.EngageEffect(balls, defaultPlayer);

        float sizeSumAfter = 0;
        balls.Iterate(ball => { sizeSumAfter += ball.Shape.Extent.X; });

        Assert.AreEqual(sizeSumBefore*2, sizeSumAfter);
    }

    [Test]
    public void DoubleSizeDisengageTest() {
        IEffect doubleSize = new DoubleSize();

        doubleSize.EngageEffect(balls, defaultPlayer);

        float sizeSumBefore = 0;
        balls.Iterate(ball => { sizeSumBefore += ball.Shape.Extent.X; });

        doubleSize.DisengageEffect(balls, defaultPlayer);

        float sizeSumAfter = 0;
        balls.Iterate(ball => { sizeSumAfter += ball.Shape.Extent.X; });

        Assert.AreEqual(sizeSumBefore/2, sizeSumAfter);
    }

    [Test]
    public void WideEngageTest() {
        IEffect wide = new Wide();

        float sizeBefore = defaultPlayer.Shape.Extent.X;
        
        wide.EngageEffect(balls, defaultPlayer);

        float sizeAfter = defaultPlayer.Shape.Extent.X;

        Assert.AreEqual(sizeBefore*2, sizeAfter);
    }

    [Test]
    public void WideSizeDisengageTest() {
        IEffect wide = new Wide();

        wide.EngageEffect(balls, defaultPlayer);
        
        float sizeBefore = defaultPlayer.Shape.Extent.X;
        
        wide.DisengageEffect(balls, defaultPlayer);

        float sizeAfter = defaultPlayer.Shape.Extent.X;

        Assert.AreEqual(sizeBefore/2, sizeAfter);
    }

    [Test]
    public void HardBallEngageTest() {
        IEffect hardBall = new HardBall();

        hardBall.EngageEffect(balls, defaultPlayer);
        foreach (Ball ball in balls) {
            Assert.That(ball.IsHard);
        }
    }

    [Test]
    public void HardBallDisengageTest() {
        IEffect hardBall = new HardBall();

        hardBall.EngageEffect(balls, defaultPlayer);
        hardBall.DisengageEffect(balls, defaultPlayer);
        foreach (Ball ball in balls) {
            Assert.That(!ball.IsHard);
        }
    }
}
