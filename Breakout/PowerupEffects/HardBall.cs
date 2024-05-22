namespace Breakout.PowerUpEffects;

public class Hardball : PowerUpEffects {
    public void ActivateHardBall(List<Ball> balls) {
        foreach (var ball in balls) {
            ball.IsHard = true;
        }
    }
    // implement timer 
}