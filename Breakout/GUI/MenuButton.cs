namespace Breakout.GUI;

using DIKUArcade.Graphics;
using DIKUArcade.Math;

/// <summary>
/// The MenuButton class is a text class that has a value and text. The text is the text that is
/// rendered, where the value is a string that can be any value. The value of the MenuButton is
/// meant to be used in switch cases, dictionaries or whatever it should show to be useful for.
/// Instances of MenuButton should only ever exist inside Menu instances.
/// </summary>
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
