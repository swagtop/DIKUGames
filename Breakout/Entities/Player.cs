namespace Breakout.Entities;

using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

/// <summary>
/// This is the Player class, the entity that the player directly controls. It moves based on
/// the commands it receives from the event bus, which should be generated on input from the
/// player when inside the GameRunning game state.
/// </summary>
public class Player : Entity, IGameEventProcessor {
    private bool fat = false;
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    private const float MOVEMENT_SPEED = 0.02f;

    public Player() : base(
        new DynamicShape(new Vec2F((1.0f - 0.07f)/2.0f, 0.0f), new Vec2F(0.14f, 0.0275f)),
        new Image(Path.Combine("Assets", "Images", "player.png"))
    ) {}
    
    public Player(DynamicShape shape, IBaseImage image) : base(shape, image) {}

    /// <summary> Doubles player width, if not already fat </summary>
    public void GetFat() {
        if (fat) return;

        var currentPosition = Shape.Position.Copy();
        Shape.Extent = new Vec2F(Shape.Extent.X * 2, Shape.Extent.Y);
        Shape.Position = new Vec2F(currentPosition.X - (Shape.Extent.X / 4), currentPosition.Y);

        fat = true;
    }
    
    /// <summary> Halves player width, if not already skinny </summary>
    public void GetSkinny() {
        if (!fat) return;

        var oldExtent = Shape.Extent.Copy();
        Shape.Extent = new Vec2F(Shape.Extent.X / 2, Shape.Extent.Y);
        Shape.Position.X = Shape.Position.X + (oldExtent.X / 4);
        
        fat = false;
    }

    /// <summary> Loads moveLeft and moveRight values into entity direction </summary>
    private void UpdateDirection() {
        Shape.AsDynamicShape().ChangeDirection(new Vec2F(moveLeft + moveRight, 0.0f));
    }

    private void SetMoveLeft(bool val) {
        if (val) { moveLeft = -MOVEMENT_SPEED; } 
        else { moveLeft = 0f; }
        UpdateDirection();
    }

    private void SetMoveRight(bool val) {
        if (val) { moveRight = MOVEMENT_SPEED; } 
        else { moveRight = 0f; }
        UpdateDirection();
    }

    /// <summary> Moves player based on direction, but preventing out of bounds movement </summary>
    public void Move() {
        float leftSide = Shape.Position.X;
        float rightSide = Shape.Position.X + Shape.Extent.X;

        if (leftSide + moveLeft < 0.0f) {
            Shape.Position.X = 0.0f;
        } else if (rightSide + moveRight > 1.0f) {
            Shape.Position.X = 1.0f - Shape.Extent.X;
        } else {
            Shape.Move();
        }
    }
    
    /// <summary> Resets player to default parameters </summary>
    public void Reset() {
        Shape.Position = new Vec2F((1.0f - Shape.Extent.X)/2.0f, 0.0f);
        SetMoveRight(false);
        SetMoveLeft(false);
        GetSkinny();
    }

    /// <summary> Interprets behaviour from game events. </summary>
    /// <param name="gameEvent"> The game event received from event bus. </param>
    public void ProcessEvent(GameEvent gameEvent) {
        switch (gameEvent.Message) {
            case "MOVE":
                switch (gameEvent.StringArg1) {
                    case "LEFT":
                        SetMoveLeft(gameEvent.StringArg2 == "START");
                        break;
                    case "RIGHT":
                        SetMoveRight(gameEvent.StringArg2 == "START");
                        break;
                }
                break;
            default:
                break;
        }
    }
}
