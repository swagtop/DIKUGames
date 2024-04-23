using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout.Entities;
using Breakout.States;

namespace Breakout;
public static class LevelFactory {
    public static void LoadFromFile(string filepath, IGameEventProcessor to) {
        // BLOCK POSITIONS
        float xRatio = 1.0f/12.0f;
        float yRatio = xRatio/3.0f;
        int maxBlockRows = 30;

        Image defaultNormalImage = new Image(Path.Combine("Assets", "Images", "grey-block.png"));
        Image defaultDamagedImage = new Image(Path.Combine("Assets", "Images", "grey-block-damaged.png"));

        // LEVEL DATA
        EntityContainer<Block> blocks = new EntityContainer<Block>();
        string levelName = "";
        int timeLimit = -1;

        // OBJECTS NEEDED FOR READING LINES
        int line = 0;
        string[] pair = new string[2];
        Queue<string> blockRows = new Queue<string>();
        Dictionary<string, string> metaDict = new Dictionary<string, string>();
        Dictionary<string, Tuple<Image, Image>> legendDict = new Dictionary<string, Tuple<Image, Image>>();

        string[] levelStrings = File.ReadAllText(filepath).Split('\n');
        for (int i = 0; i < levelStrings.Length; i++) {
            levelStrings[i] = levelStrings[i].Trim();
        }

        // READ MAP LINES
        if (levelStrings[line] != "Map:") {
            throw new Exception("Level file does not start with 'Map:'");
        } 
        
        for (line = line + 1; line < levelStrings.Length; line++) {
            if (levelStrings[line] == "Map/") break;
            if (blockRows.Count() >= maxBlockRows) continue; // Ignore blocks after row 30.
            if (line == levelStrings.Length - 1) throw new Exception("Level file corrupted.");
            
            blockRows.Enqueue(levelStrings[line]);
        }
        line += 2;

        // READ META LINES
        if (levelStrings[line] != "Meta:") {
            throw new Exception("Level file has invalid placement for 'Meta:'");
        } 
        
        for (line = line + 1; line < levelStrings.Length; line++) {
            if (levelStrings[line] == "Meta/") break;
            if (line == levelStrings.Length - 1) throw new Exception("Level file corrupted.");

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

        for (line = line + 1; line < levelStrings.Length; line++) {
            if (levelStrings[line] == "Legend/") break;
            if (line == levelStrings.Length - 1) throw new Exception("Level file corrupted.");

            pair = levelStrings[line].Split(") ");
            legendDict.Add(
            pair[0], 
            new Tuple<Image, Image>(
                new Image(Path.Combine("Assets", "Images", pair[1])),
                new Image(Path.Combine("Assets", "Images", pair[1]
                    .Substring(0, pair[1].Length - 4) + "-damaged.png"))
            ));
        }

        // MANUFACTURE BLOCKS
        for (int i = 0; i < blockRows.Count(); i++) {
            string row = blockRows.Dequeue();
            for (int j = 0; j < row.Length; j++) {
                if (row[j] != '-') {
                    Image normalImage;
                    Image damagedImage;

                    try {
                        normalImage = legendDict[Char.ToString(row[j])].Item1;
                        damagedImage = legendDict[Char.ToString(row[j])].Item2;
                    } catch (System.Collections.Generic.KeyNotFoundException e) {
                        normalImage = defaultNormalImage;
                        damagedImage = defaultDamagedImage;
                    }

                    blocks.AddEntity(new Block(
                        normalImage,
                        damagedImage,
                        new StationaryShape(new Vec2F(j * xRatio, 1.0f - ((i + 1)*yRatio)), new Vec2F(xRatio, yRatio)),
                        false, // Hardened?
                        false  // Unbreakable?
                    ));
                }
            }
        }

        // SEND OFF BLOCKS TO GAMERUNNING
        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
            EventType = GameEventType.GameStateEvent,
            To = to,
            Message = "LOAD_LEVEL",
            StringArg1 = levelName,
            ObjectArg1 = (object)blocks,
            IntArg1 = timeLimit
        });
    }
}