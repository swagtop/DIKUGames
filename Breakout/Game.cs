using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using Breakout.States;

namespace Breakout;
public class Game : DIKUGame {  
    private GameEventBus eventBus = BreakoutBus.GetBus();
    private StateMachine stateMachine = StateMachine.GetInstance();
    
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        window.SetKeyEventHandler(KeyHandler);
    }

    public override void Render() { 
        // TODO: Tilføj rendering fra den aktive state i stateMachine
    }

    public override void Update() {
        // TODO: Tilføj updates af den aktive state i stateMachine
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        // TODO: Send keyhandling til den aktive state i stateMachine
    }

    // TODO: Tilføj håndtering af GameEvent.CloseWindow
}
