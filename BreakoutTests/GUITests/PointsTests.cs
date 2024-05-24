namespace BreakoutTests;

using NUnit.Framework;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout.GUI;
using Breakout.Entities;

public class PointsTests {
    private Points points;

    [SetUp]
    public void Setup() {
    }
    
    [Test]
    public void ConstructorTest() {
        points = new Points();

        Assert.AreEqual(points.GetPoints(), 0);
    }

    [Test]
    public void AwardPointsTest() {
        points = new Points();
        Block block = BlockFactory.CreateBlock(new NoImage(), new NoImage(), BlockType.Block, 1, 1);
        Block hardenedBlock = BlockFactory.CreateBlock(new NoImage(), new NoImage(), BlockType.HardenedBlock, 1, 1);

        points.AwardPointsFor(block);
        Assert.AreEqual(points.GetPoints(), 100);
        
        points.AwardPointsFor(hardenedBlock);
        Assert.AreEqual(points.GetPoints(), 300);
    }

    [Test]
    public void ResetPointsTest() {
        points = new Points();
        Block block = BlockFactory.CreateBlock(new NoImage(), new NoImage(), BlockType.Block, 1, 1);

        points.AwardPointsFor(block);
        points.ResetPoints();       

        Assert.AreEqual(points.GetPoints(), 0);
    }

    [Test]
    public void GetPointsTest() {
        points = new Points();
        Block block = BlockFactory.CreateBlock(new NoImage(), new NoImage(), BlockType.Block, 1, 1);

        points.AwardPointsFor(block);
        Assert.AreEqual(points.GetPoints(), 100);
    }

    [Test]
    public void UpdatePointsTest() {
        points = new Points();
        Block block = BlockFactory.CreateBlock(new NoImage(), new NoImage(), BlockType.Block, 1, 1);

        points.UpdatePointsDisplay();
        points.AwardPointsFor(block);
        points.UpdatePointsDisplay();
    }

    [Test]
    public void RenderPointsTest() {
        points = new Points();
        points.RenderPoints();
    }
}
