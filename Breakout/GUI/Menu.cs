namespace Breakout.GUI;

using DIKUArcade.Graphics;
using DIKUArcade.Math;

/// <summary>
/// The Menu class contains a list of buttons, that can be interacted with. The purpose of this
/// class is to make menues easy and painless to initialize and iteract with.
/// </summary>
public class Menu {
    private float topButtonPosition;
    private Vec3F activeColor = new Vec3F(1.0f, 1.0f, 1.0f);
    private Vec3F passiveColor = new Vec3F(0.4f, 0.4f, 0.4f);
    private int activeButton = 0;
    public List<MenuButton> Buttons = new List<MenuButton>();

    /// <summary> Constructs empty Menu. </summary>
    public Menu(float topButtonPosition) {
        this.topButtonPosition = topButtonPosition;
    }
    
    /// <summary> Constructs Menu with text and value pairs. </summary>
    public Menu(float topButtonPosition, params (string text, string value)[] pairs) {
        this.topButtonPosition = topButtonPosition;
        foreach ((string text, string value) in pairs) {
            AddButton(text, value);
        }
        Buttons[activeButton].Hover();
    }

    /// <summary> Adds new MenuButton to list. </summary>
    public void AddButton(string text, string value) {
        Buttons.Add(new MenuButton(
            text, 
            value, 
            activeColor, 
            passiveColor, 
            new Vec2F(0.11f, topButtonPosition - (Buttons.Count)*0.1f)
        ));
    }

    /// <summary> Changes focus to the button above the active one, if not at the top. </summary>
    public void GoUp() {
        if (activeButton - 1 < 0) return;

        Buttons[activeButton].Unhover();
        activeButton--;
        Buttons[activeButton].Hover();
    }

    /// <summary> Changes focus to the button below the active one, if not at bottom. </summary>
    public void GoDown() {
        if (activeButton + 1 > Buttons.Count - 1) return;

        Buttons[activeButton].Unhover();
        activeButton++;
        Buttons[activeButton].Hover();
    }

    /// <summary> Fetches the text field of the focused button. </summary>
    public string GetText() {
        return Buttons[activeButton].Text;
    }
    
    /// <summary> Fetches the value field of the focused button. </summary>
    public string GetValue() {
        return Buttons[activeButton].Value;
    }

    /// <summary> Sets focused button to be the top one. </summary>
    public void Reset() {
        Buttons[activeButton].Unhover();
        activeButton = 0;
        Buttons[activeButton].Hover();
    }

    /// <summary> Empties menu button list. </summary>
    public void Clear() {
        Buttons.Clear();
        activeButton = 0;
    }

    /// <summary> Renders each individual button in button list. </summary>
    public void RenderMenu() {
        foreach (MenuButton button in Buttons) {
            button.RenderText();
        }
    }
}
