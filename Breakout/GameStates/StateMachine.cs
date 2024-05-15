namespace Breakout.GameStates;

using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.State;

public class StateMachine : IGameEventProcessor {
    private static StateMachine instance = new StateMachine();
    private Dictionary<GameStateType, IGameState> gameStateDictionary = new Dictionary<GameStateType, IGameState>();
    public IGameState ActiveState { get; private set; } = EmptyState.GetInstance();

    public static StateMachine GetInstance() {
        return StateMachine.instance;
    }
    
    public void InitializeStateMachine(params (GameStateType gameStateType, IGameState instance)[] states) {
        ActiveState = states[0].Item2; // ActiveState initializes with first state in params.

        foreach ((GameStateType gameStateType, IGameState instance) in states) {
            gameStateDictionary.Add(gameStateType, instance);
        }
    }

    public void SwitchState(GameStateType gameStateType) {
        if (gameStateDictionary.ContainsKey(gameStateType)) {
            ActiveState = gameStateDictionary[gameStateType];
            ActiveState.ResetState();
        } else {
            throw new ArgumentException($"Unrecognized GameStateType: {gameStateType}");
        }
    }

    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType != GameEventType.GameStateEvent) return;
        
        if (gameEvent.Message == "CHANGE_STATE") {
            SwitchState(GameStateTransformer.TransformStringToState(gameEvent.StringArg1));
        }
    }
}

public class EmptyState : IGameState {
    private static EmptyState instance = new EmptyState();

    public static EmptyState GetInstance() {
        return EmptyState.instance;
    }

    public void ResetState() {}
    public void RenderState() {}
    public void UpdateState() {}
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {}
}
