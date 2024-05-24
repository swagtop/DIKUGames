namespace BreakoutTests;

using NUnit.Framework;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout.GUI;
using Breakout.Entities;

public class TimerTests {
    private Timer timer;

    [SetUp]
    public void Setup() {
    }
    
    [Test]
    public void ConstructorTest() {
        timer = new Timer();

        Assert.AreEqual(timer.GetTimeLeft(100), -100);
    }

    [Test]
    public void SetTimeLimitTest() {
        timer = new Timer();
        timer.SetTimeLimit(1);

        Assert.Pass();
    }
    
    [Test]
    public void GetTimeLeftTest() {
        timer = new Timer();
        timer.SetTimeLimit(1);

        Assert.Pass();
    }

    [Test]
    public void ResetTimerTest() {
        timer = new Timer();
        Assert.AreEqual(timer.GetTimeLeft(100), -100);

        timer.Reset();
        Assert.AreEqual(timer.GetTimeLeft(100), -101);
    }
    
    [Test]
    public void UpdateTimerTest() {
        timer = new Timer();
        timer.SetTimeLimit(100);

        for (int i = 0; i < 101; i++) {
            timer.UpdateTimer(1);
        }

        timer.SetTimeLimit(100);
        timer.UpdateTimer(1);

        Assert.Pass();
    }

    [Test]
    public void RenderTimerTest() {
        timer = new Timer();
        timer.RenderTimer();

        Assert.Pass();
    }
    
    [Test]
    public void TimerIsUpTest() {
        timer = new Timer();
        timer.SetTimeLimit(100);

        for (int i = 0; i < 101; i++) {
            timer.UpdateTimer(1);
        }

        Assert.AreEqual(timer.TimeIsUp(100), false);
    }
}
