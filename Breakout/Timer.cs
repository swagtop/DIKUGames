// using System;
// using DIKUArcade.Graphics;
// using DIKUArcade.Timers;
// using DIKUArcade.Math;
// using Breakout.LevelHandling;
// namespace Breakout;

// public class Timer
// {
//     public int PlayerTime { get; private set; }
//     private Text TimerText;
//     private double startTime;
//     public Int32 maxTime;
//     public double TimeElapsed { get; private set; }
//     public Timer(Level level)
//     {
//         if (level.Meta.TimeLimit <= 0)
//         {
//             TimerText = new Text($"No TimeLimit", new Vec2F(0.6f, 0.0f), new Vec2F(0.4f, 0.25f));
//             this.maxTime = level.Meta.TimeLimit;
//             TimerText.SetColor(new Vec3I(255, 255, 255)); // White color
//         }
//         else
//         {
//             this.maxTime = level.Meta.TimeLimit;
//             TimerText = new Text($"Time left: {maxTime - TimeElapsed}", new Vec2F(0.6f, 0.0f), new Vec2F(0.4f, 0.25f));
//             TimeElapsed = 0.0f;
//             TimerText.SetColor(new Vec3I(255, 255, 255)); // White color
//             startTime = StaticTimer.GetElapsedSeconds();
//         }
//     }
//     public void PrintTimeLimit()
//     {
//         Console.WriteLine("the time limit is : " + maxTime);
//     }

//     public void RenderTime()
//     {
//         if (maxTime <= 0)
//         {
//             TimerText.RenderText();
//         }
//         else
//         {
//             TimerText.SetText($"Time left: {String.Format("{0:0}", maxTime - TimeElapsed)}");
//             TimerText.RenderText();
//         }
//     }
//     public void Update()
//     {
//         TimeElapsed = StaticTimer.GetElapsedSeconds();
//     }

//     public bool CheckLose()
//     {
//         if (maxTime <= 0)
//         {
//             return false;
//         }
//         if (TimeElapsed >= maxTime)
//         {
//             return true;
//         }
//         else
//         {
//             return false;
//         }
//     }
// }
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