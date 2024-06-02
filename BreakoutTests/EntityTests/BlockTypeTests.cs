namespace BreakoutTests;

using System.IO;
using NUnit.Framework;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout;
using Breakout.Entities;
using Breakout.LevelHandling;

public class BlockTypeTests {

    [SetUp]
    public void Setup() {
    }
    
    [Test]
    public void TransformStringToTypeTest() {
        Assert.AreEqual(BlockTypeTransformer.TransformStringToType("BLOCK"), BlockType.Block);
        Assert.AreEqual(BlockTypeTransformer.TransformStringToType("UNBREAKABLE"), BlockType.UnbreakableBlock);
        Assert.AreEqual(BlockTypeTransformer.TransformStringToType("HARDENED"), BlockType.HardenedBlock);
    }
    
    [Test]
    public void TransformTypeToStringTest() {
        Assert.AreEqual(BlockTypeTransformer.TransformTypeToString(BlockType.Block), "BLOCK");
        Assert.AreEqual(BlockTypeTransformer.TransformTypeToString(BlockType.UnbreakableBlock), "UNBREAKABLE");
        Assert.AreEqual(BlockTypeTransformer.TransformTypeToString(BlockType.HardenedBlock), "HARDENED");
    }
}
