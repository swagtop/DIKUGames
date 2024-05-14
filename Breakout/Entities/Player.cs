using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

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
    
    public void Reset() {
        Shape.Position = new Vec2F((1.0f - Shape.Extent.X)/2.0f, 0.0f);
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