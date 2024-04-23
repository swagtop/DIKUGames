using Breakout.Entities;

namespace Breakout.HitStrategies;
public interface IHitStrategy {
    bool Hit(Block block);
}