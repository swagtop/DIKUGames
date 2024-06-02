namespace BreakoutTests;

using System.IO;
using NUnit.Framework;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout;
using Breakout.Entities;
using Breakout.Entities.Blocks;
using Breakout.LevelHandling;

public class BlockTests {
    private EntityContainer<Block> blocks;
    private IBaseImage noImage = new NoImage();
    private int maxHealth;
    private int maxBlockRows;
    private int maxNumberOfBlocksInRow;
    private float xRatio;
    private float yRatio;

    [SetUp]
    public void Setup() {
        blocks = new EntityContainer<Block>();
        maxHealth = 2;
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
                    maxHealth
                ));
            }
        }
        
        Assert.AreEqual(blocks.CountEntities(), 360);
        for (int i = 0; i < maxHealth; i++) {
            blocks.Iterate(block => block.Hit());
        }
        Assert.AreEqual(blocks.CountEntities(), 0);
    }

    [Test]
    public void KillHardenedBlocksTest() {
        for (int i = 0; i < 30; i++) {
            for (int j = 0; j < 12; j++) {
                blocks.AddEntity(new HardenedBlock(
                    noImage,
                    noImage,
                    new StationaryShape(
                        new Vec2F(j * xRatio, 1.0f - ((i + 1)*yRatio)), 
                        new Vec2F(xRatio, yRatio)
                    )
                ));
            }
        }
        
        Assert.AreEqual(blocks.CountEntities(), 360);
        blocks.Iterate(block => block.Hit());
        Assert.AreEqual(blocks.CountEntities(), 360);
        blocks.Iterate(block => block.Hit());
        Assert.AreEqual(blocks.CountEntities(), 0);
    }
    
    [Test]
    public void UnbreakableBlocksCannotBeKilledTest() {
        for (int i = 0; i < 30; i++) {
            for (int j = 0; j < 12; j++) {
                blocks.AddEntity(new UnbreakableBlock(
                    noImage,
                    noImage,
                    new StationaryShape(
                        new Vec2F(j * xRatio, 1.0f - ((i + 1)*yRatio)), 
                        new Vec2F(xRatio, yRatio)
                    )
                ));
            }
        }
        
        Assert.AreEqual(blocks.CountEntities(), 360);
        for (int i = 0; i < 10000; i++) {
            blocks.Iterate(block => block.Hit());
        }
        Assert.AreEqual(blocks.CountEntities(), 360);
    }

    [Test]
    public void BlocksHealthGoDownWhenHitTest() {
        int maxHealth = 2;

        for (int i = 0; i < 30; i++) {
            for (int j = 0; j < 12; j++) {
                blocks.AddEntity(new Block(
                    noImage,
                    noImage,
                    new StationaryShape(
                        new Vec2F(j * xRatio, 1.0f - ((i + 1)*yRatio)), 
                        new Vec2F(xRatio, yRatio)
                    ),
                    maxHealth
                ));
            }
        }
        
        foreach (Block block in blocks){
            Assert.AreEqual(block.Health, maxHealth);
            block.Hit();
            Assert.AreEqual(block.Health, maxHealth - 1);
        }
    }

    [Test]
    public void BlocksDieAtZeroHealthTest() {
        int maxHealth = 2;

        for (int i = 0; i < 30; i++) {
            for (int j = 0; j < 12; j++) {
                blocks.AddEntity(new Block(
                    noImage,
                    noImage,
                    new StationaryShape(
                        new Vec2F(j * xRatio, 1.0f - ((i + 1)*yRatio)), 
                        new Vec2F(xRatio, yRatio)
                    ),
                    maxHealth
                ));
            }
        }
        
        Assert.AreEqual(blocks.CountEntities(), 360);
        blocks.Iterate(block => block.Health = 0);
        Assert.AreEqual(blocks.CountEntities(), 0);
    }

    [Test]
    public void BlocksAreEntitiesTest() {
        StationaryShape shape = new StationaryShape(
            new Vec2F(0.1f, 0.1f), 
            new Vec2F(0.1f, 0.1f)
        );

        Block block = new Block(noImage, noImage, shape);
        HardenedBlock hardenedBlock = new HardenedBlock(noImage, noImage, shape);
        UnbreakableBlock unbreakableBlock = new UnbreakableBlock(noImage, noImage, shape);
        
        Assert.That(block, Is.InstanceOf<Entity>());
        Assert.That(hardenedBlock, Is.InstanceOf<Entity>());
        Assert.That(unbreakableBlock, Is.InstanceOf<Entity>());
    }

    [Test]
    public void SpecialBlocksAreBlocksTest() {
        StationaryShape shape = new StationaryShape(
            new Vec2F(0.1f, 0.1f), 
            new Vec2F(0.1f, 0.1f)
        );

        Block block = new Block(noImage, noImage, shape);
        HardenedBlock hardenedBlock = new HardenedBlock(noImage, noImage, shape);
        UnbreakableBlock unbreakableBlock = new UnbreakableBlock(noImage, noImage, shape);
        
        Assert.That(block, Is.InstanceOf<Block>());
        Assert.That(hardenedBlock, Is.InstanceOf<Block>());
        Assert.That(unbreakableBlock, Is.InstanceOf<Block>());
    }
}
