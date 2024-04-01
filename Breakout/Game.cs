using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using Breakout.States;

namespace Breakout;
public class Game : DIKUGame, IGameEventProcessor {  
    private GameEventBus eventBus;
    private StateMachine stateMachine;
    
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        eventBus = BreakoutBus.GetBus();
        stateMachine = StateMachine.GetInstance();
        
        window.SetKeyEventHandler(KeyHandler);
        eventBus.InitializeEventBus(new List<GameEventType> {
            GameEventType.WindowEvent
        });
        
        eventBus.Subscribe(GameEventType.WindowEvent, this);
    }

    public override void Render() { 
        stateMachine.ActiveState.RenderState();
    }

    public override void Update() {
        eventBus.ProcessEventsSequentially();
        stateMachine.ActiveState.UpdateState();
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        stateMachine.ActiveState.HandleKeyEvent(action, key);
    }

    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.WindowEvent) {
            switch (gameEvent.Message) {
                case "CLOSE_WINDOW":
                    window.CloseWindow();
                    System.Environment.Exit(0);
                    break;
            }
        } 
    }
}
