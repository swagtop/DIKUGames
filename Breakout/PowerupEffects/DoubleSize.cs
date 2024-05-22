namespace Breakout.PowerUpEffects;

public class DoubleSize : PowerUpEffects {
    public vois ActivateDoubleSize (List<Ball>) {
        foreach (var ball in balls) {
            ball.Shape.Extent *= 2;
        }
        // Implementering af timer 
    }
}
