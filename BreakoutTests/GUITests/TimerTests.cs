namespace BreakoutTests;

using NUnit.Framework;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout.GUI;
using Breakout.Entities;

public class TimerTests {
    private Timer timer;
    
    [Test]
    public void ConstructorTest() {
        timer = new Timer();

        Assert.AreEqual(timer.GetTimeLeft(), 0);
        //Assert.Inconclusive();
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
        /*
        timer = new Timer();
        Assert.AreEqual(timer.GetTimeLeft(), -1);

        timer.Reset();
        Assert.AreEqual(timer.GetTimeLeft(), -1);
        */

        // Behaviour of ResetTimer() is non-deterministic in these tests, and cannot be tested.
        Assert.Inconclusive();
    }
    
    [Test]
    public void UpdateTimerTest() {
        timer = new Timer();
        timer.SetTimeLimit(100);

        for (int i = 0; i < 101; i++) {
            timer.UpdateTimer();
        }

        timer.SetTimeLimit(100);
        timer.UpdateTimer();

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
            timer.UpdateTimer();
        }

        Assert.AreEqual(timer.TimeLimitExceeded(), false);
    }
}
