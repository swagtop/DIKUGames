namespace BreakoutTests;

using System.Collections.Generic;
using NUnit.Framework;
using DIKUArcade.Entities;
using Breakout.Entities;
using Breakout.Entities.Blocks;
using Breakout.GUI;
using Breakout.LevelHandling;

public class LevelMetaTests {
    private LevelMeta levelMeta;
    
    [Test]
    public void EmptyLevelMetaTest() {
        levelMeta = new LevelMeta();
        
        Assert.AreEqual(levelMeta.LevelName, "");
        Assert.AreEqual(levelMeta.TimeLimit, -1);
        Assert.That(levelMeta.CharDictionary is Dictionary<char, BlockType>);
        Assert.AreEqual(levelMeta.CharDictionary.Count, 0);
    }

    [Test]
    public void ConstructedLevelMetaTest() {
        levelMeta = new LevelMeta("Level name!", 500, new Dictionary<char, BlockType>());
        
        Assert.AreEqual(levelMeta.LevelName, "Level name!");
        Assert.AreEqual(levelMeta.TimeLimit, 500);
        Assert.That(levelMeta.CharDictionary is Dictionary<char, BlockType>);
        Assert.AreEqual(levelMeta.CharDictionary.Count, 0);
    }
}
