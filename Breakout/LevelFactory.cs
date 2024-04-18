using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout.Entities;

namespace Breakout;
public static class LevelFactory {
    public static EntityContainer<Block> FromFile(string filepath) {
        float xRatio = 1.0f/12.0f;
        float yRatio = xRatio/3.0f;

        EntityContainer<Block> blocks = new EntityContainer<Block>();
        string levelName = "";
        int timeLimit = -1;

        string fileContents = File.ReadAllText(filepath);
        string[] levelStrings = fileContents.Split('\n');
        for (int i = 0; i < levelStrings.Length; i++) {
            levelStrings[i] = levelStrings[i].Trim();
        }
        
        int line = 0;

        // READ MAP LINES
        if (levelStrings[line] != "Map:") {
            throw new Exception("Level file does not start with 'Map:'");
        } 
        
        Queue<string> blockRows = new Queue<string>();
        for (line = line + 1; line < levelStrings.Length; line++) {
            if (levelStrings[line] == "Map/") break;
            Console.WriteLine(levelStrings[line]);
            blockRows.Enqueue(levelStrings[line]);
        }
        line += 2;

        // READ META LINES
        if (levelStrings[line] != "Meta:") {
            throw new Exception("Level file has invalid placement for 'Meta:'");
        } 
        
        string[] pair = new string[2];
        Dictionary<string, string> metaDict = new Dictionary<string, string>();
        for (line = line + 1; line < levelStrings.Length; line++) {
            if (levelStrings[line] == "Meta/") break;
            Console.WriteLine(levelStrings[line]);
            pair = levelStrings[line].Split(": ");
            switch (pair[0]) {
                case "Name":
                    levelName = pair[1];
                    break;
                case "Time":
                    timeLimit = Int32.Parse(pair[1]);
                    break;
                case "Powerup":
                    break;
                case "Hardened":
                    break;
                case "Unbreakable":
                    break;
                default:
                    break;
            }
        }
        line += 2;

        // READ LEGEND LINES
        if (levelStrings[line] != "Legend:") {
            throw new Exception("Level file has invalid placement for 'Legend:'");
        } 

        Dictionary<string, Tuple<Image, Image>> legendDict = new Dictionary<string, Tuple<Image, Image>>();
        for (line = line + 1; line < levelStrings.Length; line++) {
            if (levelStrings[line] == "Legend/") break;
            Console.WriteLine(levelStrings[line]);
            pair = levelStrings[line].Split(") ");
            legendDict.Add(
                pair[0], 
                new Tuple<Image, Image>(
                    new Image(Path.Combine("Assets", "Images", pair[1])),
                    new Image(Path.Combine("Assets", "Images", pair[1]
                        .Substring(0, pair[1].Length - 4) + "-damaged.png"))
                )
            );
        }

        // MANUFACTURE BLOCKS
        int blockCount = blockRows.Count();
        for (int i = 0; i < blockCount; i++) {
            string row = blockRows.Dequeue();
            for (int j = 0; j < 11; j++) {
                if (row[j] != '-') {
                    blocks.AddEntity(new Block(
                        legendDict[Char.ToString(row[j])].Item1, 
                        legendDict[Char.ToString(row[j])].Item2, 
                        new StationaryShape(new Vec2F(1.0f - (j * xRatio), 1.0f - (i*yRatio)), new Vec2F(xRatio, yRatio)),
                        true, // Hardened?
                        true, // Unbreakable?
                        true  // Drops powerup?
                    ));
                }
            }
        }

        return blocks;
    }
}