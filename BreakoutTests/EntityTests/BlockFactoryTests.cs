namespace BreakoutTests;

using System.Collections.Generic;
using NUnit.Framework;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Entities;
using Breakout.GUI;
using Breakout.LevelHandling;

public class BlockFactoryTests {

    private Block block;
    private IBaseImage noImgae = new NoImage();

    [SetUp]
    public void Setup() {
    }
    
    [Test]
    public void CorrectBlockTypesTest() {
        block = BlockFactory.CreateBlock(noImgae, noImgae, BlockType.Block, 1, 1);
        Assert.That(block is Block);

        block = BlockFactory.CreateBlock(noImgae, noImgae, BlockType.HardenedBlock, 2, 2);
        Assert.That(block is HardenedBlock);

        block = BlockFactory.CreateBlock(noImgae, noImgae, BlockType.UnbreakableBlock, 3, 3);
        Assert.That(block is UnbreakableBlock);
    }
}
