using Breakout.Entities;

namespace Breakout.HitStrategies;
public class StandardHit : IHitStrategy {
    public bool Hit(Block block) {
        block.Health -= 1;
        return block.IsDeleted();
    }
}