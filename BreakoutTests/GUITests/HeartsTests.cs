namespace BreakoutTests;

using NUnit.Framework;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout.GUI;
using Breakout.Entities;

public class HeartsTests {
    private Hearts hearts;
    
    [OneTimeSetUp]
    public void Init() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
    }
    
    [Test]
    public void ConstructorTest() {
        hearts = new Hearts(10);

        Assert.AreEqual(hearts.Amount, 10);
    }
    
    [Test]
    public void SetHeartsTest() {
        hearts = new Hearts(10);
        hearts.SetHearts(11);

        Assert.AreEqual(hearts.Amount, 11);
    }
    
    [Test]
    public void SurviveBreakHeartTest() {
        hearts = new Hearts(10);
        bool breakHeartReturn = hearts.BreakHeart();

        Assert.AreEqual(hearts.Amount, 9);
        Assert.AreEqual(breakHeartReturn, false);
    }
    
    [Test]
    public void KillingBreakHeartTest() {
        hearts = new Hearts(1);

        bool breakHeartFirstReturn = hearts.BreakHeart();
        Assert.AreEqual(hearts.Amount, 0);
        Assert.AreEqual(breakHeartFirstReturn, false);

        bool breakHeartSecondReturn = hearts.BreakHeart();
        Assert.AreEqual(hearts.Amount, 0);
        Assert.AreEqual(breakHeartSecondReturn, true);
    }

    [Test]
    public void RenderHeartsTest() {
        hearts = new Hearts(2);
        hearts.BreakHeart();

        hearts.RenderHearts();
    }
}
