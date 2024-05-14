namespace Breakout.Entities;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;

public class HardenedBlock : Block{
    public HardenedBlock(IBaseImage image, IBaseImage damagedImage, Shape shape) : base(image, damagedImage, shape, 2){
    }
}
