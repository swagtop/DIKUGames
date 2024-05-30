namespace Breakout.Effects.Powerups;

using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using Breakout;
using Breakout.Entities;

public class HardBall : IEffect {
    private static IBaseImage normalBall = new Image(Path.Combine("Assets", "Images", "ball.png"));
    private static IBaseImage hardBall = new Image(Path.Combine("Assets", "Images", "ball2.png"));

    private static GameEvent disengageEvent = new GameEvent {
        EventType = GameEventType.TimedEvent,
        Message = "DISENGAGE_EFFECT",
        Id = 301,
        ObjectArg1 = new HardBall(),
    };

    public void EngageEffect(EntityContainer<Ball> balls, Player player) {
        foreach (Ball ball in balls) {
            ball.IsHard = true;
            ball.Image = hardBall;
        }
        BreakoutBus.GetBus().AddOrResetTimedEvent(disengageEvent, TimePeriod.NewSeconds(5));
    }

    public void DisengageEffect(EntityContainer<Ball> balls, Player player) {
        foreach (Ball ball in balls) {
            ball.IsHard = false;
            ball.Image = normalBall;
        }
    }
}
