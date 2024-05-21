using System;
using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.GameStates;
using Breakout;

namespace Breakout;
public class Timer : Text
{
    private int timeLeft;
    private int timeLimit;
    private GameEventBus eventBus = BreakoutBus.GetBus();
    public Timer() : base("TimeLeft: -1", new Vec2F(0.77f, 0.01f), new Vec2F(0.2f, 0.2f))
    {
        timeLeft = -1;

    }
    public void SetTimeLimit(int timeLimit)
    {
        this.timeLimit = timeLimit;
    }
    public int GetTimeLeft(double timePassed)
    {
        double timeLeftD = (double)timeLimit - timePassed;
        timeLeft = (int)timeLeftD;
        return timeLeft;
    }

    public void Reset()
    {
        timeLeft = -1;
        timeLimit = -1;
        SetColor(new Vec3F(1.0f, 1.0f, 1.0f));
    }
    public void UpdateTimer(double timePassed)
    {
        if (timeLimit < 0)
        {
            SetText($"No time Limit");
        }
        else
            SetText($"TimeLeft: {GetTimeLeft(timePassed)}");
    }
    public void Render()
    {
        if (timeLimit > 0)
        {
            RenderText();
        }
    }

    public void TimeIsUp(double timePassed)
    {
        if (timeLimit > 0 && timeLimit - timePassed < 0)
        {
            eventBus.RegisterEvent(new GameEvent
            {
                EventType = GameEventType.GameStateEvent,
                To = StateMachine.GetInstance(),
                Message = "CHANGE_STATE",
                StringArg1 = "GAME_OVER"
            });
        }
    }

}