using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout.Entities;

namespace Breakout.LevelHandling;
public static class LevelFactory {
    public static LevelMeta ParseMetaLines(string[] lines) {
        LevelMeta levelMeta = new LevelMeta();
        string[] itemPair = new string[2]; 

        for (int i = 0; i < lines.Length; i++) {
            itemPair = lines[i].Split(": ");
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

    public static Dictionary<char, Image[]> ParseLegendLines(string[] lines) {
        Dictionary<char, Image[]> levelLegend = new Dictionary<char, Image[]>();
        string[] itemPair = new string[2]; 

        for (int i = 0; i < lines.Length; i++) {
            itemPair = lines[i].Split(") ");
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

    public static EntityContainer<Block> ParseMapLinesWithMetaAndLegend(string[] lines, LevelMeta levelMeta, Dictionary<char, Image[]> levelLegend) {
        float xRatio = 1.0f/12.0f;
        float yRatio = xRatio/3.0f;
        int maxBlockRows = 30;
        int rowsInQueue;
        EntityContainer<Block> blocks = new EntityContainer<Block>();
        Queue<string> rowQueue = new Queue<string>();
        
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
        
        for (int i = 0; i < lines.Length; i++) {
            if (rowQueue.Count() > maxBlockRows - 1) continue; // Ignore rows after maxBlockRows.
            rowQueue.Enqueue(lines[i]);
        }
        
        rowsInQueue = rowQueue.Count();
        for (int i = 0; i < rowsInQueue; i++) {
            string row = rowQueue.Dequeue();
            for (int j = 0; j < row.Length; j++) {
                if (row[j] == '-') continue;
                
                Image normalImage;
                Image damagedImage;

                try {
                    normalImage = levelLegend[row[j]][0];
                    damagedImage = levelLegend[row[j]][1];
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

        string[] fileLines = File.ReadAllText(filepath).Split('\n');
        for (int i = 0; i < fileLines.Length; i++) {
            fileLines[i] = fileLines[i].Trim();
        }

        int index = 1;
        mapStart = index;
        while (fileLines[index + 1] != "Map/") {
            index++;
        }
        mapEnd = index - mapStart + 1;
        index += 4;

        metaStart = index;
        while (fileLines[index + 1] != "Meta/") {
            index++;
        }
        metaEnd = index - metaStart + 1;
        index += 4;

        legendStart = index;
        legendEnd = fileLines.Length - legendStart - 1 ;

        LevelMeta levelMeta = ParseMetaLines(
            new ArraySegment<string>(fileLines, metaStart, metaEnd).ToArray()
        );
        Dictionary<char, Image[]> levelLegend = ParseLegendLines(
            new ArraySegment<string>(fileLines, legendStart, legendEnd).ToArray()
        );
        EntityContainer<Block> blocks = ParseMapLinesWithMetaAndLegend(
            new ArraySegment<string>(fileLines, mapStart, mapEnd).ToArray(),
            levelMeta, 
            levelLegend
        );

        return new Level(levelMeta, blocks);
    }
}