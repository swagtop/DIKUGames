namespace Breakout.GUI;

using System;
using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using Breakout;
using Breakout.GameStates;

/// <summary>
/// The Timer class is both responsible for rendering the timer, and keeping track of time spent
/// in levels with time limits.
/// </summary>
public class Timer : Text {
    private GameEventBus eventBus = BreakoutBus.GetBus();
    private int timeLeft;
    private int timeLimit;
    
    public Timer() : base("TimeLeft: -1", new Vec2F(0.77f, 0.01f), new Vec2F(0.2f, 0.2f)) {
        timeLeft = -1;
        SetColor(new Vec3F(1.0f, 1.0f, 1.0f));
    }

    public void SetTimeLimit(int timeLimit) {
        this.timeLimit = timeLimit;
    }

    public int GetTimeLeft() {
        double timePassed = StaticTimer.GetElapsedSeconds();
        double timeLeftD = (double)timeLimit - timePassed;
        timeLeft = (int)timeLeftD;
        return timeLeft;
    }

    public void Reset() {
        timeLeft = -1;
        timeLimit = -1;
    }

    public void UpdateTimer() {
        if (timeLimit < 0) {
            SetText($"No time Limit");
        } else {
            SetText($"TimeLeft: {GetTimeLeft()}");
        }
    }

    public void RenderTimer() {
        if (timeLimit >= 0) {
            RenderText();
        }
    }

    public bool TimeLimitExceeded() {
        double timePassed = StaticTimer.GetElapsedSeconds();
        return (timeLimit > 0 && timeLimit - timePassed < 0);
    }
}
