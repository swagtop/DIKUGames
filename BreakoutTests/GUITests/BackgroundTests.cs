namespace BreakoutTests;

using System.Collections.Generic;
using NUnit.Framework;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Entities;
using Breakout.GUI;
using Breakout.LevelHandling;

public class BackgroundTests {
    private Background background;
    private IBaseImage noImage = new NoImage();

    [SetUp]
    public void Setup() {
    }

    [Test]
    public void BackgroundNoExceptionsTest() {
        background = new Background(noImage);
        background.RenderBackground();

        Assert.Pass();
    }
}
