namespace Breakout.Entities.Blocks;

using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using Breakout;

public class PowerupBlock : Block {
    public PowerupBlock(IBaseImage image, IBaseImage damagedImage, Shape shape) : base(image, damagedImage, shape) {}

    public override bool Hit() {
        DeleteEntity();
        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
            EventType = GameEventType.StatusEvent,
            Message = "SPAWN_POWERUP",
            ObjectArg1 = (object)Shape.Position
        });
        return IsDeleted();
    }
}
