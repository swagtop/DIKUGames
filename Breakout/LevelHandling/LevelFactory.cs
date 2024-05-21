using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Entities;

namespace Breakout.LevelHandling;
public static class LevelFactory
{
    public static Level LoadFromFile(string filepath)
    {
        LevelFileSections levelFileSections;
        LevelMeta levelMeta;
        Dictionary<char, Image[]> levelLegend;
        EntityContainer<Block> blocks;
        string[] fileLines;

        try
        {
            fileLines = File.ReadAllText(filepath).Split('\n');
            for (int i = 0; i < fileLines.Length; i++)
            {
                fileLines[i] = fileLines[i].Trim(); //Trim() deletes all leading and trailing whitespaces
            }
        }
        catch
        {
            throw new Exception("Level file could not be read.");
        }

        levelFileSections = GetLevelFileSections(fileLines);

        levelMeta = ParseMetaSection(levelFileSections.Meta);
        levelLegend = ParseLegendSection(levelFileSections.Legend);

        blocks = ParseMapSection(levelFileSections.Map, levelMeta, levelLegend);

        return new Level(levelMeta, blocks);
    }

    public static LevelFileSections GetLevelFileSections(string[] lines)
    {
        int mapStart, mapEnd;
        int metaStart, metaEnd;
        int legendStart, legendEnd;

        int index = 1;

        try
        {
            while (lines[index - 1] != "Map:") index++;
            mapStart = index;
            while (lines[index] != "Map/") index++;
            mapEnd = index - mapStart;

            while (lines[index - 1] != "Meta:") index++;
            metaStart = index;
            while (lines[index] != "Meta/") index++;
            metaEnd = index - metaStart;

            while (lines[index - 1] != "Legend:") index++;
            legendStart = index;
            while (lines[index] != "Legend/") index++;
            legendEnd = index - legendStart;
        }
        catch
        {
            throw new Exception("Level file invalid or corrupted.");
        }


        string[] mapSection = new ArraySegment<string>(lines, mapStart, mapEnd).ToArray();
        string[] metaSection = new ArraySegment<string>(lines, metaStart, metaEnd).ToArray();
        string[] legendSection = new ArraySegment<string>(lines, legendStart, legendEnd).ToArray();

        return new LevelFileSections(mapSection, metaSection, legendSection);
    }

    public static LevelMeta ParseMetaSection(string[] lines)
    {
        LevelMeta levelMeta = new LevelMeta();
        string[] itemPair = new string[2];

        try
        {
            for (int i = 0; i < lines.Length; i++)
            {
                itemPair = lines[i].Split(": "); // Split each line to make a pair
                switch (itemPair[0])
                {
                    case "Name":
                        levelMeta.LevelName = itemPair[1];
                        break;
                    case "Time":
                        levelMeta.TimeLimit = Int32.Parse(itemPair[1]);
                        break;
                    case "PowerUp":
                        break;
                    case "Hardened":
                        levelMeta.CharDictionary.Add(char.Parse(itemPair[1]), BlockType.HardenedBlock);
                        break;
                    case "Unbreakable":
                        levelMeta.CharDictionary.Add(char.Parse(itemPair[1]), BlockType.UnbreakableBlock);
                        break;
                    default:
                        break;
                }
            }
        }
        catch
        {
            throw new Exception("Level meta section invalid or corrupted.");
        }

        return levelMeta;
    }

    public static Dictionary<char, Image[]> ParseLegendSection(string[] lines)
    {
        Dictionary<char, Image[]> levelLegend = new Dictionary<char, Image[]>();
        string[] itemPair = new string[2];

        try
        {
            for (int i = 0; i < lines.Length; i++)
            {
                itemPair = lines[i].Split(") ");
                levelLegend.Add(
                    char.Parse(itemPair[0]),
                    new Image[2] {
                        new Image(Path.Combine("Assets", "Images", itemPair[1])),
                        new Image(Path.Combine("Assets", "Images", itemPair[1]
                            .Substring(0, itemPair[1].Length - 4) + "-damaged.png"))
                });
            }
        }
        catch
        {
            throw new Exception("Level legend section invalid or corrupted.");
        }

        return levelLegend;
    }

    public static EntityContainer<Block> ParseMapSection(string[] lines, LevelMeta levelMeta, Dictionary<char, Image[]> levelLegend)
    {
        int maxBlockRows = 30;
        int maxNumberOfBlocksInRow = 12;
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

        try
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (rowQueue.Count() > maxBlockRows - 1) continue; // Ignore rows after maxBlockRows.
                rowQueue.Enqueue(lines[i]);
            }

            rowsInQueue = rowQueue.Count();
            for (int i = 0; i < rowsInQueue; i++)
            {
                string row = rowQueue.Dequeue();
                for (int j = 0; j < row.Length; j++)
                {
                    if ((row[j] == '-') || (j > maxNumberOfBlocksInRow - 1)) continue;

                    Image normalImage;
                    Image damagedImage;
                    BlockType blockType;

                    if (levelLegend.ContainsKey(row[j]))
                    {
                        normalImage = levelLegend[row[j]][0];
                        damagedImage = levelLegend[row[j]][1];
                    }
                    else
                    {
                        normalImage = defaultNormalImage;
                        damagedImage = defaultDamagedImage;
                    }

                    if (levelMeta.CharDictionary.ContainsKey(row[j]))
                    {
                        blockType = levelMeta.CharDictionary[row[j]];
                    }
                    else
                    {
                        blockType = BlockType.Block;
                    }

                    blocks.AddEntity(BlockFactory.CreateBlock(normalImage, damagedImage, blockType, i, j));
                }
            }
        }
        catch
        {
            throw new Exception("Level map section invalid or corrupted.");
        }

        return blocks;
    }
}