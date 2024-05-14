namespace Breakout.GUI;

using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class Menu {
    private float topButtonPosition;
    private Vec3F activeColor = new Vec3F(1.0f, 1.0f, 1.0f);
    private Vec3F passiveColor = new Vec3F(0.4f, 0.4f, 0.4f);
    private int activeButton = 0;
    public List<MenuButton> Buttons = new List<MenuButton>();

    public Menu(float topButtonPosition) {
        this.topButtonPosition = topButtonPosition;
    }
    
    public Menu(float topButtonPosition, params (string text, string value)[] pairs) {
        this.topButtonPosition = topButtonPosition;
        foreach ((string text, string value) in pairs) {
            AddButton(text, value);
        }
        Buttons[activeButton].Hover();
    }

    public void AddButton(string text, string value) {
        Buttons.Add(new MenuButton(
            text, 
            value, 
            activeColor, 
            passiveColor, 
            new Vec2F(0.11f, topButtonPosition - (Buttons.Count)*0.1f)
        ));
    }

    public void GoUp() {
        if (activeButton - 1 < 0) return;

        Buttons[activeButton].Unhover();
        activeButton--;
        Buttons[activeButton].Hover();
    }

    public void GoDown() {
        if (activeButton + 1 > Buttons.Count - 1) return;

        Buttons[activeButton].Unhover();
        activeButton++;
        Buttons[activeButton].Hover();
    }

    public string GetText() {
        return Buttons[activeButton].Text;
    }
    
    public string GetValue() {
        return Buttons[activeButton].Value;
    }

    public void Reset() {
        Buttons[activeButton].Unhover();
        activeButton = 0;
        Buttons[activeButton].Hover();
    }

    public void Clear() {
        Buttons.Clear();
        activeButton = 0;
    }

    public void RenderButtons() {
        foreach (MenuButton button in Buttons) {
            button.RenderText();
        }
    }
}
