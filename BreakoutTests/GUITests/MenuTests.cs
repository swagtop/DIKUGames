namespace BreakoutTests;

using NUnit.Framework;
using Breakout.GUI;

public class MenuTests {
    private Menu menu;

    [SetUp]
    public void Setup() {
    }
    
    [Test]
    public void FirstNewMenuTest() {
        menu = new Menu(0.0f);
        menu.AddButton("text", "value");

        Assert.AreEqual(menu.GetText(), "text");
        Assert.AreEqual(menu.GetValue(), "value");
    }

    [Test]
    public void SecondNewMenuTest() {
        menu = new Menu(
            0.0f,
            ("text", "value")
        );

        Assert.AreEqual(menu.GetText(), "text");
        Assert.AreEqual(menu.GetValue(), "value");
    }

    [Test]
    public void ResetMenuTest() {
        menu = new Menu(
            0.0f,
            ("text1", "value1"),
            ("text2", "value2")
        );

        Assert.AreEqual(menu.GetText(), "text1");
        Assert.AreEqual(menu.GetValue(), "value1");

        menu.GoDown();

        Assert.AreEqual(menu.GetText(), "text2");
        Assert.AreEqual(menu.GetValue(), "value2");

        menu.Reset();

        Assert.AreEqual(menu.GetText(), "text1");
        Assert.AreEqual(menu.GetValue(), "value1");
    }

    [Test]
    public void GoDownTest() {
        menu = new Menu(
            0.0f,
            ("text1", "value1"),
            ("text2", "value2")
        );

        Assert.AreEqual(menu.GetText(), "text1");
        Assert.AreEqual(menu.GetValue(), "value1");

        menu.GoDown();

        Assert.AreEqual(menu.GetText(), "text2");
        Assert.AreEqual(menu.GetValue(), "value2");
    }

    [Test]
    public void GoDownThenUpTest() {
        menu = new Menu(
            0.0f,
            ("text1", "value1"),
            ("text2", "value2")
        );

        Assert.AreEqual(menu.GetText(), "text1");
        Assert.AreEqual(menu.GetValue(), "value1");

        menu.GoDown();

        Assert.AreEqual(menu.GetText(), "text2");
        Assert.AreEqual(menu.GetValue(), "value2");

        menu.GoUp();

        Assert.AreEqual(menu.GetText(), "text1");
        Assert.AreEqual(menu.GetValue(), "value1");
    }

    [Test]
    public void GoingUpCannotGoOutOfBoundsTest() {
        menu = new Menu(
            0.0f,
            ("text1", "value1"),
            ("text2", "value2")
        );

        Assert.AreEqual(menu.GetText(), "text1");
        Assert.AreEqual(menu.GetValue(), "value1");

        menu.GoUp();

        Assert.AreEqual(menu.GetText(), "text1");
        Assert.AreEqual(menu.GetValue(), "value1");
    }

    [Test]
    public void GoingDownCannotGoOutOfBoundsTest() {
        menu = new Menu(
            0.0f,
            ("text1", "value1"),
            ("text2", "value2")
        );

        menu.GoDown();       

        Assert.AreEqual(menu.GetText(), "text2");
        Assert.AreEqual(menu.GetValue(), "value2");

        menu.GoDown();       

        Assert.AreEqual(menu.GetText(), "text2");
        Assert.AreEqual(menu.GetValue(), "value2"); 
    }

    [Test]
    public void AddButtonTest() {
        menu = new Menu(
            0.0f,
            ("text1", "value1")
        );

        menu.GoDown();       

        Assert.AreEqual(menu.GetText(), "text1");
        Assert.AreEqual(menu.GetValue(), "value1");

        menu.GoDown();       

        Assert.AreEqual(menu.GetText(), "text1");
        Assert.AreEqual(menu.GetValue(), "value1");

        menu.AddButton("text2", "value2");

        menu.GoDown();       

        Assert.AreEqual(menu.GetText(), "text2");
        Assert.AreEqual(menu.GetValue(), "value2");
    }

    [Test]
    public void AddManyButtonsTest() {
        menu = new Menu(
            0.0f,
            ("text1", "value1")
        );

        for (int i = 2; i < 100; i++) {
            menu.AddButton($"text{i}", $"value{i}");
        }

        for (int i = 1; i < 100; i++) {
            Assert.AreEqual(menu.GetText(), $"text{i}");
            Assert.AreEqual(menu.GetValue(), $"value{i}");
            menu.GoDown();
        }

    }
}
