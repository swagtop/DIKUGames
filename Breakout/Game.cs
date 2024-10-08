namespace Breakout;

using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using Breakout.GameStates;

/// <summary>
/// The main Game class. This class initializes the event bus and state machine.
/// After this initialization, it renders the active state, and sends off the player inputs to
/// said state. This class is also responsible for handling the quitting of the game.
/// </summary>
public class Game : DIKUGame, IGameEventProcessor {  
    private GameEventBus eventBus;
    private StateMachine stateMachine;
    
    /// <summary> Constructor, initializes event bus and StateMachine </summary>
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

    /// <summary> Renders active state of StateMachine </summary>
    public override void Render() { 
        stateMachine.ActiveState.RenderState();
    }

    /// <summary> Updates event bus, THEN updates active state of StateMachine </summary>
    public override void Update() {
        eventBus.ProcessEvents();
        stateMachine.ActiveState.UpdateState();
    }

    /// <summary> Sends key inputs to active state of StateMachine </summary>
    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        stateMachine.ActiveState.HandleKeyEvent(action, key);
    }

    /// <summary> Quits game when receiving signal from event bus. </summary>
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType != GameEventType.WindowEvent) return;
        
        if (gameEvent.Message == "QUIT_GAME") {
            window.CloseWindow();
            System.Environment.Exit(0);
        }
    }
}
