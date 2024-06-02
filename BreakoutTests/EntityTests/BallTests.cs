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
using Breakout.LevelHandling;

public class BallTests {
    private Vec2F ballExtent = new Vec2F(0.025f, 0.025f);
    private Ball ball;
    private IBaseImage noImage = new NoImage();
    
    [Test]
    public void XOutOfBoundsTest() {
        Vec2F ballPosition = new Vec2F(0.8f, 0.5f);
        Vec2F ballDirection = new Vec2F(0.1f, 0.0f);
        ball = new Ball(
            noImage,
            new DynamicShape(
                ballPosition,
                ballExtent, 
                ballDirection
            )
        );

        for (int i = 0; i < 10000; i++) {
            ball.Move();
            Assert.IsTrue(ball.Shape.Position.X >= 0.0f && ball.Shape.Position.X + ball.Shape.Extent.X <= 1.0f);
        }
    }
    
    [Test]
    public void YOutOfBoundsTest() {
        Vec2F ballPosition = new Vec2F(0.5f, 0.0f);
        Vec2F ballDirection = new Vec2F(0.0f, 0.1f);
        ball = new Ball(
            noImage,
            new DynamicShape(
                ballPosition,
                ballExtent, 
                ballDirection
            )
        );

        for (int i = 0; i < 20; i++) {
            ball.Move();
            Assert.IsTrue(ball.Shape.Position.Y + ball.Shape.Extent.Y <= 1.0f);
        }

        Assert.IsTrue(ball.IsDeleted());
    }

    [Test]
    public void ChangeDirectionsTest() {
        Vec2F ballPosition = new Vec2F(0.5f, 0.0f);
        Vec2F ballDirection = new Vec2F(0.0f, 0.1f);
        ball = new Ball(
            noImage,
            new DynamicShape(
                ballPosition,
                ballExtent, 
                ballDirection
            )
        );

        ball.ChangeDirection(CollisionDirection.CollisionDirUp);
        ball.ChangeDirection(CollisionDirection.CollisionDirDown);
        ball.ChangeDirection(CollisionDirection.CollisionDirLeft);
        ball.ChangeDirection(CollisionDirection.CollisionDirRight);

        Assert.Pass();
    }
}
