namespace Breakout.Entities;

using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Utilities;

/// <summary>
/// This is the BallLauncher class. Adds a new ball to the GameRunning game state when asked to,
/// and launches this ball when prompted by the GameRunning game state aswell.
/// The direction of the ball when launched, is based on its relation to the player, such that
/// the player can aim which direction they would like the ball to go.
/// </summary>
public class BallLauncher {
    private readonly EntityContainer<Ball> balls;
    private readonly Player player;

    /// <summary> Constructor, fetching the ball container and player instance. </summary>
    /// <param name="balls"> A ball container, should be the one in GameRunning. </param>
    /// <param name="player"> A player instance, should be the one in GameRunning. </param>
    public BallLauncher(EntityContainer<Ball> balls, Player player) {
        this.balls = balls;
        this.player = player;
    }

    /// <summary> Launches the ball in a slightly, based on player position. </summary>
    public void LaunchBall() {
        Vec2F launchVector;
        balls.Iterate(ball => {
            launchVector = new Vec2F(0.0f, 0.0f);
            float playerMiddle = player.Shape.Position.X + (player.Shape.Extent.X / 2);
            float ballMiddle = ball.Shape.Position.X + (ball.Shape.Extent.X / 2);

            float directionX = ballMiddle - playerMiddle;
            float directionY = 1.0f;

            // Slight randomization
            float randomFloat = RandomGenerator.Generator.NextSingle() * 0.01f;
            if (RandomGenerator.Generator.Next(2) == 0) { randomFloat *= -1; }
            directionX += randomFloat;

            // Normalize the vector
            Vec2F normalizedDir = Vec2F.Normalize(new Vec2F(directionX * 10, directionY));

            // Scale the vector to get the desired speed of the ball
            float desiredLength = 0.015f;
            normalizedDir *= desiredLength;

            // Add the scaled direction to the launch vector
            launchVector += normalizedDir;
            if (ball.Shape.Position.Y < 0.05f) {
                ball.Shape.AsDynamicShape().ChangeDirection(launchVector);
            }
        });
    }

    /// <summary> Loads a new ball into the BallLauncher, ready to be launched. </summary>
    public void AddNewBall() {
        Vec2F ballExtent = new Vec2F(0.025f, 0.025f);
        Vec2F ballPosition = new Vec2F(
            player.Shape.Position.X + (player.Shape.Extent.X / 2) - ballExtent.X / 2,
            player.Shape.Extent.Y
        );
        Vec2F ballDirection = new Vec2F(0.0f, 0.0f);

        // Create a new ball with appropriate parameters
        balls.AddEntity(new Ball(
            new Image(Path.Combine("Assets", "Images", "ball.png")),
            new DynamicShape(ballPosition, ballExtent, ballDirection)
        ));
    }
}
