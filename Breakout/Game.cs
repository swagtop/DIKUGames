namespace Breakout;

using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using Breakout.GameStates;

/// <summary>
///
/// </summary>
public class Game : DIKUGame, IGameEventProcessor {  
    private GameEventBus eventBus;
    private StateMachine stateMachine;
    
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        window.SetKeyEventHandler(KeyHandler);

        eventBus = BreakoutBus.GetBus();
        eventBus.InitializeEventBus(new List<GameEventType> {
            GameEventType.WindowEvent,
            GameEventType.StatusEvent,
            GameEventType.GameStateEvent,
            GameEventType.PlayerEvent,
            GameEventType.GraphicsEvent,
            GameEventType.TimedEvent,
        });
        eventBus.Subscribe(GameEventType.WindowEvent, this);

        stateMachine = StateMachine.GetInstance();
        stateMachine.InitializeStateMachine(
            (GameStateType.MainMenu, MainMenu.GetInstance()),
            (GameStateType.ChooseLevel, ChooseLevel.GetInstance()),
            (GameStateType.GameRunning, GameRunning.GetInstance()),
            (GameStateType.GamePaused, GamePaused.GetInstance()),
            (GameStateType.PostGame, PostGame.GetInstance())
        );
        eventBus.Subscribe(GameEventType.GameStateEvent, stateMachine);
    }

    public override void Render() { 
        stateMachine.ActiveState.RenderState();
    }

    public override void Update() {
        eventBus.ProcessEvents();
        stateMachine.ActiveState.UpdateState();
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        stateMachine.ActiveState.HandleKeyEvent(action, key);
    }

    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType != GameEventType.WindowEvent) return;
        
        if (gameEvent.Message == "QUIT_GAME") {
            window.CloseWindow();
            System.Environment.Exit(0);
        }
    }
}
