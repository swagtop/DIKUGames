namespace BreakoutTests;

using System.Collections.Generic;
using NUnit.Framework;
using DIKUArcade.Entities;
using Breakout.Entities;
using Breakout.Entities.Blocks;
using Breakout.GUI;
using Breakout.LevelHandling;

public class LevelTests {
    private Level level;

    [Test]
    public void EmptyLevelTest() {
        level = new Level();

        LevelMeta emptyMeta = new LevelMeta();

        Assert.AreEqual(level.Blocks.CountEntities(), 0);
        Assert.AreEqual(level.Meta.LevelName, emptyMeta.LevelName);
        Assert.AreEqual(level.Meta.TimeLimit, emptyMeta.TimeLimit);
        Assert.That(level.Meta.CharDictionary is Dictionary<char, BlockType>);
        Assert.AreEqual(level.Meta.CharDictionary.Count, emptyMeta.CharDictionary.Count);
        Assert.AreEqual(level.BreakableLeft, 0);
    }

    [Test]
    public void ConstructedLevelTest() {
        LevelMeta exampleMeta = new LevelMeta("Level name", 100, new Dictionary<char, BlockType>());
        EntityContainer<Block> blocks = new EntityContainer<Block>();
        level = new Level(exampleMeta, blocks, (uint)blocks.CountEntities());

        Assert.AreEqual(level.Blocks.CountEntities(), 0);
        Assert.AreEqual(level.Meta.LevelName, "Level name");
        Assert.AreEqual(level.Meta.TimeLimit, 100);
        Assert.That(level.Meta.CharDictionary is Dictionary<char, BlockType>);
        Assert.AreEqual(level.Meta.CharDictionary.Count, 0);
        Assert.That(level.BreakableLeft is uint);
        Assert.AreEqual(level.BreakableLeft, 0);
    }
}
