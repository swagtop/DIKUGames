using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System;
using DIKUArcade;
using DIKUArcade.Events;
using Breakout.GameStates;
using Breakout.Entities;

namespace Breakout;
public class Points : Text
{

    private uint currentPoints;
    private GameEventBus eventBus = BreakoutBus.GetBus();
    public Points() : base("Points:0", new Vec2F(0.77f, 0.1f), new Vec2F(0.2f, 0.2f))
    {
        currentPoints = 0;
    }

    public void AwardPoints(Block block)
    {
        if (block.IsDeleted())
            if (block is HardenedBlock)
                currentPoints += 2;
            else
            {
                currentPoints += 1;
            }

    }

    public void Reset()
    {
        currentPoints = 0;
        SetColor(new Vec3F(1.0f, 1.0f, 1.0f));
    }

    public void SetPointColor()
    {

    }
    public uint GetPoints()
    {
        return currentPoints;
    }

    public void UpdatePointsDisplay()
    {
        SetText($"Points: {currentPoints}");
    }

    public void Render()
    {
        RenderText();
    }
    public void HasWon()
    {
        if (currentPoints >= 3)
        {
            eventBus.RegisterEvent(new GameEvent
            {
                EventType = GameEventType.GameStateEvent,
                To = StateMachine.GetInstance(),
                Message = "CHANGE_STATE",
                StringArg1 = "GAME_WON"
            });
        }
    }
}