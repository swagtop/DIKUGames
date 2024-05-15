namespace Breakout.GUI;

using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Entities;

public class Points : Text {
    private uint currentPoints;

    public Points() : base("Points:0", new Vec2F(0.77f, 0.1f), new Vec2F(0.2f, 0.2f)) {
        currentPoints = 0;
    }

    public void AwardPoints(Block block) {
        if (block is HardenedBlock) {
            currentPoints += 2;
        } else {
            currentPoints += 1;
        }
    }

    public void Reset() {
        currentPoints = 0;
        SetColor(new Vec3F(1.0f, 1.0f, 1.0f));
    }

    public void SetPointColor() {
    }

    public void UpdatePointsDisplay() {
        SetText($"Points: {currentPoints}");
    }

    public void Render() {
        RenderText();
    }
}