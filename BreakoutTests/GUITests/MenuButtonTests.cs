namespace BreakoutTests;

using NUnit.Framework;
using DIKUArcade.Math;
using Breakout.GUI;

public class MenuButtonTests {
    private string text = "text";
    private string value = "value";
    private Vec3F activeColor = new Vec3F(1.0f, 1.0f, 1.0f);
    private Vec3F passiveColor = new Vec3F(0.1f, 0.1f, 0.1f);
    private Vec2F position = new Vec2F(0.0f, 0.0f);
    private MenuButton menuButton;

    [SetUp]
    public void Setup() {
    }
    
    [Test]
    public void ConstructorTest() {
        menuButton = new MenuButton(
            text,
            value,
            activeColor,
            passiveColor,
            position
        );
        Assert.AreEqual(menuButton.Text, text);
        Assert.AreEqual(menuButton.Value, value);
        Assert.AreEqual(menuButton.ActiveColor, activeColor);
        Assert.AreEqual(menuButton.PassiveColor, passiveColor);
    }
}
