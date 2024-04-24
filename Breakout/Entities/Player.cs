using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout.Entities;
public class Player : Entity, IGameEventProcessor {
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    private const float MOVEMENT_SPEED = 0.01f;

    public Player(DynamicShape shape, IBaseImage image) : base(shape, image) {
    }

    private void UpdateDirection() {
        Shape.AsDynamicShape().ChangeDirection(new Vec2F(moveLeft + moveRight, 0.0f));
    }

    private void SetMoveLeft(bool val) {
        if (val) { moveLeft = - MOVEMENT_SPEED; } 
        else { moveLeft = 0f; }
        UpdateDirection();
    }

    private void SetMoveRight(bool val) {
        if (val) { moveRight = MOVEMENT_SPEED; } 
        else { moveRight = 0f; }
        UpdateDirection();
    }

    public void Move() {
        if (!(Shape.Position.X+moveLeft > 1.0f - Shape.Extent.X) && !(Shape.Position.X+moveRight < 0.0)) {
            Shape.Position.X += Shape.AsDynamicShape().Direction.X;
        }
    }

    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.Message != "MOVE") return;
        
        switch (gameEvent.StringArg1) {
            case "LEFT":
                SetMoveLeft(gameEvent.StringArg2 == "START");
                break;
            case "RIGHT":
                SetMoveRight(gameEvent.StringArg2 == "START");
                break;
        }
    }
}