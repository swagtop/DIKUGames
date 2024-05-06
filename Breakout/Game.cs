using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using Breakout.GameStates;

namespace Breakout;
public class Game : DIKUGame, IGameEventProcessor {  
    private GameEventBus eventBus;
    private StateMachine stateMachine;
    
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        window.SetKeyEventHandler(KeyHandler);

        eventBus = BreakoutBus.GetBus();
        stateMachine = StateMachine.GetInstance();
        
        eventBus.InitializeEventBus(new List<GameEventType> {
            GameEventType.WindowEvent,
            GameEventType.GameStateEvent,
            GameEventType.PlayerEvent,
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
        if (gameEvent.EventType != GameEventType.WindowEvent) return;
        
        switch (gameEvent.Message) {
            case "QUIT_GAME":
                window.CloseWindow();
                System.Environment.Exit(0);
                break;
        }
    }
}
