using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;

namespace Breakout;
public class Game : DIKUGame {
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        window.SetKeyEventHandler(KeyHandler);
    }

    public override void Render() { 
    }

    public override void Update() {
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
    }
}
