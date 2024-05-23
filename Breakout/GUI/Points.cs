namespace Breakout.GUI;

using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Entities;

public class Points : Text {
    private uint currentPoints;
    private const uint POINTS_BASE_AMOUNT = 100;

    public Points() : base("Points:0", new Vec2F(0.77f, 0.1f), new Vec2F(0.2f, 0.2f)) {
        currentPoints = 0;
        SetColor(new Vec3F(1.0f, 1.0f, 1.0f));
    }

    public void AwardPointsFor(Block block) {
        if (block is HardenedBlock) {
            currentPoints += POINTS_BASE_AMOUNT * 2;
        } else {
            currentPoints += POINTS_BASE_AMOUNT;
        }
        UpdatePointsDisplay();
    }

    public void ResetPoints() {
        currentPoints = 0;
        UpdatePointsDisplay();
    }

    public uint GetPoints() {
        return currentPoints;
    }

    public void UpdatePointsDisplay() {
        SetText($"Points: {currentPoints}");
    }

    public void RenderPoints() {
        RenderText();
    }
}