namespace Breakout.Entities.Blocks;

using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using Breakout;

/// <summary>
/// The PowerupBlock spawns a powerup effect close to its position when broken.
/// </summary>
public class PowerupBlock : Block {
    public PowerupBlock(IBaseImage image, IBaseImage damagedImage, Shape shape) : base(image, damagedImage, shape) {}

    /// <summary> Decrements health, tells GameRunning to spawn powerup if broken. </summary>
    public override bool Hit() {
        Health -= 1;
        if (this.IsDeleted()) {
            BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.StatusEvent,
                Message = "SPAWN_POWERUP",
                ObjectArg1 = (object)Shape.Position
            });
            return true;
        }
        return false;
    }
}
