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

namespace Breakout.LevelHandling;
public class LevelData {
    public string LevelName;
    public int TimeLimit;
    public EntityContainer<Block> Blocks;

    public LevelData() {
        LevelName = "";
        TimeLimit = -1;
        Blocks = new EntityContainer<Block>();
    }
}