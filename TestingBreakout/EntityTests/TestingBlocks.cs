using System.IO;
using NUnit.Framework;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using Breakout;
using Breakout.Entities;
using Breakout.HitStrategies;
using Breakout.LevelHandling;

namespace BreakoutTests;
public class TestingBlocks {
    private EntityContainer<Block> blocks;
    private IHitStrategy hitStrategy = new StandardHit();
    private IBaseImage noImage = new NoImage();
    private int maxBlockRows;
    private int maxNumberOfBlocksInRow;
    private float xRatio;
    private float yRatio;

    [SetUp]
    public void Setup() {
        blocks = new EntityContainer<Block>();
        maxBlockRows = 30;
        maxNumberOfBlocksInRow = 12;
        xRatio = 1.0f/maxNumberOfBlocksInRow;
        yRatio = xRatio/3.0f;
    }
    
    [Test]
    public void KillBlocksTest() {
        for (int i = 0; i < 30; i++) {
            for (int j = 0; j < 12; j++) {
                blocks.AddEntity(new Block(
                    noImage,
                    noImage,
                    new StationaryShape(
                        new Vec2F(j * xRatio, 1.0f - ((i + 1)*yRatio)), 
                        new Vec2F(xRatio, yRatio)
                    ),
                    false,
                    false
                ));
            }
        }
        
        Assert.AreEqual(blocks.CountEntities(), 360);
        blocks.Iterate(block => hitStrategy.Hit(block));
        blocks.Iterate(block => hitStrategy.Hit(block));
        Assert.AreEqual(blocks.CountEntities(), 0);
    }

    [Test]
    public void KillHardenedBlocksTest() {
        for (int i = 0; i < 30; i++) {
            for (int j = 0; j < 12; j++) {
                blocks.AddEntity(new Block(
                    noImage,
                    noImage,
                    new StationaryShape(
                        new Vec2F(j * xRatio, 1.0f - ((i + 1)*yRatio)), 
                        new Vec2F(xRatio, yRatio)
                    ),
                    true,
                    false
                ));
            }
        }
        
        Assert.AreEqual(blocks.CountEntities(), 360);
        blocks.Iterate(block => hitStrategy.Hit(block));
        blocks.Iterate(block => hitStrategy.Hit(block));
        Assert.AreEqual(blocks.CountEntities(), 360);
        blocks.Iterate(block => hitStrategy.Hit(block));
        blocks.Iterate(block => hitStrategy.Hit(block));
        Assert.AreEqual(blocks.CountEntities(), 0);
    }
    
    [Test]
    public void UnbreakableBlocksCannotBeKilledTest() {
        for (int i = 0; i < 30; i++) {
            for (int j = 0; j < 12; j++) {
                blocks.AddEntity(new Block(
                    noImage,
                    noImage,
                    new StationaryShape(
                        new Vec2F(j * xRatio, 1.0f - ((i + 1)*yRatio)), 
                        new Vec2F(xRatio, yRatio)
                    ),
                    false,
                    true
                ));
            }
        }
        
        Assert.AreEqual(blocks.CountEntities(), 360);
        for (int i = 0; i < 10000; i++) {
            blocks.Iterate(block => hitStrategy.Hit(block));
        }
        Assert.AreEqual(blocks.CountEntities(), 360);
    }

    [Test]
    public void BlocksHealthGoDownWhenHitTest() {
        for (int i = 0; i < 30; i++) {
            for (int j = 0; j < 12; j++) {
                blocks.AddEntity(new Block(
                    noImage,
                    noImage,
                    new StationaryShape(
                        new Vec2F(j * xRatio, 1.0f - ((i + 1)*yRatio)), 
                        new Vec2F(xRatio, yRatio)
                    ),
                    false,
                    false
                ));
            }
        }
        
        foreach (Block block in blocks){
            Assert.AreEqual(block.Health, 2);
            hitStrategy.Hit(block);
            Assert.AreEqual(block.Health, 1);
        }
    }

    [Test]
    public void BlocksDieAtZeroHpTest() {
        for (int i = 0; i < 30; i++) {
            for (int j = 0; j < 12; j++) {
                blocks.AddEntity(new Block(
                    noImage,
                    noImage,
                    new StationaryShape(
                        new Vec2F(j * xRatio, 1.0f - ((i + 1)*yRatio)), 
                        new Vec2F(xRatio, yRatio)
                    ),
                    false,
                    false
                ));
            }
        }
        
        Assert.AreEqual(blocks.CountEntities(), 360);
        for (int i = 0; i < 10000; i++) {
            blocks.Iterate(block => block.Health = 0);
        }
        Assert.AreEqual(blocks.CountEntities(), 0);
    }
}