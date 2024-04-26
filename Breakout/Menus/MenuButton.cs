using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Menus;
public class MenuButton : Text {
    public Vec3F ActiveColor;
    public Vec3F PassiveColor;
    public string Text;
    public string Value;

    public MenuButton(string text, string value, Vec3F activeColor, Vec3F passiveColor, Vec2F position) : base(text, position, new Vec2F(0.3f, 0.3f)) {
        Text = text;
        Value = value;
        ActiveColor = activeColor;
        PassiveColor = passiveColor;
        SetColor(passiveColor);
    }

    public void Hover() {
        SetColor(ActiveColor);
    }
    
    public void Unhover() {
        SetColor(PassiveColor);
    }
}