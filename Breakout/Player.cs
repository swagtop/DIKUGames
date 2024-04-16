using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout;
public class Player : IGameEventProcessor {
    private Entity entity;
    private DynamicShape shape;
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    private float moveUp = 0.0f;
    private float moveDown = 0.0f;
    private const float MOVEMENT_SPEED = 0.01f;

    public Player(DynamicShape shape, IBaseImage image) {
        entity = new Entity(shape, image);
        this.shape = shape;
    }

    private void UpdateDirection() {
        shape.Direction.X = moveLeft + moveRight;
    }

    private void SetMoveLeft(bool val) {
        if (val) {
            moveLeft = - MOVEMENT_SPEED;
        } else {
            moveLeft = 0f;
        }
        UpdateDirection();
    }

    private void SetMoveRight(bool val) {
        if (val) {
            moveRight = MOVEMENT_SPEED;
        } else {
            moveRight = 0f;
        }
        UpdateDirection();
    }
    
    public void Render() {
        entity.RenderEntity();
    }

    public void Move() {
        if (!(shape.Position.X+moveLeft > 1.0f - shape.Extent.X) && !(shape.Position.X+moveRight < 0.0)) {
            shape.Position.X += shape.Direction.X;
        }
    }

    public Vec2F GetPosition {
        get { return shape.Position; }
    }

    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.Message == "MOVE") {
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
}