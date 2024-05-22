namespace Breakout.GameStates;

using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.State;

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

    public static StateMachine GetInstance() {
        return StateMachine.instance;
    }
    
    public void InitializeStateMachine(params (GameStateType gameStateType, IGameState instance)[] states) {
        if (initialized) throw new InvalidOperationException("StateMachine is already initialized!");
        if (states.Length == 0) throw new ArgumentException("StateMachine must initialize with at least one GameStateType and GameState pair!");

        ActiveState = states[0].Item2; // ActiveState initializes with first state in params.

        foreach ((GameStateType gameStateType, IGameState instance) in states) {
            gameStateDictionary.Add(gameStateType, instance);
            // ActiveState.ResetState();
        }

        initialized = true;
    }

    public void SwitchState(GameStateType gameStateType) {
        if (gameStateDictionary.ContainsKey(gameStateType)) {
            ActiveState = gameStateDictionary[gameStateType];
        } else {
            throw new ArgumentException($"Could not switch to state. Did you initialize the StateMachine with {gameStateType}?");
        }
    }

    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType != GameEventType.GameStateEvent) return;
        
        if (gameEvent.Message == "CHANGE_STATE") {
            SwitchState(GameStateTransformer.TransformStringToState(gameEvent.StringArg1));
        }
    }
}
