namespace Breakout.PowerUpEffects;

public class Wide : PowerUpEffects {
    public void ActivateWide () {
        var currentPosition = this.Shape.Position;
        this.Shape.Exstent = new Vec2F(this.SHape.Extent.X * 2, this.Shape.Extent.Y);
        this.Shape.Position = new.Vec2F(currentPosition.X - (this.Shape.Extent.X / 4), currentPosition.Y);
        // Implementering af timer 
    }

}
