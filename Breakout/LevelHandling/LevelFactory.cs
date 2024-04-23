using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout.Entities;

namespace Breakout.LevelHandling;
public static class LevelFactory {
    public static LevelMeta ParseMetaStrings(string[] strings) {
        LevelMeta levelMeta = new LevelMeta();
        string[] itemPair = new string[2]; 

        for (int i = 0; i < strings.Length; i++) {
            itemPair = strings[i].Split(": ");
            switch (itemPair[0]) {
                case "Name":
                    levelMeta.LevelName = itemPair[1];
                    break;
                case "Time":
                    levelMeta.TimeLimit = Int32.Parse(itemPair[1]);
                    break;
                case "Powerup":
                    levelMeta.PowerupChar = char.Parse(itemPair[1]);
                    break;
                case "Hardened":
                    levelMeta.HardenedChar = char.Parse(itemPair[1]);
                    break;
                case "Unbreakable":
                    levelMeta.UnbreakableChar = char.Parse(itemPair[1]);
                    break;
                default:
                    break;
            }
        }
        return levelMeta;
    }

    public static Dictionary<char, Image[]> ParseLegendStrings(string[] strings) {
        Dictionary<char, Image[]> levelLegend = new Dictionary<char, Image[]>();
        string[] itemPair = new string[2]; 

        for (int i = 0; i < strings.Length; i++) {
            itemPair = strings[i].Split(") ");
            levelLegend.Add(
                char.Parse(itemPair[0]), 
                new Image[2] {
                    new Image(Path.Combine("Assets", "Images", itemPair[1])),
                    new Image(Path.Combine("Assets", "Images", itemPair[1]
                        .Substring(0, itemPair[1].Length - 4) + "-damaged.png"))
            });
        }
        return levelLegend;
    }

    public static EntityContainer<Block> ParseMapStrings(string[] strings, LevelMeta levelMeta, Dictionary<char, Image[]> legendDictionary) {
        float xRatio = 1.0f/12.0f;
        float yRatio = xRatio/3.0f;
        int maxBlockRows = 30;
        int rowsInQueue;

        
        Image defaultNormalImage = new Image(
            Path.Combine(
                "Assets", 
                "Images", 
                "grey-block.png"
        ));
        Image defaultDamagedImage = new Image(
            Path.Combine(
                "Assets", 
                "Images", 
                "grey-block-damaged.png"
        ));
        
        EntityContainer<Block> blocks = new EntityContainer<Block>();
        Queue<string> blockRows = new Queue<string>();
        
        for (int i = 0; i < strings.Length; i++) {
            if (blockRows.Count() > maxBlockRows - 1) continue; // Ignore rows after maxBlockRows.
            blockRows.Enqueue(strings[i]);
        }
        
        rowsInQueue = blockRows.Count();
        for (int i = 0; i < rowsInQueue; i++) {
            string row = blockRows.Dequeue();
            for (int j = 0; j < row.Length; j++) {
                if (row[j] == '-') continue;
                
                Image normalImage;
                Image damagedImage;

                try {
                    normalImage = legendDictionary[row[j]][0];
                    damagedImage = legendDictionary[row[j]][1];
                } catch (System.Collections.Generic.KeyNotFoundException) {
                    normalImage = defaultNormalImage;
                    damagedImage = defaultDamagedImage;
                }

                blocks.AddEntity(new Block(
                    normalImage,
                    damagedImage,
                    new StationaryShape(
                        new Vec2F(j * xRatio, 1.0f - ((i + 1)*yRatio)), 
                        new Vec2F(xRatio, yRatio)
                    ),
                    row[j] == levelMeta.HardenedChar,
                    row[j] == levelMeta.UnbreakableChar
                ));
            }
        }
        return blocks;
    }

    public static Level LoadFromFile(string filepath) {
        int mapStart, mapEnd;
        int metaStart, metaEnd;
        int legendStart, legendEnd;

        string[] levelStrings = File.ReadAllText(filepath).Split('\n');
        for (int i = 0; i < levelStrings.Length; i++) {
            levelStrings[i] = levelStrings[i].Trim();
        }

        int index = 1;
        mapStart = index;
        while (levelStrings[index + 1] != "Map/") {
            index++;
        }
        mapEnd = index - mapStart + 1;
        index += 4;

        metaStart = index;
        while (levelStrings[index + 1] != "Meta/") {
            index++;
        }
        metaEnd = index - metaStart + 1;
        index += 4;

        legendStart = index;
        legendEnd = levelStrings.Length - 1 - legendStart;

        LevelMeta levelMeta = ParseMetaStrings(
            new ArraySegment<string>(levelStrings, metaStart, metaEnd).ToArray()
        );
        Dictionary<char, Image[]> levelLegend = ParseLegendStrings(
            new ArraySegment<string>(levelStrings, legendStart, legendEnd).ToArray()
        );
        EntityContainer<Block> blocks = ParseMapStrings(new ArraySegment<string>(
            levelStrings, mapStart, mapEnd).ToArray(), 
            levelMeta, 
            levelLegend
        );

        return new Level(levelMeta, blocks);
    }
}