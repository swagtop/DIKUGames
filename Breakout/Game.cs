using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;

namespace Breakout;
public class Game : DIKUGame {
    private GameEventBus eventBus = BreakoutBus.GetBus();
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
