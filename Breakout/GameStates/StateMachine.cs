namespace Breakout.GameStates;

using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.State;

/// <summary> 
/// The StateMachine class, responsible for switching the gamestate, and supplying
/// the active state to the Game class, to be rendered and interacted with. 
/// </summary>
public class StateMachine : IGameEventProcessor {
    private static StateMachine instance = new StateMachine();
    private Dictionary<GameStateType, IGameState> gameStateDictionary = new Dictionary<GameStateType, IGameState>();
    private bool initialized = false;
    private IGameState? activeState = null;

    public IGameState ActiveState { 
        get {
            if (activeState != null) return activeState;
            else throw new Exception("StateMachine has not been initialized.");
        }
        private set { activeState = value; }
    }

    /// <summary> GetInstance method for Singleton purposes. </summary>
    public static StateMachine GetInstance() {
        return StateMachine.instance;
    }
    
    /// <summary> Initializes StateMachine with GameStateType values and game states </summary>
    /// <param name="states"> Pairs of GameStateType and GameState instances </param>
    public void InitializeStateMachine(params (GameStateType gameStateType, IGameState instance)[] states) {
        if (initialized) throw new InvalidOperationException("StateMachine is already initialized!");
        if (states.Length == 0) throw new ArgumentException("StateMachine must initialize with at least one GameStateType and GameState pair!");

        ActiveState = states[0].Item2; // ActiveState initializes with first state in params.

        foreach ((GameStateType gameStateType, IGameState instance) in states) {
            gameStateDictionary.Add(gameStateType, instance);
        }

        initialized = true;
    }

    /// <summary> Switches to game state associated with GameStateType </summary>
    /// <param name="gameStateType"> A GameStateType enum value </param>
    public void SwitchState(GameStateType gameStateType) {
        if (gameStateDictionary.ContainsKey(gameStateType)) {
            ActiveState = gameStateDictionary[gameStateType];
        } else {
            throw new ArgumentException($"Could not switch to state. Did you initialize the StateMachine with {gameStateType}?");
        }
    }

    /// <summary> Receives order to change state from event bus here </summary>
    /// <param name="gameEvent"> The game event received from event bus. </param>
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType != GameEventType.GameStateEvent) return;
        
        if (gameEvent.Message == "CHANGE_STATE") {
            SwitchState(GameStateTransformer.TransformStringToState(gameEvent.StringArg1));
        }
    }
}
