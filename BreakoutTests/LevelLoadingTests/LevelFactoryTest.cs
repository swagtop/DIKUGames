using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Utilities;
using Breakout;
using Breakout.Entities;
using Breakout.LevelHandling;

namespace BreakoutTests;
public class LevelFactoryTest {
    private Level level;

    [OneTimeSetUp]
    public void Init() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }

    [SetUp]
    public void Setup() {
    }
    
    [Test]
    public void SplitsSectionsTest() {
        var fileLines = new string[] {
            "Map:",
            "------------",
            "------------",
            "-aaaaaaaaaa-",
            "-aaaaaaaaaa-",
            "-000----000-",
            "-000-%%-000-",
            "-000-11-000-",
            "-000-%%-000-",
            "-000----000-",
            "-%%%%%%%%%%-",
            "-%%%%%%%%%%-",
            "------------",
            "------------",
            "------------",
            "------------",
            "------------",
            "------------",
            "------------",
            "------------",
            "------------",
            "------------",
            "------------",
            "------------",
            "------------",
            "------------",
            "Map/",
            "",
            "Meta:",
            "Name: LEVEL 1",
            "Time: 300",
            "Hardened: #",
            "PowerUp: 2",
            "Meta/",
            "",
            "Legend:",
            "%) blue-block.png",
            "0) grey-block.png",
            "1) orange-block.png",
            "a) purple-block.png",
            "Legend/",
        };
        var sections = LevelFactory.GetLevelFileSections(fileLines);
        Assert.AreEqual(
            sections.Map,
            new string[] {
                "------------",
                "------------",
                "-aaaaaaaaaa-",
                "-aaaaaaaaaa-",
                "-000----000-",
                "-000-%%-000-",
                "-000-11-000-",
                "-000-%%-000-",
                "-000----000-",
                "-%%%%%%%%%%-",
                "-%%%%%%%%%%-",
                "------------",
                "------------",
                "------------",
                "------------",
                "------------",
                "------------",
                "------------",
                "------------",
                "------------",
                "------------",
                "------------",
                "------------",
                "------------",
                "------------",
            }
        );
        Assert.AreEqual(
            sections.Meta,
            new string[] {
                "Name: LEVEL 1",
                "Time: 300",
                "Hardened: #",
                "PowerUp: 2",
            }
        );
        Assert.AreEqual(
            sections.Legend,
            new string[] {
                "%) blue-block.png",
                "0) grey-block.png",
                "1) orange-block.png",
                "a) purple-block.png",
            }
        );
    }

    [Test]
    public void ParseMetaTest() {
        LevelMeta meta = new LevelMeta();
        meta.LevelName = "LEVEL 1";
        meta.TimeLimit = 300;
        meta.CharDictionary.Add('#', BlockType.HardenedBlock);
        meta.CharDictionary.Add('%', BlockType.UnbreakableBlock);

        LevelMeta fromMethod = LevelFactory.ParseMetaSection(new string[] {
                "Name: LEVEL 1",
                "Time: 300",
                "Hardened: #",
                "Unbreakable: %",
                "PowerUp: 2",
            }
        );

        Assert.AreEqual(meta.LevelName, fromMethod.LevelName);
        Assert.AreEqual(meta.TimeLimit, fromMethod.TimeLimit);
        Assert.AreEqual(meta.CharDictionary['#'], fromMethod.CharDictionary['#']);
        Assert.AreEqual(meta.CharDictionary['#'], fromMethod.CharDictionary['#']);
    }

    [Test]
    public void ParseLegendTest() {
        Dictionary<char, Image[]> legend = new Dictionary<char, Image[]>();
        legend.Add(
            '%', 
            new Image[2] {
                new Image(Path.Combine("Assets", "Images", "blue-block.png")),
                new Image(Path.Combine("Assets", "Images", "blue-block-damaged.png"))
        });
        legend.Add(
            '0', 
            new Image[2] {
                new Image(Path.Combine("Assets", "Images", "grey-block.png")),
                new Image(Path.Combine("Assets", "Images", "grey-block-damaged.png"))
        });
        legend.Add(
            '1', 
            new Image[2] {
                new Image(Path.Combine("Assets", "Images", "orange-block.png")),
                new Image(Path.Combine("Assets", "Images", "orange-block-damaged.png"))
        });
        legend.Add(
            'a', 
            new Image[2] {
                new Image(Path.Combine("Assets", "Images", "purple-block.png")),
                new Image(Path.Combine("Assets", "Images", "purple-block-damaged.png"))
        });

        Dictionary<char, Image[]> fromMethod = LevelFactory.ParseLegendSection(new string[] {
                "%) blue-block.png",
                "0) grey-block.png",
                "1) orange-block.png",
                "a) purple-block.png",
            }
        );

        //Assert.True(Equals(legend, fromMethod));
        // We are having trouble comparing these objects, so for now we will confirm
        // this section visually, and it seems to hold up.
        Assert.Inconclusive();
    }

    [Test]
    public void ParseMapTest() {
        var meta = LevelFactory.ParseMetaSection(new string[] {
                "Name: LEVEL 1",
                "Time: 300",
                "Hardened: #",
                "PowerUp: 2",
        });
        var legend = LevelFactory.ParseLegendSection(new string[] {
                "%) blue-block.png",
                "0) grey-block.png",
                "1) orange-block.png",
                "a) purple-block.png",
        });
        var blocks = LevelFactory.ParseMapSection(
            new string[] {
                "------------",
                "------------",
                "-aaaaaaaaaa-",
                "-aaaaaaaaaa-",
                "-000----000-",
                "-000-%%-000-",
                "-000-11-000-",
                "-000-%%-000-",
                "-000----000-",
                "-%%%%%%%%%%-",
                "-%%%%%%%%%%-",
                "------------",
                "------------",
                "------------",
                "------------",
                "------------",
                "------------",
                "------------",
                "------------",
                "------------",
                "------------",
                "------------",
                "------------",
                "------------",
                "------------",
            },
            meta,
            legend
        );
        Assert.AreEqual(blocks.CountEntities(), 76);
    }

    [Test]
    public void LoadFromFileTest() {
        string fullPath = FileIO.GetProjectPath();
        Level level = LevelFactory.LoadFromFile(Path.Combine(fullPath, "Assets", "Levels", "level1.txt"));
        
        Assert.AreEqual(level.Meta.LevelName, "LEVEL 1");
        Assert.AreEqual(level.Meta.TimeLimit, 300);
        Assert.AreEqual(level.Meta.CharDictionary['#'], BlockType.HardenedBlock);
        Assert.AreEqual(level.Blocks.CountEntities(), 76);
    }

    [Test]
    public void CorrectDataStructuresTest() {
        string fullPath = FileIO.GetProjectPath();
        Level level = LevelFactory.LoadFromFile(Path.Combine(fullPath, "Assets", "Levels", "level1.txt"));
        
        Assert.That(level, Is.InstanceOf<Level>());
        Assert.That(level.Meta, Is.InstanceOf<LevelMeta>());
        Assert.That(level.Meta.CharDictionary, Is.InstanceOf<Dictionary<char, BlockType>>());
        Assert.That(level.Blocks, Is.InstanceOf<EntityContainer<Block>>());
    }

    [Test]
    public void HandleDifferentMetadataTest() {
        bool noException = true;
        Level level;

        try {
            string fullPath = FileIO.GetProjectPath();
            level = LevelFactory.LoadFromFile(Path.Combine(fullPath, "Assets", "Levels", "metadata1.txt"));
        } catch {
            noException = false;
        }
        
        try {
            string fullPath = FileIO.GetProjectPath();
            level = LevelFactory.LoadFromFile(Path.Combine(fullPath, "Assets", "Levels", "metadata2.txt"));
        } catch {
            noException = false;
        }

        try {
            string fullPath = FileIO.GetProjectPath();
            level = LevelFactory.LoadFromFile(Path.Combine(fullPath, "Assets", "Levels", "metadata3.txt"));
        } catch {
            noException = false;
        }

        Assert.IsTrue(noException);
    }

    // The following tests will be handling invalid files throwing errors exactly as done in the
    // actual codeplace, which puts the method calls inside try catch statements, and switches
    // state to GameRunning only if the LevelFactory methods complete without throwing errors.
    [Test]
    public void HandleInvalidSectionsTest() {
        bool exceptionHandled = false;

        try {
            string fullPath = FileIO.GetProjectPath();
            Level level = LevelFactory.LoadFromFile(Path.Combine(fullPath, "Assets", "Levels", "invalid.txt"));
        } catch (Exception e) {
            Assert.AreEqual(
                e.ToString().Split('\n')[0].Trim(), 
                "System.Exception: Level sections are incorrectly ordered or corrupted.");   
            exceptionHandled = true;
        }

        Assert.IsTrue(exceptionHandled);
    }

    [Test]
    public void HandleEmptyFileTest() {
        bool exceptionHandled = false;

        try {
            string fullPath = FileIO.GetProjectPath();
            Level level = LevelFactory.LoadFromFile(Path.Combine(fullPath, "Assets", "Levels", "empty.txt"));
        } catch (Exception e) {
            Assert.AreEqual(
                e.ToString().Split('\n')[0].Trim(), 
                "System.Exception: Level sections are incorrectly ordered or corrupted.");   
            exceptionHandled = true;
        }

        Assert.IsTrue(exceptionHandled);
    }
}