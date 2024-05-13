using DIKUArcade.Entities;
using DIKUArcade.Math;
using Breakout.Entities;


namespace Breakout;

public class BallLauncher
{
    private readonly EntityContainer<Ball> balls;
    private readonly Player player;

    public BallLauncher(EntityContainer<Ball> balls, Player player)
    {
        this.balls = balls;
        this.player = player;
    }

    public void LaunchBall()
    {
        Vec2F launchVector = new Vec2F(0.0f, 0.0f);
        balls.Iterate(ball =>
        {
            float directionX = (ball.Shape.Position.X) - (player.Shape.Position.X + player.Shape.Extent.X / 2);
            float directionY = 1.0f;

            // Normalize the vector
            Vec2F normalizedDir = Vec2F.Normalize(new Vec2F(directionX * 10, directionY));

            // Scale the vector to get the desired speed of the ball
            float desiredLength = 0.015f;
            normalizedDir *= desiredLength;

            // Add the scaled direction to the launch vector
            launchVector += normalizedDir;
            ball.Shape.AsDynamicShape().ChangeDirection(launchVector);
        });
    }
}

