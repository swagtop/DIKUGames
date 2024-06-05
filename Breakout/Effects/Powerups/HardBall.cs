namespace Breakout.Effects.Powerups;

using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using Breakout;
using Breakout.Entities;

/// <summary>
/// This powerup changes every existing ball to be hard when engaged, and changes them back to
/// normal when disengaged.
/// </summary>
public class HardBall : PowerupEffect, IEffect {
    private static IBaseImage normalBall = new Image(Path.Combine("Assets", "Images", "ball.png"));
    private static IBaseImage hardBall = new Image(Path.Combine("Assets", "Images", "ball2.png"));

    private static GameEvent disengageEvent = new GameEvent {
        EventType = GameEventType.TimedEvent,
        Message = "DISENGAGE_EFFECT",
        Id = 301,
        ObjectArg1 = new HardBall(),
    };

    /// <summary> Makes balls hard, adds timed event to return them to normal. </param>
    /// <param name="balls"=> The ball container of balls we wish to make hard </param>
    /// <param name="player"=> Not used here. </param>
    public override void EngageEffect(EntityContainer<Ball> balls, Player player) {
        foreach (Ball ball in balls) {
            ball.IsHard = true;
            ball.Image = hardBall;
        }
        BreakoutBus.GetBus().AddOrResetTimedEvent(disengageEvent, TimePeriod.NewSeconds(5));
    }

    /// <summary> Returns balls to normal </param>
    /// <param name="balls"=> The ball container of balls we wish to make normal </param>
    /// <param name="player"=> Not used here. </param>
    public override void DisengageEffect(EntityContainer<Ball> balls, Player player) {
        foreach (Ball ball in balls) {
            ball.IsHard = false;
            ball.Image = normalBall;
        }
    }
}
