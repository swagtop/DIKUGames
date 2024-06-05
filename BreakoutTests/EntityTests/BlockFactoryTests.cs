namespace BreakoutTests;

using System.Collections.Generic;
using NUnit.Framework;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Entities;
using Breakout.Entities.Blocks;
using Breakout.GUI;
using Breakout.LevelHandling;

public class BlockFactoryTests {
    private Block block;
    private IBaseImage noImage = new NoImage();
    
    [Test]
    public void CorrectBlockTypesTest() {
        block = BlockFactory.CreateBlock(noImage, noImage, BlockType.Block, 1, 1);
        Assert.That(block is Block);

        block = BlockFactory.CreateBlock(noImage, noImage, BlockType.HardenedBlock, 2, 2);
        Assert.That(block is HardenedBlock);

        block = BlockFactory.CreateBlock(noImage, noImage, BlockType.UnbreakableBlock, 3, 3);
        Assert.That(block is UnbreakableBlock);

        block = BlockFactory.CreateBlock(noImage, noImage, BlockType.PowerupBlock, 3, 3);
        Assert.That(block is PowerupBlock);
    }
}
