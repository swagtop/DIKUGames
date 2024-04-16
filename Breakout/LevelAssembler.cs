using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using Breakout.Entities;

namespace Breakout;
public static class LevelAssembler {
    public static EntityContainer<Block> FromFile(string filepath) {
        EntityContainer<Block> blocks = new EntityContainer<Block>();
        string levelName = "Unknown";
        int timeLimit = -1;

        string fileContents = File.ReadAllText(filepath);
        string[] levelStrings = fileContents.Split('\n');
        
        if (levelStrings[0] != "Map:") {
            throw new Exception("Level file invalid or corrupted (Has to start with 'Map:')");
        }
        
        int i = 1;
        Stack<string> blockRows = new Stack<string>();
        while (levelStrings[i] != "Map/") {
            blockRows.Push(levelStrings[i]);
            i++;
        }
        i += 2;

        string[] pair = new string[2];
        Dictionary<string, string> metaDict = new Dictionary<string, string>();
        while (levelStrings[i] != "Meta/") {
            pair = levelStrings[i].Split(": ");
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
        i += 2;

        Dictionary<string, string> legendDict = new Dictionary<string, string>();
        while (levelStrings[i] != "Legend/") {
            pair = levelStrings[i].Split(") ");
            legendDict.Add(pair[0], pair[1]);
        }

        return blocks;
    }
}